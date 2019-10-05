using System;
using Dromedary;
using Dromedary.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.Hosting
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureDromedaryContext(this IHostBuilder hostBuilder, Action<IDromedaryContextBuilder> config)
        {
            return hostBuilder.ConfigureServices((host, service) =>
            {
                var builder = new DefaultDromedaryContextBuilder(service);
                config(builder);
                service.TryAddSingleton(provider =>  builder.Build(provider));
                service.AddHostedService(provider =>
                {
                    var context = provider.GetRequiredService<IDromedaryContext>();

                    foreach (var router in builder.RoutesBuilder)
                    {
                        context.AddRoute(router);
                    }

                    return context;
                });
            });
        }
    }
}
