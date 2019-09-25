using System.Collections.Generic;

namespace Dromedary
{
    public interface IChannel : IEnumerable<IProcessor>, IEnumerator<IProcessor>
    {
        
    }
}
