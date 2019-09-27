using System;
using Dromedary.Activator;
using Dromedary.Factories;
using Dromedary.IdGenerator;
using Dromedary.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Dromedary.Builder
{
    public class DromedaryContextBuilder : IDromedaryContextBuilder
    {
        private readonly ILoggingBuilder _builder;
        private readonly IServiceCollection _service;
        
        private string _id = Guid.NewGuid().ToString();
        private string _name;
        private string _version = "1.0.0";

        public DromedaryContextBuilder()
            : this(new ServiceCollection())
        {
            
        }

        public DromedaryContextBuilder(IServiceCollection service)
        {
            _service = service;
            _builder = new LoggingBuilder(service);
            
            _service.TryAddSingleton<IActivator, DefaultActivator>();
            _service.TryAddSingleton<IExchangeIdGenerator, DefaultIdGenerator>();
            _service.TryAddSingleton<IMessageIdGenerator, DefaultIdGenerator>();

            _service.AddScoped<IResolverDromedaryContext, DefaultResolverDromedaryContext>();

            _service.AddScoped(ctx => ctx.GetRequiredService<IResolverDromedaryContext>().Context);
            
            _service.TryAddSingleton<IMessageFactory, DefaultMessageFactory>();
            _service.TryAddSingleton<IExchangeFactory, DefaultExchangeFactory>();
            _service.TryAddSingleton<ICommandFactory, DefaultCommandFactory>();
            _service.TryAddSingleton<IStatementFactory, DefaultStatementFactory>();
            _service.TryAddSingleton<IRouteNodeFactory, DefaultRouteNodeFactory>();
            _service.TryAddTransient<IRouteGraphBuilder, DefaultRouteGraphBuilder>();
            
            _service.TryAddTransient<IRouteBuilder, DefaultRouteBuilder>();
        }

        IServiceCollection IDromedaryContextBuilder.Service => _service;

        IDromedaryContextBuilder IDromedaryContextBuilder.AddLogging(Action<ILoggingBuilder> configure)
        {
            configure(_builder);
            return this;
        }
        
        #region Set

        IDromedaryContextBuilder IDromedaryContextBuilder.SetId(string id)
        {
            _id = id ?? throw new ArgumentNullException(nameof(id));
            return this;
        }

        IDromedaryContextBuilder IDromedaryContextBuilder.SetName(string name)
        {
            _name = name;
            return this;
        }

        IDromedaryContextBuilder IDromedaryContextBuilder.SetVersion(string version)
        {
            _version = version ?? throw new ArgumentNullException(nameof(version));;
            return this;
        }
        #endregion

        #region Component

        IDromedaryContextBuilder IDromedaryContextBuilder.AddComponent<TService>()
        {
            _service.AddTransient<TService>();
            return this;
        }

        IDromedaryContextBuilder IDromedaryContextBuilder.AddComponent<TService>(ServiceLifetime lifetime)
        {
            _service.Add(new ServiceDescriptor(typeof(TService), typeof(TService), lifetime));
            return this;
        }

        IDromedaryContextBuilder IDromedaryContextBuilder.AddComponent<TService>(Func<IServiceProvider, TService> implement)
        {
            _service.AddTransient(implement);
            return this;
        }

        IDromedaryContextBuilder IDromedaryContextBuilder.AddComponent<TService>(Func<IServiceProvider, TService> implement, ServiceLifetime lifetime)
        {
            _service.Add(new ServiceDescriptor(typeof(TService), implement, lifetime));
            return this;
        }

        IDromedaryContextBuilder IDromedaryContextBuilder.AddComponent<TService, TImplementation>()
        {
            _service.AddTransient<TService, TImplementation>();
            return this;
        }

        IDromedaryContextBuilder IDromedaryContextBuilder.AddComponent<TService, TImplementation>(ServiceLifetime lifetime)
        {
            _service.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime));
            return this;
        }

        IDromedaryContextBuilder IDromedaryContextBuilder.AddComponent<TService, TImplementation>(TImplementation implementation)
        {
            _service.Add(new ServiceDescriptor(typeof(TService), implementation));
            return this;
        }

        IDromedaryContextBuilder IDromedaryContextBuilder.AddComponent<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementation)
        {
            _service.AddTransient<TService, TImplementation>(implementation);
            return this;
        }

        IDromedaryContextBuilder IDromedaryContextBuilder.AddComponent<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementation, ServiceLifetime lifetime)
        {
            _service.Add(new ServiceDescriptor(typeof(TService), implementation, lifetime));
            return this;
        }

        IDromedaryContextBuilder IDromedaryContextBuilder.AddComponent(Type component)
        {
            _service.Add(new ServiceDescriptor(component, component));
            return this;
        }

        IDromedaryContextBuilder IDromedaryContextBuilder.AddComponent(Type component, ServiceLifetime lifetime)
        {
            _service.Add(new ServiceDescriptor(component, component, lifetime));
            return this;
        }

        IDromedaryContextBuilder IDromedaryContextBuilder.AddComponent(Type component, Type implementation)
        {
            _service.Add(new ServiceDescriptor(component, implementation));
            return this;
        }

        IDromedaryContextBuilder IDromedaryContextBuilder.AddComponent(Type component, Type implementation, ServiceLifetime lifetime)
        {
            _service.Add(new ServiceDescriptor(component, implementation, lifetime));
            return this;
        }

        #endregion
        
        IDromedaryContext IDromedaryContextBuilder.Build() 
            => new DefaultDromedaryContext(_id, _name, _version, _service.BuildServiceProvider());
    }
}
