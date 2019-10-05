using System;

namespace Dromedary.Generator
{
    public class DefaultIdGenerator : IExchangeIdGenerator, IMessageIdGenerator
    {
        public string Generate() 
            => Guid.NewGuid().ToString();

        public string Generate(IExchange exchange) 
            => Guid.NewGuid().ToString();
    }
}
