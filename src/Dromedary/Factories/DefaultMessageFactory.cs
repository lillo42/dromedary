using System;
using Dromedary.Generator;

namespace Dromedary.Factories
{
    public class DefaultMessageFactory : IMessageFactory
    {
        private readonly IMessageIdGenerator _generator;

        public DefaultMessageFactory(IMessageIdGenerator generator)
        {
            _generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        public IMessage Create(IExchange exchange) 
            => new DefaultMessage(_generator.Generate(exchange), exchange);
    }
}
