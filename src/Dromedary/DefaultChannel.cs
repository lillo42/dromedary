namespace Dromedary
{
    public class DefaultChannel : IChannel
    {
        public DefaultChannel(IChannelNode root)
        {
            Root = root;
        }

        public IChannelNode Root { get; }
    }
}
