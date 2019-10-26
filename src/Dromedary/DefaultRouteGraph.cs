using System;

namespace Dromedary
{
    public class DefaultRouteGraph : IRouteGraph
    {
        public DefaultRouteGraph(IRouteNode root)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root));
        }

        public IRouteNode Root { get; }
    }
}
