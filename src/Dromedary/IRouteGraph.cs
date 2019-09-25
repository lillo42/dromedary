using System.Collections.Generic;

namespace Dromedary
{
    public interface IRouteGraph
    {
        IReadOnlyCollection<IRouteNode> Root { get; }
    }
}
