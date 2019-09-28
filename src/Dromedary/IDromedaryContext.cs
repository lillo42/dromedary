using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dromedary.Builder;
using Microsoft.Extensions.Hosting;

namespace Dromedary
{
    public interface IDromedaryContext : IHostedService
    {
        string Id { get; }
        string Name { get; }
        string Version { get; }
        DateTime UpTime { get; }
        IEnumerable<IRoute> Routes { get; }
        IServiceProvider Service { get; }

        Task ExecuteAsync(CancellationToken stoppingToken);
        void AddRoute(Action<IRouteBuilder> builder);
    }
}
