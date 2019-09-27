using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dromedary.Builder
{
    public interface IDromedaryContextBuilder
    {
        IServiceCollection Service { get; }
        IDromedaryContextBuilder AddLogging(Action<ILoggingBuilder> configure);

        #region Set
        IDromedaryContextBuilder SetId(string id);
        IDromedaryContextBuilder SetName(string name);
        IDromedaryContextBuilder SetVersion(string version);
        #endregion
        

        #region Component

        IDromedaryContextBuilder AddComponent<TService>()
            where TService : class;

        IDromedaryContextBuilder AddComponent<TService>(ServiceLifetime lifetime)
            where TService : class;
        
        IDromedaryContextBuilder AddComponent<TService>(Func<IServiceProvider, TService> implement)
            where TService : class;
        IDromedaryContextBuilder AddComponent<TService>(Func<IServiceProvider, TService> implement, ServiceLifetime lifetime)
            where TService : class;
        
        IDromedaryContextBuilder AddComponent<TService, TImplementation>()
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService;
        
        IDromedaryContextBuilder AddComponent<TService, TImplementation>(ServiceLifetime lifetime)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService;
        
        IDromedaryContextBuilder AddComponent<TService, TImplementation>(TImplementation implementation)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService;

        IDromedaryContextBuilder AddComponent<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementation)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService;
        
        IDromedaryContextBuilder AddComponent<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementation, ServiceLifetime lifetime)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService;

        IDromedaryContextBuilder AddComponent(Type component);
        
        IDromedaryContextBuilder AddComponent(Type component, ServiceLifetime lifetime);
        
        IDromedaryContextBuilder AddComponent(Type component, Type implementation);
        
        IDromedaryContextBuilder AddComponent(Type component, Type implementation, ServiceLifetime lifetime);
        
        #endregion
        
        IDromedaryContext Build();
    }
}
