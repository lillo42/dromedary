using System;
using System.Collections.Generic;
using Dromedary.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dromedary
{
    public class DefaultDromedaryContext : IDromedaryContext
    {
        private readonly List<IRoute> _routes = new List<IRoute>();
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
        public IReadOnlyCollection<IRoute> Routes => _routes;
        public IServiceProvider Service { get; }
        
        public void AddRoute(Action<IRouteBuilder> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            using var scope = Service.CreateScope();
            var set = scope.ServiceProvider.GetRequiredService<IResolverDromedaryContext>();
            set.Context = this;
            
            var routeBuilder = scope.ServiceProvider.GetRequiredService<IRouteBuilder>();
            builder(routeBuilder);
                
            _routes.Add(routeBuilder.Build());
        }
    }
}
