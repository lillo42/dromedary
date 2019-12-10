using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dromedary.Components.Process
{
    internal class ProcessWithAction : IConsumer, IProcessor
    {
        private readonly Action<IExchange> _action;

        public ProcessWithAction(Action<IExchange> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public ValueTask ExecuteAsync(IExchange exchange, CancellationToken cancellationToken = default)
        {
            _action(exchange);
            return new ValueTask();
        }

        public IProcessor CreateProcessor(IServiceProvider provider) 
            => this;
    }
}
