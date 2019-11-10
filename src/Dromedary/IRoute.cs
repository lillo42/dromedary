using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dromedary
{
    public interface IRoute : IDisposable
    {
        string Id { get; }
        string? Description { get; }
        bool AllowSynchronousContinuations { get; }
        IRouteGraph RouteGraph { get; }
        Task ExecuteAsync(IServiceProvider service, CancellationToken cancellationToken = default);
    }
}
