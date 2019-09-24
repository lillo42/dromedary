namespace Dromedary.Factories
{
    public interface IMessageFactory
    {
        IMessage Create(string id, IExchange exchange);
    }
}
