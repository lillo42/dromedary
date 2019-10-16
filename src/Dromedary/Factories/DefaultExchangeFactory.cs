using System;
using Dromedary.Generator;

namespace Dromedary.Factories
{
    public class DefaultExchangeFactory : IExchangeFactory
    {
        private readonly IExchangeIdGenerator _generator;
        private readonly IMessageFactory _messageFactory;

        public DefaultExchangeFactory(IExchangeIdGenerator generator, IMessageFactory messageFactory)
        {
            _generator = generator ?? throw new ArgumentNullException(nameof(generator));
            _messageFactory = messageFactory;
        }

        public IExchange Create() 
        {
            var exchange =  new DefaultExchange(_generator.Generate());
            exchange.Message = _messageFactory.Create(exchange);
            return exchange;
        }
    }
}
