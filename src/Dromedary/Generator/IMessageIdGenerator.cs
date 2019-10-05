namespace Dromedary.Generator
{
    public interface IMessageIdGenerator
    {
        string Generate(IExchange exchange);
    }
}
