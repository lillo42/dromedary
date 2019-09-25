using System;
using System.Collections.Generic;

namespace Dromedary
{
    public class DefaultRouteGraph : IRouteGraph
    {
        public DefaultRouteGraph(IReadOnlyCollection<IRouteNode> root)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root));
        }

        public IReadOnlyCollection<IRouteNode> Root { get; }
    }
}
