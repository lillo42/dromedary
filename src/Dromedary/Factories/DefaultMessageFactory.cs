namespace Dromedary.Factories
{
    public class DefaultMessageFactory : IMessageFactory
    {
        public IMessage Create(string id, IExchange exchange) 
            => new DefaultMessage(id, exchange);
    }
}
