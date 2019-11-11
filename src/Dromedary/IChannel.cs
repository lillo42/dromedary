using System;
using System.Collections.Generic;

namespace Dromedary
{
    public interface IChannel : IAsyncEnumerable<IProcessor>
    {
        
    }
}
