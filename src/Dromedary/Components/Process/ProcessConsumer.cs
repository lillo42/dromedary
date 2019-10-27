using System.Threading;
using System.Threading.Tasks;

namespace Dromedary.Components.Process
{
    internal class ProcessConsumer : IConsumer
    {
        private readonly IProcessor _processor;

        public ProcessConsumer(IProcessor processor)
        {
            _processor = processor;
        }

        public ValueTask ExecuteAsync(IExchange exchange, CancellationToken cancellationToken = default)
            => _processor.ExecuteAsync(exchange, cancellationToken);
    }
}
