using System;

namespace Dromedary.IdGenerator
{
    public class DefaultIdGenerator : IExchangeIdGenerator, IMessageIdGenerator
    {
        public string Generate(IRoute route) 
            => Guid.NewGuid().ToString();

        public string Generate(IRoute route, IExchange exchange) 
            => Guid.NewGuid().ToString();
    }
}
