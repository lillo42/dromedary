namespace Dromedary.Generator
{
    public interface IExchangeIdGenerator
    {
        string Generate(IRoute route);
    }
}
