using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Dromedary
{
    public class RouteHostedService : BackgroundService
    {
        private readonly IServiceProvider _provider;
        private readonly IRoute _route;

        public RouteHostedService(IServiceProvider provider, IRoute route)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _route = route ?? throw new ArgumentNullException(nameof(route));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _route.ExecuteAsync(_provider, stoppingToken);
        }
    }
}
