using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dromedary.Statements;
using Microsoft.Extensions.DependencyInjection;

namespace Dromedary
{
    public class DefaultChannel : IChannel
    {
        private readonly IServiceProvider service;
        private readonly IRouteGraph _graph;
        private readonly IExchange _exchange;
        private IRouteNode _currentNode;

        public DefaultChannel(IRouteGraph graph, IExchange exchange, IServiceProvider service)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
            _exchange = exchange;
            this.service = service;
            _currentNode =  _graph.Root;
        }

        public virtual IProcessor Current { get; protected set; }

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
            
            var configure = _currentNode.Statement.ConfigureComponent;
             
            var component = (IDromedaryComponent)service.GetRequiredService(configure.ComponentType);
            configure.Configure(_exchange, component);
            Current = component.CreateEndpoint()
                .CreateConsumer()
                .Processor;
            
            return true;
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
