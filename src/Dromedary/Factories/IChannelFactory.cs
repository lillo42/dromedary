namespace Dromedary.Factories
{
    public interface IChannelFactory
    {
        IChannel Create(IRouteGraph graph);
    }
}
