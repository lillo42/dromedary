using System;
using Dromedary;
using Dromedary.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.Hosting
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseDromedary(this IHostBuilder hostBuilder)
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            return hostBuilder.ConfigureServices((host, service) =>
            {
                service.AddDromedary();
            });
        }
        
        public static IHostBuilder AddRoute(this IHostBuilder hostBuilder, Action<IRouteBuilder> routeBuilder)
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

            if (routeBuilder == null)
            {
                throw new ArgumentNullException(nameof(routeBuilder));
            }
            
            return hostBuilder.ConfigureServices((host, service) =>
            {
                service.AddDromedary()
                    .AddRoute(routeBuilder);
            });
        }
    }
}
