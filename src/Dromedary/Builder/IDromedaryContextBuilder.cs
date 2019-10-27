using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Dromedary.Builder
{
    public interface IDromedaryContextBuilder
    {
        IServiceCollection Service { get; }
        IEnumerable<Action<IRouteBuilder>> RoutesBuilder { get; }

        #region Set
        IDromedaryContextBuilder SetId(string id);
        IDromedaryContextBuilder SetName(string name);
        IDromedaryContextBuilder SetVersion(string version);
        #endregion
        
        #region Component

        IDromedaryContextBuilder AddComponent<TService>()
            where TService : class
        {
            Service.AddTransient<TService>();
            return this;
        }

        IDromedaryContextBuilder AddComponent<TService>(ServiceLifetime lifetime)
        {
            Service.Add(new ServiceDescriptor(typeof(TService), typeof(TService), lifetime));
            return this;
        }
        
        IDromedaryContextBuilder AddComponent<TService>(Func<IServiceProvider, TService> implement)
            where TService : class
        {
            Service.AddTransient(implement);
            return this;
        }
        
        IDromedaryContextBuilder AddComponent<TService>(Func<IServiceProvider, TService> implement, ServiceLifetime lifetime)
            where TService : class
        {
            Service.Add(new ServiceDescriptor(typeof(TService), implement, lifetime));
            return this;
        }

        IDromedaryContextBuilder AddComponent<TService, TImplementation>()
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService
        {
            Service.AddTransient<TService, TImplementation>();
            return this;
        }

        IDromedaryContextBuilder AddComponent<TService, TImplementation>(ServiceLifetime lifetime)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService
        {
            Service.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime));
            return this;
        }
        
        IDromedaryContextBuilder AddComponent<TService, TImplementation>(TImplementation implementation)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService
        {
            Service.Add(new ServiceDescriptor(typeof(TService), implementation));
            return this;
        }

        IDromedaryContextBuilder AddComponent<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementation)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService
        {
            Service.AddTransient<TService, TImplementation>(implementation);
            return this;
        }
        
        IDromedaryContextBuilder AddComponent<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementation, ServiceLifetime lifetime)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService
        {
            Service.Add(new ServiceDescriptor(typeof(TService), implementation));
            return this;
        }

        IDromedaryContextBuilder AddComponent(Type component)
        {
            Service.Add(new ServiceDescriptor(component, component));
            return this;
        }

        IDromedaryContextBuilder AddComponent(Type component, ServiceLifetime lifetime)
        {
            Service.Add(new ServiceDescriptor(component, component, lifetime));
            return this;
        }
        
        IDromedaryContextBuilder AddComponent(Type component, Type implementation)
        {
            Service.Add(new ServiceDescriptor(component, implementation));
            return this;
        }
        
        IDromedaryContextBuilder AddComponent(Type component, Type implementation, ServiceLifetime lifetime)
        {
            Service.Add(new ServiceDescriptor(component, implementation, lifetime));
            return this;
        }
        
        #endregion

        IDromedaryContextBuilder AddRoute(Action<IRouteBuilder> builder);
        
        IDromedaryContext Build();
        
        IDromedaryContext Build(IServiceProvider provider);
    }
}
