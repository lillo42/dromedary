using System;

namespace Dromedary.Generator
{
    public class DefaultIdGenerator : IExchangeIdGenerator, IMessageIdGenerator
    {
        public string Generate(IRoute route) 
            => Guid.NewGuid().ToString();

        public string Generate(IRoute route, IExchange exchange) 
            => Guid.NewGuid().ToString();
    }
}
