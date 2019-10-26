using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dromedary.Components.Process
{
    internal class ProcessWithFunc : IProcessor
    {
        private readonly Func<IExchange, Task> _func;

        public ProcessWithFunc(Func<IExchange, Task> func)
        {
            _func = func ?? throw new ArgumentNullException(nameof(func));
        }

        public ValueTask ExecuteAsync(IExchange exchange, CancellationToken cancellationToken = default) 
            => new ValueTask(_func(exchange));
    }
}
