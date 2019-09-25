using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dromedary
{
    public interface IRoute : IDisposable
    {
        string Id { get; }
        string Description { get; }
        IRouteGraph RouteGraph { get; }
        IDromedaryContext Context { get; }

        ValueTask ExecuteAsync(CancellationToken cancellationToken);
    }
}
