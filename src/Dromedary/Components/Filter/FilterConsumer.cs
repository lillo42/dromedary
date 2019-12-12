using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dromedary.Components.Filter
{
    public class FilterConsumer : IConsumer, IProcessor
    {
        private readonly Func<IExchange, bool> _filter;

        public FilterConsumer(Func<IExchange, bool> filter)
        {
            _filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public IProcessor CreateProcessor(IServiceProvider provider)
        {
            return this;
        }

        public ValueTask ExecuteAsync(IExchange exchange, CancellationToken cancellationToken = default)
        {
            _filter(exchange);
            return new ValueTask();
        }
    }
}
