using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dromedary.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dromedary
{
    public class DefaultDromedaryContext :  BackgroundService, IDromedaryContext
    {
        private readonly ICollection<IRoute> _routes = new List<IRoute>();
        public DefaultDromedaryContext(string id, 
            string name, 
            string version, 
            IServiceProvider service)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name;
            Version = version;
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public string Id { get; }
        public string Name { get; }
        public string Version { get; }
        public DateTime UpTime { get; } = DateTime.UtcNow;
        public IEnumerable<IRoute> Routes => _routes;
        public IServiceProvider Service { get; }

        public void AddRoute(Action<IRouteBuilder> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            using var scope = Service.CreateScope();
            
            var routeBuilder = scope.ServiceProvider.GetRequiredService<IRouteBuilder>();
            builder(routeBuilder);
                
            _routes.Add(routeBuilder.Build());
        }

        Task IDromedaryContext.ExecuteAsync(CancellationToken stoppingToken) 
            => ExecuteAsync(stoppingToken);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var tasks = new Task[_routes.Count];
            int i = 0;
            foreach (var route in _routes)
            {
                tasks[i++] = ExecuteRouteAsync(Service, route, stoppingToken);
            }

            await Task.WhenAll(tasks);
        }

        private static async Task ExecuteRouteAsync(IServiceProvider provider, IRoute route, CancellationToken stopCancellationToken)
        {
            using var scope = provider.CreateScope();
            await route.ExecuteAsync(scope.ServiceProvider, stopCancellationToken)
                .ConfigureAwait(false);
        }
    }
}
