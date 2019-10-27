using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Dromedary.Builder;
using Dromedary.Factories;
using Xunit;

namespace Dromedary.Acceptance.Test
{
    public class DromedaryRouteAcceptanceTest
    {
        [Fact(Timeout = 1_000)]
        public async Task Execute_With_From_To()
        {
            IDromedaryContextBuilder builder = new DefaultDromedaryContextBuilder();
            var context = builder
                .AddComponent<FakeComponent>()
                .Build();
            
            var from = new List<string>();
            var to = new List<string>();
            
            context
                .AddRoute(builder =>
                {
                    builder.From<FakeComponent>(c =>
                        {
                            c.Logs = from;
                            c.Text = "From";
                            c.MaxExchangeGenerated = 1;
                        })
                        .To<FakeComponent>(c =>
                        {
                            c.Logs = to;
                            c.Text = "To";
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

    internal class FakeComponent : IDromedaryComponent
    {
        public ICollection<string> Logs { get; set; }
        public string Text { get; set; }
        public int MaxExchangeGenerated { get; set; }
        public IEndpoint CreateEndpoint() 
            => new FakeEndpoint(Logs, Text, MaxExchangeGenerated);

        public void ConfigureProperties(Action<IDromedaryComponent> config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            config(this);
        }

        public async Task ConfigurePropertiesAsync(Func<IDromedaryComponent, Task> config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            await config(this);
        }

        public void ConfigureProperties(IDictionary<string, object> config)
        {
            
        }
    }
    
    internal class FakeEndpoint : IEndpoint
    {
        private readonly ICollection<string> _logs;
        private readonly string _text;
        private readonly int _maxExchangeGenerated;

        public FakeEndpoint(ICollection<string> logs, string text, int maxExchangeGenerated)
        {
            _logs = logs;
            _text = text;
            _maxExchangeGenerated = maxExchangeGenerated;
        }

        public IProducer CreateProducer() 
            => new FakeProducer(_text, _logs, _maxExchangeGenerated);

        public IConsumer CreateConsumer() 
            => new FakeConsumer(_logs, _text);
    }
    
    internal class FakeProducer : IProducer
    {
        private readonly ICollection<string> _logs;
        private readonly string _text;
        private readonly int _maxExchangeGenerated;

        public FakeProducer(string text, ICollection<string> logs, int maxExchangeGenerated)
        {
            _text = text;
            _logs = logs;
            _maxExchangeGenerated = maxExchangeGenerated;
        }

        public IExchangeFactory Factory { get; set; }

        public async Task ExecuteAsync(ChannelWriter<IExchange> channel, CancellationToken cancellationToken = default)
        {
            int counter = 0;
            while (_maxExchangeGenerated <  counter && !cancellationToken.IsCancellationRequested)
            {
                _logs.Add(_text);
                await channel.WriteAsync(Factory.Create(), cancellationToken);
                counter++;
            }
        }
    }

    internal class FakeConsumer : IConsumer
    {
        private readonly ICollection<string> _logs;
        private readonly string _text;

        public FakeConsumer(ICollection<string> logs, string text)
        {
            _logs = logs;
            _text = text;
        }

        public ValueTask ExecuteAsync(IExchange exchange, CancellationToken cancellationToken = default)
        {
            _logs.Add(_text);
            return new ValueTask();
        }
    }
}
