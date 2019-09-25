using System.Collections.Generic;
using Dromedary.Statements;

namespace Dromedary
{
    public interface IRouteNode
    {
        IStatement Statement { get; }
        
        IEnumerable<IRouteNode> Children { get; }

        void Add(IRouteNode children);
    }
}
