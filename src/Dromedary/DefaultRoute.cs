using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Dromedary
{
    public class DefaultRoute : IRoute
    {
        public DefaultRoute(string id, string description, IRouteGraph routeGraph, IDromedaryContext context)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Description = description;
            RouteGraph = routeGraph ?? throw new ArgumentNullException(nameof(routeGraph));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string Id { get; }
        public string Description { get; }
        public IRouteGraph RouteGraph { get; }
        public IDromedaryContext Context { get; }
        
        public async Task ExecuteAsync(IServiceProvider service, CancellationToken cancellationToken = default)
        {
            var @from = RouteGraph.Root.First();
            var command = from.Statement.Command;

            var component = (IDromedaryComponent)service.GetRequiredService(command.ComponentType);

            if (await command.CanExecuteAsync(null, component, cancellationToken)
                .ConfigureAwait(false))
            {
                await command.ExecuteAsync(null, component, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        public void Dispose()
        {
        
        }
    }
}
