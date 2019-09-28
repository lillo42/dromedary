namespace Dromedary.Generator
{
    public interface IMessageIdGenerator
    {
        string Generate(IRoute route, IExchange exchange);
    }
}
