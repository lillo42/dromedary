namespace Dromedary.Factories
{
    public class DefaultExchangeFactory : IExchangeFactory
    {
        public IExchange Create(string id, IDromedaryContext context) 
            => new DefaultExchange(id, context);
    }
}
