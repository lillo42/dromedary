using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Dromedary
{
    public interface IDromedaryBuilder
    {
        IServiceCollection Service { get; }
        IDromedaryBuilder AddLogging(Action<ILoggingBuilder> configure);

        #region Component

        IDromedaryBuilder AddComponent<TService>()
            where TService : class;

        IDromedaryBuilder AddComponent<TService>(ServiceLifetime lifetime)
            where TService : class;
        
        IDromedaryBuilder AddComponent<TService>(Func<IServiceProvider, TService> implement)
            where TService : class;
        IDromedaryBuilder AddComponent<TService>(Func<IServiceProvider, TService> implement, ServiceLifetime lifetime)
            where TService : class;
        
        IDromedaryBuilder AddComponent<TService, TImplementation>()
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService;
        
        IDromedaryBuilder AddComponent<TService, TImplementation>(ServiceLifetime lifetime)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService;
        
        IDromedaryBuilder AddComponent<TService, TImplementation>(TImplementation implementation)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService;

        IDromedaryBuilder AddComponent<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementation)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService;
        
        IDromedaryBuilder AddComponent<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementation, ServiceLifetime lifetime)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService;

        IDromedaryBuilder AddComponent(Type component);
        
        IDromedaryBuilder AddComponent(Type component, ServiceLifetime lifetime);
        
        IDromedaryBuilder AddComponent(Type component, Type implementation);
        
        IDromedaryBuilder AddComponent(Type component, Type implementation, ServiceLifetime lifetime);
        
        #endregion
        
        IDromedaryContext Build();
    }
}
