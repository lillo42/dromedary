using System;
using Dromedary.Exceptions;
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
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public virtual IRouteGraphBuilder Add(IStatement statement)
        {
            if (statement == null)
            {
                throw new ArgumentNullException(nameof(statement));
            }
            
            switch (statement.Statement)
            {
                case Statement.From:
                    _root = _factory.Create(statement);
                    _currentNode = _root;
                    break;
                case Statement.To:
                    if (_currentNode == null)
                    {
                        throw new DromedaryRouteGraphBuilderException("From node was not add", statement);
                    }
                    
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
