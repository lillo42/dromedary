using System.Threading;
using System.Threading.Tasks;

using static System.Console;

namespace Dromedary.Component.Console
{
    public class ConsoleConsumer : IConsumer, IProcessor
    {
        public IProcessor Processor => this;

        public ValueTask ExecuteAsync(IExchange exchange, CancellationToken cancellationToken = default)
        {
            WriteLine(exchange.Message.Body);
            return default;
        }
    }
}
