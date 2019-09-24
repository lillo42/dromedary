using System;
using Dromedary.Activator;
using Dromedary.Builder;
using Dromedary.Factories;
using Dromedary.IdGenerator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Dromedary
{
    public class DromedaryBuilder : IDromedaryBuilder
    {
        private readonly IServiceCollection _service;

        public DromedaryBuilder()
            : this(new ServiceCollection())
        {
            
        }

        public DromedaryBuilder(IServiceCollection service)
        {
            _service = service;
            
            _service.TryAddSingleton<IActivator, DefaultActivator>();
            _service.TryAddSingleton<IExchangeIdGenerator, DefaultIdGenerator>();
            _service.TryAddSingleton<IMessageIdGenerator, DefaultIdGenerator>();
            
            _service.TryAddSingleton<IMessageFactory, DefaultMessageFactory>();
            _service.TryAddSingleton<IExchangeFactory, DefaultExchangeFactory>();
            _service.TryAddSingleton<INavigateFactory, DefaultNavigateFactory>();
            _service.TryAddSingleton<ICommandFactory, DefaultCommandFactory>();
            
            _service.TryAddTransient<IRouteBuilder, DefaultRouteBuilder>();
        }

        IServiceCollection IDromedaryBuilder.Service => _service;

        IDromedaryBuilder IDromedaryBuilder.AddLogging(Action<ILoggingBuilder> configure)
        {
            var builder = new Logging.LoggingBuilder(_service);
            throw new NotImplementedException();
        }

        #region Component

        IDromedaryBuilder IDromedaryBuilder.AddComponent<TService>()
        {
            _service.AddTransient<TService>();
            return this;
        }

        IDromedaryBuilder IDromedaryBuilder.AddComponent<TService>(ServiceLifetime lifetime)
        {
            _service.Add(new ServiceDescriptor(typeof(TService), typeof(TService), lifetime));
            return this;
        }

        IDromedaryBuilder IDromedaryBuilder.AddComponent<TService>(Func<IServiceProvider, TService> implement)
        {
            _service.AddTransient(implement);
            return this;
        }

        IDromedaryBuilder IDromedaryBuilder.AddComponent<TService>(Func<IServiceProvider, TService> implement, ServiceLifetime lifetime)
        {
            _service.Add(new ServiceDescriptor(typeof(TService), implement, lifetime));
            return this;
        }

        IDromedaryBuilder IDromedaryBuilder.AddComponent<TService, TImplementation>()
        {
            _service.AddTransient<TService, TImplementation>();
            return this;
        }

        IDromedaryBuilder IDromedaryBuilder.AddComponent<TService, TImplementation>(ServiceLifetime lifetime)
        {
            _service.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime));
            return this;
        }

        IDromedaryBuilder IDromedaryBuilder.AddComponent<TService, TImplementation>(TImplementation implementation)
        {
            _service.Add(new ServiceDescriptor(typeof(TService), implementation));
            return this;
        }

        IDromedaryBuilder IDromedaryBuilder.AddComponent<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementation)
        {
            _service.AddTransient<TService, TImplementation>(implementation);
            return this;
        }

        IDromedaryBuilder IDromedaryBuilder.AddComponent<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementation, ServiceLifetime lifetime)
        {
            _service.Add(new ServiceDescriptor(typeof(TService), implementation, lifetime));
            return this;
        }

        IDromedaryBuilder IDromedaryBuilder.AddComponent(Type component)
        {
            _service.Add(new ServiceDescriptor(component, component));
            return this;
        }

        IDromedaryBuilder IDromedaryBuilder.AddComponent(Type component, ServiceLifetime lifetime)
        {
            _service.Add(new ServiceDescriptor(component, component, lifetime));
            return this;
        }

        IDromedaryBuilder IDromedaryBuilder.AddComponent(Type component, Type implementation)
        {
            _service.Add(new ServiceDescriptor(component, implementation));
            return this;
        }

        IDromedaryBuilder IDromedaryBuilder.AddComponent(Type component, Type implementation, ServiceLifetime lifetime)
        {
            _service.Add(new ServiceDescriptor(component, implementation, lifetime));
            return this;
        }

        #endregion
        

        IDromedaryContext IDromedaryBuilder.Build()
        {
            throw new NotImplementedException();
        }
    }
}
