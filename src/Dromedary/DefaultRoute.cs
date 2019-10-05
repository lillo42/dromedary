using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Dromedary.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Dromedary
{
    public class DefaultRoute : IRoute
    {
        public DefaultRoute(string id, string description, IRouteGraph routeGraph, IDromedaryContext context)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Description = description;
            RouteGraph = routeGraph ?? throw new ArgumentNullException(nameof(routeGraph));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string Id { get; }
        public string Description { get; }
        public IRouteGraph RouteGraph { get; }
        public IDromedaryContext Context { get; }
        
        public async Task ExecuteAsync(IServiceProvider service, CancellationToken cancellationToken = default)
        {
            var @from = RouteGraph.Root;
            var configure = from.Statement.ConfigureComponent;

            var component = (IDromedaryComponent)service.GetRequiredService(configure.ComponentType);
            configure.Configure(null, component);

            var producer = component.CreateEndpoint()
                .CreateProducer();

            var channel = Channel.CreateUnbounded<IExchange>(new UnboundedChannelOptions
            {
                SingleWriter = true,
                SingleReader = false
            });
            
            var consumer = ConsumeAsync(service, channel, cancellationToken)
                .ConfigureAwait(false);
            
            var producerTask = producer.ExecuteAsync(channel.Writer, cancellationToken)
                .ConfigureAwait(false);

            await producerTask;
            await consumer;
        }


        private async Task ConsumeAsync(IServiceProvider service, ChannelReader<IExchange> reader, CancellationToken cancellationToken)
        {
            while (await reader.WaitToReadAsync(cancellationToken) & !cancellationToken.IsCancellationRequested)
            {
                if (reader.TryRead(out var exchange))
                {
                    using (var scope = service.CreateScope())
                    {
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
                            
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
        
        }
    }
}
