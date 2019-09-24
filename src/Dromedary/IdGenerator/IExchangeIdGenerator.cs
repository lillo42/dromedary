namespace Dromedary.IdGenerator
{
    public interface IExchangeIdGenerator
    {
        string Generate(IRoute route);
    }
}
