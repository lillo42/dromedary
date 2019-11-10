using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dromedary.Statements;
using Microsoft.Extensions.DependencyInjection;

namespace Dromedary
{
    public class DefaultChannel : IChannel
    {
        private readonly IServiceProvider _service;
        private readonly IRouteGraph _graph;
        private readonly IExchange _exchange;
        private IRouteNode _currentNode;

        public DefaultChannel(IRouteGraph graph, IExchange exchange, IServiceProvider service)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
            _exchange = exchange;
            _service = service;
            _currentNode =  _graph.Root;
        }

        public virtual IProcessor? Current { get; protected set; }

        object IEnumerator.Current => Current;

        public virtual IEnumerator<IProcessor> GetEnumerator()
            => this;

        IEnumerator IEnumerable.GetEnumerator() 
            => GetEnumerator();

        public virtual bool MoveNext()
        {
            _currentNode = NextNode(_currentNode);
            
            if (_currentNode == null)
            {
                Current = null;
                return false;
            }
            
            var statement = _currentNode.Statement;
             
            var component = (IDromedaryComponent)_service.GetRequiredService(statement.Component);
            ConfigureComponentAsync(statement, component);
            Current = component.CreateEndpoint()
                .CreateConsumer();
            
            return true;
        }

        private static ValueTask ConfigureComponentAsync(IStatement statement, IDromedaryComponent component)
        {
            if (statement.ConfigureComponent != null)
            {
                statement.ConfigureComponent(component);
                return new ValueTask();
            }

            if (statement.ConfigureComponentAsync != null)
            {
                return new ValueTask(statement.ConfigureComponentAsync(component));
            }
            
            //TODO: Review this exception
            throw new NotSupportedException();
        }

        private static IRouteNode NextNode(IRouteNode current) 
            => current.Children
                .FirstOrDefault();

        public virtual void Reset() 
            => _currentNode = _graph.Root;

        public virtual void Dispose()
        {
            
        }
    }
}
