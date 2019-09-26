using System.Collections.Generic;
using Dromedary.Statements;

namespace Dromedary
{
    public class DefaultRouteNode : IRouteNode
    {
        private readonly ICollection<IRouteNode> _children;
        
        public DefaultRouteNode(IStatement statement)
        {
            Statement = statement;
            _children = new List<IRouteNode>();
        }

        public IStatement Statement { get; }
        public IEnumerable<IRouteNode> Children => _children;
        public void Add(IRouteNode children) 
            => _children.Add(children);
    }
}
