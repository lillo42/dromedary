using System;
using Dromedary;
using Dromedary.Activator;
using Dromedary.Builder;
using Dromedary.Components.Log;
using Dromedary.Components.Process;
using Dromedary.Factories;
using Dromedary.Generator;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDromedary(this IServiceCollection service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            service.TryAddScoped<IActivator, DefaultActivator>();
            service.TryAddSingleton<IExchangeIdGenerator, DefaultIdGenerator>();
            service.TryAddSingleton<IMessageIdGenerator, DefaultIdGenerator>();

            service.TryAddSingleton<IMessageFactory, DefaultMessageFactory>();
            service.TryAddSingleton<IExchangeFactory, DefaultExchangeFactory>();
            service.TryAddSingleton<IStatementFactory, DefaultStatementFactory>();
            service.TryAddSingleton<IRouteNodeFactory, DefaultRouteNodeFactory>();
            service.TryAddTransient<IRouteGraphBuilder, DefaultRouteGraphBuilder>();
            
            service.TryAddScoped<IChannelFactory, DefaultChannelFactory>();
            service.TryAddScoped<IExchangeResolver, DefaultExchangeResolver>();
            service.TryAddScoped(provider => provider.GetRequiredService<IExchangeResolver>().Exchange);
            
            service.TryAddTransient<IProcessComponent, ProcessComponent>();
            service.TryAddTransient<ILoggerDromedaryComponent, LogComponent>();
            service.TryAddTransient<IRouteBuilder, DefaultRouteBuilder>();

            return service;
        }

        #region Route
        public static IServiceCollection AddRoute(this IServiceCollection service, Action<IRouteBuilder> routeBuilder)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (routeBuilder == null)
            {
                throw new ArgumentNullException(nameof(routeBuilder));
            }

            service.AddHostedService(provider =>
            {
                var builder = provider.GetRequiredService<IRouteBuilder>();
                routeBuilder(builder);
                return new RouteHostedService(provider,builder.Build());
            });
            
            return service;
        }
        
        public static IServiceCollection AddRoute(this IServiceCollection service, IRoute route)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (route == null)
            {
                throw new ArgumentNullException(nameof(route));
            }

            service.AddHostedService(provider => new RouteHostedService(provider,route));
            
            return service;
        }

        #endregion

        #region Component

        #region Service
        public static IServiceCollection AddComponent<TService>(this IServiceCollection service)
            where TService : class, IDromedaryComponent
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return service.AddTransient<TService>();
        }

        public static IServiceCollection AddComponent<TService>(this IServiceCollection service, ServiceLifetime lifetime)
            where TService : class, IDromedaryComponent
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            service.Add(new ServiceDescriptor(typeof(TService), typeof(TService), lifetime));
            return service;
        }

        public static IServiceCollection AddComponent<TService>(this IServiceCollection service, 
            Func<IServiceProvider, TService> implement)
            where TService : class, IDromedaryComponent
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implement == null)
            {
                throw new ArgumentNullException(nameof(implement));
            }

            return service.AddTransient(implement);
        }
        
        
        public static IServiceCollection AddComponent<TService>(this IServiceCollection service, 
            Func<IServiceProvider, TService> implement, 
            ServiceLifetime lifetime)
            where TService : class, IDromedaryComponent
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implement == null)
            {
                throw new ArgumentNullException(nameof(implement));
            }

            service.Add(new ServiceDescriptor(typeof(TService), implement, lifetime));
            
            return service;
        }
        
        #endregion

        #region Service & Implementation
        public static IServiceCollection AddComponent<TService, TImplementation>(this IServiceCollection service)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            service.AddTransient<TService, TImplementation>();
            return service;
        }
        
        public static IServiceCollection AddComponent<TService, TImplementation>(this IServiceCollection service,
            ServiceLifetime lifetime)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            service.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime));
            return service;
        }
        
        public static IServiceCollection AddComponent<TService, TImplementation>(this IServiceCollection service,
            TImplementation implementation)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementation == null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            service.Add(new ServiceDescriptor(typeof(TService), implementation));
            return service;
        }
        
        public static IServiceCollection AddComponent<TService, TImplementation>(this IServiceCollection service,
            Func<IServiceProvider, TImplementation> implementation)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementation == null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            service.AddTransient<TService, TImplementation>(implementation);
            return service;
        }
        
        public static IServiceCollection AddComponent<TService, TImplementation>(this IServiceCollection service,
            Func<IServiceProvider, TImplementation> implementation,
            ServiceLifetime lifetime)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementation == null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            service.Add(new ServiceDescriptor(typeof(TService), implementation, lifetime));
            return service;
        }
        #endregion

        #region Type

        public static IServiceCollection AddComponent(this IServiceCollection service, Type component)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            service.Add(new ServiceDescriptor(component, component));
            return service;
        }
        
        public static IServiceCollection AddComponent(this IServiceCollection service, Type component, 
            ServiceLifetime lifetime)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            service.Add(new ServiceDescriptor(component, component, lifetime));
            return service;
        }
        
        public static IServiceCollection AddComponent(this IServiceCollection service, 
            Type component,
            Type implementation)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            if (implementation == null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            service.Add(new ServiceDescriptor(component, implementation));
            return service;
        }
        
        public static IServiceCollection AddComponent(this IServiceCollection service, 
            Type component,
            Type implementation,
            ServiceLifetime lifetime)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            if (implementation == null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            service.Add(new ServiceDescriptor(component, implementation, lifetime));
            return service;
        }

        #endregion
        
        #endregion
    }
}
