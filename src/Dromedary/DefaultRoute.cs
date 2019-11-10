using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Dromedary.Factories;
using Dromedary.Statements;
using Microsoft.Extensions.DependencyInjection;

namespace Dromedary
{
    public class DefaultRoute : IRoute
    {
        public DefaultRoute(string id, string? description, IRouteGraph routeGraph, bool allowSynchronousContinuations)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Description = description;
            RouteGraph = routeGraph ?? throw new ArgumentNullException(nameof(routeGraph));
            AllowSynchronousContinuations = allowSynchronousContinuations;
        }

        public string Id { get; }
        public string? Description { get; }
        public bool AllowSynchronousContinuations { get; }
        public IRouteGraph RouteGraph { get; }

        public async Task ExecuteAsync(IServiceProvider service, CancellationToken cancellationToken = default)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            var @from = RouteGraph.Root;
            var statement = from.Statement;

            var component = (IDromedaryComponent)service.GetRequiredService(statement.Component);
            await ConfigureComponent(statement, component);

            var producer = component.CreateEndpoint()
                .CreateProducer();

            producer.Factory = service.GetRequiredService<IExchangeFactory>();

            var channel = Channel.CreateUnbounded<IExchange>(new UnboundedChannelOptions
            {
                SingleWriter = true,
                SingleReader = false,
                AllowSynchronousContinuations = AllowSynchronousContinuations 
            });
            
            var consumer = ConsumeAsync(service, channel, cancellationToken)
                .ConfigureAwait(false);
            
            var producerTask = Task.Run(() => producer.ExecuteAsync(channel.Writer, cancellationToken), cancellationToken);

            await producerTask;
            await consumer;
        }

        private static ValueTask ConfigureComponent(IStatement statement, IDromedaryComponent component)
        {
            if (statement.ConfigureComponent != null)
            {
                statement.ConfigureComponent(component);
                return new ValueTask();
            }

            if (statement.ConfigureComponentAsync != null)
            {
                return new ValueTask(statement.ConfigureComponentAsync(component));
            }
            
            //TODO: Review this Exception
            throw new NotSupportedException();
        }


        private async Task ConsumeAsync(IServiceProvider service, ChannelReader<IExchange> reader, CancellationToken cancellationToken)
        {
            try
            {
                while (await reader.WaitToReadAsync(cancellationToken) & !cancellationToken.IsCancellationRequested)
                {
                    if (!reader.TryRead(out var exchange))
                    {
                        continue;
                    }

                    using var scope = service.CreateScope();
                    try
                    {
                        var resolver = scope.ServiceProvider.GetRequiredService<IExchangeResolver>();
                        resolver.Exchange = exchange;
                        var factory = scope.ServiceProvider.GetRequiredService<IChannelFactory>();
                        var channel = factory.Create(RouteGraph);
                        foreach (var process in channel)
                        {
                            await process.ExecuteAsync(exchange, cancellationToken);
                            
                            if (cancellationToken.IsCancellationRequested)
                            {
                                break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        exchange.Exception = e;
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        public void Dispose()
        {
        
        }
    }
}
