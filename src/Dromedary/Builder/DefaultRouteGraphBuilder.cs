using System;
using Dromedary.Factories;
using Dromedary.Statements;

namespace Dromedary.Builder
{
    public class DefaultRouteGraphBuilder : IRouteGraphBuilder
    {
        private readonly IRouteNodeFactory _factory;
        private IRouteNode _root;
        private IRouteNode _currentNode;

        public DefaultRouteGraphBuilder(IRouteNodeFactory factory)
        {
            _factory = factory;
        }

        public IRouteGraphBuilder Add(IStatement statement)
        {
            switch (statement.Statement)
            {
                case Statement.From:
                    _root = _factory.Create(statement);
                    _currentNode = _root;
                    break;
                case Statement.To:
                    var node = _factory.Create(statement);
                    _currentNode.Add(node);
                    _currentNode = node;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return this;
        }

        public virtual IRouteGraph Build() 
            => new DefaultRouteGraph(new []{ _root });
    }
}
