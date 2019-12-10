using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Dromedary.Factories;

namespace Dromedary.Acceptance.Test
{
    public partial class DromedaryRouteAcceptanceTest
    {
        internal class FakeComponent : IDromedaryComponent
        {
            public TaskCompletionSource<object> CompletionSource { get; set; }
            public object Object { get; set; }
            public int MaxExchangeGenerated { get; set; }
            public IEndpoint CreateEndpoint() 
                => new FakeEndpoint(CompletionSource, Object, MaxExchangeGenerated);

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
            private readonly TaskCompletionSource<object> _completionSource;
            private readonly object _obj;
            private readonly int _maxExchangeGenerated;

            public FakeEndpoint(TaskCompletionSource<object> completionSource, object obj, int maxExchangeGenerated)
            {
                _completionSource = completionSource;
                _obj = obj;
                _maxExchangeGenerated = maxExchangeGenerated;
            }

            public IProducer CreateProducer() 
                => new FakeProducer(_obj, _completionSource, _maxExchangeGenerated);

            public IConsumer CreateConsumer() 
                => new FakeConsumer(_completionSource, _obj);
        }
        
        internal class FakeProducer : IProducer
        {
            private readonly TaskCompletionSource<object> _completionSource;
            private readonly object _obj;
            private readonly int _maxExchangeGenerated;

            public FakeProducer(object obj, TaskCompletionSource<object> completionSource, int maxExchangeGenerated)
            {
                _obj = obj;
                _completionSource = completionSource;
                _maxExchangeGenerated = maxExchangeGenerated;
            }

            public IExchangeFactory Factory { get; set; }

            public async Task ExecuteAsync(ChannelWriter<IExchange> channel, CancellationToken cancellationToken = default)
            {
                int counter = 0;
                while (_maxExchangeGenerated > counter && !cancellationToken.IsCancellationRequested)
                {
                    _completionSource.SetResult(counter);
                    await channel.WriteAsync(Factory.Create(), cancellationToken);
                    counter++;
                }
            }
        }

        internal class FakeConsumer : IConsumer, IProcessor
        {
            private readonly TaskCompletionSource<object> _completionSource;
            private readonly object _obj;

            public FakeConsumer(TaskCompletionSource<object> completionSource, object obj)
            {
                _completionSource = completionSource;
                _obj = obj;
            }

            public ValueTask ExecuteAsync(IExchange exchange, CancellationToken cancellationToken = default)
            {
                _completionSource.SetResult(1);
                return new ValueTask();
            }

            public IProcessor CreateProcessor(IServiceProvider provider)
            {
                return this;
            }
        }
    }
}
