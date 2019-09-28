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
                service.TryAddSingleton(provider => builder.Build(provider));

                service.AddHostedService(provider => provider.GetRequiredService<IDromedaryContext>());
            });
        }
    }
}
