namespace Dromedary.Factories
{
    public class DefaultNavigateFactory : INavigateFactory
    {
        public INavigate Create(IChannel channel) 
            => new DefaultINavigate(channel);
    }
}
