using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Dromedary.Factories;
using static System.Console;

namespace Dromedary.Component.Console
{
    public class ConsoleProducer : IProducer
    {
        private readonly IExchangeFactory _exchangeFactory;

        public ConsoleProducer(IExchangeFactory exchangeFactory)
        {
            _exchangeFactory = exchangeFactory ?? throw new ArgumentNullException(nameof(exchangeFactory));
        }

        public string PromptMessage { get; set; }
        
        public async Task ExecuteAsync(ChannelWriter<IExchange> channel, CancellationToken cancellationToken = default)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Write(PromptMessage);

                var input = ReadLine();
                var exchange = _exchangeFactory.Create();
                exchange.Message.Body = input;

                await channel.WriteAsync(exchange, cancellationToken)
                    .ConfigureAwait(false);
            }
            
            channel.Complete();
        }
    }
}
