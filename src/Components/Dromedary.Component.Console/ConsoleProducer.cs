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
        private readonly IMessageFactory _messageFactory;
        public ConsoleProducer(IExchangeFactory exchangeFactory, IMessageFactory messageFactory)
        {
            _exchangeFactory = exchangeFactory ?? throw new ArgumentNullException(nameof(exchangeFactory));
            _messageFactory = messageFactory ?? throw new ArgumentNullException(nameof(messageFactory));
        }

        public string PromptMessage { get; set; }
        
        public async Task ExecuteAsync(ChannelWriter<IExchange> channel, CancellationToken cancellationToken = default)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Write(PromptMessage);

                var input = ReadLine();
                var exchange = _exchangeFactory.Create();
                exchange.Message = _messageFactory.Create(exchange);
                exchange.Message.Body = input;

                await channel.WriteAsync(exchange, cancellationToken)
                    .ConfigureAwait(false);
            }
            
            channel.Complete();
        }
    }
}
