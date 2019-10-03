using System;

namespace Dromedary.Factories
{
    public class DefaultChannelFactory : IChannelFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IExchange _exchange;

        public DefaultChannelFactory(IExchange exchange, IServiceProvider serviceProvider)
        {
            _exchange = exchange ?? throw new ArgumentNullException(nameof(exchange));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IChannel Create(IRouteGraph graph) 
            => new DefaultChannel(graph, _exchange, _serviceProvider);
    }
}
