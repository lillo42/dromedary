namespace Dromedary.Factories
{
    public interface IExchangeFactory
    {
        IExchange Create(string id, IDromedaryContext context);
    }
}
