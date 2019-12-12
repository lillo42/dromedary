using System;

namespace Dromedary.Components.Filter
{
    internal class FilterEndpoint : IEndpoint
    {
        private readonly IConsumer _consumer;

        public FilterEndpoint(Func<IExchange, bool> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }
            
            _consumer = new FilterConsumer(filter);
        }
        
        public IProducer CreateProducer()
        {
            throw new NotImplementedException();
        }

        public IConsumer CreateConsumer() 
            => _consumer;
    }
}
