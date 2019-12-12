using System;
using Microsoft.Extensions.DependencyInjection;

namespace Dromedary.Factories
{
    public class DefaultChannelFactory : IChannelFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultChannelFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IChannel Create(IRouteGraph graph) 
            => new DefaultChannel(graph, _serviceProvider, _serviceProvider.GetRequiredService<IReadOutput>());
    }
}
