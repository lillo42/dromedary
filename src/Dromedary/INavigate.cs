using System.Collections.Generic;

namespace Dromedary
{
    public interface INavigate : IEnumerable<IChannelNode>, IEnumerator<IChannelNode>
    {
        IChannel Channel { get; }
    }
}
