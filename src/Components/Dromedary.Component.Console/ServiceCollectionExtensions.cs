using System;
using Dromedary.Component.Console;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConsoleComponent(this IServiceCollection service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return service.AddComponent<ConsoleComponent>();
        }
    }
}
