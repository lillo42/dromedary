    using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
    using Dromedary.Builder;
    using Dromedary.Factories;
    using NSubstitute;
    using Xunit;

    namespace Dromedary.Acceptance.Test
{
    public class DromedaryRouteAcceptanceTest
    {
        [Fact]
        public async Task Execute_With_From_To()
        {
            var context = new DefaultDromedaryContextBuilder()
                .AddComponent<FakeComponent>()
                .Build();
            
            var from = new List<string>();
            var to = new List<string>();
            
            context
                .AddRoute(builder =>
                {
                    builder.From<FakeComponent>(c =>
                        {
                            c.Execute = from;
                        })
                        .To<FakeComponent>(c =>
                        {
                            c.Execute = to;
                        });
                });
            
            var source = new CancellationTokenSource();
            var task = context.ExecuteAsync(source.Token);
            
            while (to.Count == 0)
            {
                await Task.Delay(100);
            }
            
            source.Cancel();

            await task;
        }
    }

    public class ControlFake
    {
        public bool ShouldGenerator { get; set; }
    }
    
    public class FakeComponent : IDromedaryComponent
    {
        public ICollection<string> Execute { get; set; }
        public ControlFake ControlFake { get; set; }
        public IEndpoint CreateEndpoint()
        {
            return new FakeEndpoint(Execute);
        }
    }
        
    internal class FakeEndpoint : IEndpoint
    {
        private readonly ICollection<string> _execute;
        private readonly ControlFake _control;

        public FakeEndpoint(ICollection<string> execute, ControlFake control)
        {
            _execute = execute;
            _control = control;
        }

        public IProducer CreateProducer()
        {
            return new FakeProducer(, _control);
        }

        public IConsumer CreateConsumer()
        {
            return new FakeConsumer(_execute);
        }

        public void ConfigureProperties(IDictionary<string, object> option)
        {
            throw new System.NotImplementedException();
        }
    }
        
    internal class FakeProducer : IProducer
    {
        private readonly ControlFake _control;
        private readonly IExchangeFactory _factory;

        public FakeProducer(ControlFake control, IExchangeFactory factory)
        {
            _control = control;
            _factory = factory;
        }

        public async Task ExecuteAsync(ChannelWriter<IExchange> channel, CancellationToken cancellationToken = default)
        {
            while (cancellationToken.IsCancellationRequested)
            {
                if (_control.ShouldGenerator)
                {
                    await channel.WriteAsync(_factory.Create(), cancellationToken);
                }

                await Task.Delay(100);
            }
        }
    }
        
    internal class FakeConsumer : IConsumer
    {
        private readonly ICollection<string> _execute;

        public FakeConsumer(ICollection<string> execute)
        {
            _execute = execute;
        }

        public IProcessor Processor => new FakeProcessor(_execute);
    }

    internal class FakeProcessor : IProcessor
    {
        private readonly ICollection<string> _execute;

        public FakeProcessor(ICollection<string> execute)
        {
            _execute = execute;
        }

        public ValueTask ExecuteAsync(IExchange exchange, CancellationToken cancellationToken = default)
        {
            _execute.Add(exchange.Message.Body.ToString());
            return new ValueTask();
        }
    }
}
