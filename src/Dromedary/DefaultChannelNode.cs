using System.Collections.Generic;
using Dromedary.Commands;

namespace Dromedary
{
    public class DefaultChannelNode : IChannelNode
    {
        public DefaultChannelNode(IChannelNode parent, ICommand command)
        {
            Parent = parent;
            Command = command;
        }

        public IChannelNode Parent { get; }
        public ICollection<IChannelNode> Children { get; } = new List<IChannelNode>();
        public ICommand Command { get; }
    }
}
