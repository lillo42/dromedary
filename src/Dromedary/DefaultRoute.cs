using System;
using System.Threading;
using System.Threading.Tasks;

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

        public ValueTask ExecuteAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
