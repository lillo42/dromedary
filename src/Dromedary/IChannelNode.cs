using System.Collections.Generic;
using Dromedary.Commands;

namespace Dromedary
{
    public interface IChannelNode
    {
        IChannelNode Parent { get; }
        ICollection<IChannelNode> Children { get; }
        
        ICommand Command { get; }
    }
}
