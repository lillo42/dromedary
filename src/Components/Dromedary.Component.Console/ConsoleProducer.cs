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
        private readonly string _promptMessage;

        public ConsoleProducer(string promptMessage)
        {
            _promptMessage = promptMessage;
        }

        public IExchangeFactory Factory { get; set; }

        public async Task ExecuteAsync(ChannelWriter<IExchange> channel, CancellationToken cancellationToken = default)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Write(_promptMessage);

                var input = ReadLine();
                var exchange = Factory.Create();
                exchange.Message.Body = input;

                await channel.WriteAsync(exchange, cancellationToken)
                    .ConfigureAwait(false);
            }
            
            channel.Complete();
        }
    }
}
