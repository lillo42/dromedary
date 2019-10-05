namespace Dromedary.Factories
{
    public interface IMessageFactory
    {
        IMessage Create(IExchange exchange);
    }
}
