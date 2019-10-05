using System;
using Dromedary.Generator;

namespace Dromedary.Factories
{
    public class DefaultExchangeFactory : IExchangeFactory
    {
        private readonly IDromedaryContext _context;
        private readonly IExchangeIdGenerator _generator;

        public DefaultExchangeFactory(IDromedaryContext context, IExchangeIdGenerator generator)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        public IExchange Create() 
            => new DefaultExchange(_generator.Generate(), _context);
    }
}
