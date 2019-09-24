namespace Dromedary.IdGenerator
{
    public interface IMessageIdGenerator
    {
        string Generate(IRoute route, IExchange exchange);
    }
}
