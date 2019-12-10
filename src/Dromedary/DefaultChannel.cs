using System;
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

        public DefaultChannel(IRouteGraph graph, IServiceProvider service)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
            _service = service;
        }
        
        public IAsyncEnumerator<IProcessor> GetAsyncEnumerator(CancellationToken cancellationToken = default) 
            => new Enumerator(this, cancellationToken);

        internal async ValueTask<IProcessor> CreateProcessor(IStatement statement)
        {
            var component = (IDromedaryComponent)_service.GetRequiredService(statement.Component);
            await ConfigureComponentAsync(statement, component);
            return component.CreateEndpoint()
                .CreateConsumer();
        }

        internal static ValueTask ConfigureComponentAsync(IStatement statement, IDromedaryComponent component)
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

        

        private class Enumerator : IAsyncEnumerator<IProcessor>
        {
            private readonly DefaultChannel _channel;
            private readonly CancellationToken _cancellationToken;
            private IRouteNode _current;

            public Enumerator(DefaultChannel channel, CancellationToken cancellationToken)
            {
                _channel = channel ?? throw new ArgumentNullException(nameof(channel));
                _cancellationToken = cancellationToken;
                _current = _channel._graph.Root;
            }

            public ValueTask DisposeAsync() 
                => new ValueTask();

            public async ValueTask<bool> MoveNextAsync()
            {
                if (_current == null || _cancellationToken.IsCancellationRequested)
                {
                    Current = null;
                    return false;
                }

                Current = await _channel.CreateProcessor(_current.Statement);
                _current = _current.Children.First();
                return false;
            }

            public IProcessor Current { get; private set; }
        }
    }
}
