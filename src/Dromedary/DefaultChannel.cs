using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dromedary.Exceptions;
using Dromedary.Statements;
using Microsoft.Extensions.DependencyInjection;

namespace Dromedary
{
    public class DefaultChannel : IChannel
    {
        private readonly IServiceProvider _service;
        private readonly IRouteGraph _graph;
        private IRouteNode _currentNode;

        public DefaultChannel(IRouteGraph graph, IServiceProvider service)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
            _service = service;
            _currentNode =  _graph.Root;
        }
        
        public async IAsyncEnumerator<IProcessor> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            var current = _graph.Root;

            while (current != null && !cancellationToken.IsCancellationRequested)
            {
                var statement = current.Statement;
                
                var component = (IDromedaryComponent)_service.GetRequiredService(statement.Component);
                await ConfigureComponentAsync(statement, component);
                yield return component.CreateEndpoint()
                    .CreateConsumer();

                current = NextNode(current);
            }
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
            
            throw new InvalidStatementException(statement);
        }

        private static IRouteNode NextNode(IRouteNode current) 
            => current.Children
                .FirstOrDefault();
    }
}
