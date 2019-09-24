using System.Collections;
using System.Collections.Generic;

namespace Dromedary
{
    public class DefaultINavigate : INavigate
    {
        public DefaultINavigate(IChannel channel)
        {
            Channel = channel;
        }

        public IChannel Channel { get; }
        public IChannelNode Current { get; private set; }
        
        object IEnumerator.Current => Current;
        
        public IEnumerator<IChannelNode> GetEnumerator() 
            => this;

        IEnumerator IEnumerable.GetEnumerator() 
            => GetEnumerator();

        public bool MoveNext()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            Current = Channel.Root;
        }

        public void Dispose()
        {
        }
    }
}
