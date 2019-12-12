using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dromedary.Statements;

namespace Dromedary
{
    public class DefaultChannel : IChannel
    {
        private readonly IReadOutput _output;
        private readonly IServiceProvider _service;
        private readonly IRouteGraph _graph;

        public DefaultChannel(IRouteGraph graph, IServiceProvider service, IReadOutput output)
        {
            _graph = graph ?? throw new ArgumentNullException(nameof(graph));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }
        
        public IAsyncEnumerator<IProcessor> GetAsyncEnumerator(CancellationToken cancellationToken = default) 
            => new Enumerator(this, cancellationToken, _output);

        private IProcessor CreateProcessor(IStatement statement)
        {
            return statement.Endpoint.CreateConsumer()
                .CreateProcessor(_service);
        }
        
        private class Enumerator : IAsyncEnumerator<IProcessor>
        {
            private readonly DefaultChannel _channel;
            private readonly CancellationToken _cancellationToken;
            private readonly IReadOutput _output;
            private IRouteNode _current;

            private bool _isFirst = false;

            public Enumerator(DefaultChannel channel, CancellationToken cancellationToken, IReadOutput output)
            {
                _channel = channel ?? throw new ArgumentNullException(nameof(channel));
                _cancellationToken = cancellationToken;
                _output = output;
                _current = _channel._graph.Root;
            }

            public ValueTask DisposeAsync() 
                => new ValueTask();

            public ValueTask<bool> MoveNextAsync()
            {
                _current = NextNode(_current);
                
                if (_current == null || _cancellationToken.IsCancellationRequested)
                {
                    Current = null;
                    return new ValueTask<bool>(false);
                }
                
                Current =  _channel.CreateProcessor(_current.Statement);
                return new ValueTask<bool>(true);
            }

            private IRouteNode NextNode(IRouteNode current)
            {
                if (_isFirst)
                {
                    _isFirst = false;
                    return _channel._graph.Root;
                }

                switch (current.Statement.Statement)
                {
                    case Statement.Filter:
                        if (_output.Output is bool filter && filter)
                        {
                            goto default;
                        }

                        return null;
                    default:
                        return _current.Children.FirstOrDefault();
                }
            }

            public IProcessor Current { get; private set; }
        }
    }
}
