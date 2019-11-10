using System.Diagnostics.CodeAnalysis;

namespace Dromedary
{
    public class DefaultExchangeResolver : IExchangeResolver
    {
        public IExchange Exchange { get; set; }
    }
}
