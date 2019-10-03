using System;
using System.Collections.Generic;
using Dromedary.Activator;
using Dromedary.Factories;
using Dromedary.Generator;
using Dromedary.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Dromedary.Builder
{
    public class DefaultDromedaryContextBuilder : IDromedaryContextBuilder
    {
        private readonly ILoggingBuilder _builder;
        private readonly ICollection<Action<IRouteBuilder>> _routes = new List<Action<IRouteBuilder>>();

        private string _id = Guid.NewGuid().ToString();
        private string _name;
        private string _version = "1.0.0";

        public DefaultDromedaryContextBuilder()
            : this(new ServiceCollection())
        {
            
        }

        public DefaultDromedaryContextBuilder(IServiceCollection service)
        {
            Service = service;
            _builder = new LoggingBuilder(service);
            
            Service.TryAddSingleton<IActivator, DefaultActivator>();
            Service.TryAddSingleton<IExchangeIdGenerator, DefaultIdGenerator>();
            Service.TryAddSingleton<IMessageIdGenerator, DefaultIdGenerator>();

            Service.TryAddSingleton<IMessageFactory, DefaultMessageFactory>();
            Service.TryAddSingleton<IExchangeFactory, DefaultExchangeFactory>();
            Service.TryAddSingleton<ICommandFactory, DefaultCommandFactory>();
            Service.TryAddSingleton<IStatementFactory, DefaultStatementFactory>();
            Service.TryAddSingleton<IRouteNodeFactory, DefaultRouteNodeFactory>();
            Service.TryAddTransient<IRouteGraphBuilder, DefaultRouteGraphBuilder>();
            
            Service.TryAddScoped<IChannelFactory, DefaultChannelFactory>();
            
            
            Service.TryAddTransient<IRouteBuilder, DefaultRouteBuilder>();
        }

        public IServiceCollection Service { get; }
        
        #region Set

        public IDromedaryContextBuilder SetId(string id)
        {
            _id = id ?? throw new ArgumentNullException(nameof(id));
            return this;
        }

        public IDromedaryContextBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public IDromedaryContextBuilder SetVersion(string version)
        {
            _version = version ?? throw new ArgumentNullException(nameof(version));;
            return this;
        }
        #endregion

        #region Component

        public IDromedaryContextBuilder AddComponent<TService>()
            where TService : class
        {
            Service.AddTransient<TService>();
            return this;
        }

        public IDromedaryContextBuilder AddComponent<TService>(ServiceLifetime lifetime)
            where TService : class
        {
            Service.Add(new ServiceDescriptor(typeof(TService), typeof(TService), lifetime));
            return this;
        }

        public IDromedaryContextBuilder AddComponent<TService>(Func<IServiceProvider, TService> implement)
            where TService : class
        {
            Service.AddTransient(implement);
            return this;
        }

        public IDromedaryContextBuilder AddComponent<TService>(Func<IServiceProvider, TService> implement, ServiceLifetime lifetime)
            where TService : class
        {
            Service.Add(new ServiceDescriptor(typeof(TService), implement, lifetime));
            return this;
        }

        public IDromedaryContextBuilder AddComponent<TService, TImplementation>()
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService
        {
            Service.AddTransient<TService, TImplementation>();
            return this;
        }

        public IDromedaryContextBuilder AddComponent<TService, TImplementation>(ServiceLifetime lifetime)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService
        {
            Service.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime));
            return this;
        }

        public IDromedaryContextBuilder AddComponent<TService, TImplementation>(TImplementation implementation)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService
        {
            Service.Add(new ServiceDescriptor(typeof(TService), implementation));
            return this;
        }

        public IDromedaryContextBuilder AddComponent<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementation)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService
        {
            Service.AddTransient<TService, TImplementation>(implementation);
            return this;
        }

        public IDromedaryContextBuilder AddComponent<TService, TImplementation>(Func<IServiceProvider, TImplementation> implementation, ServiceLifetime lifetime)
            where TService : class, IDromedaryComponent
            where TImplementation : class, TService
        {
            Service.Add(new ServiceDescriptor(typeof(TService), implementation, lifetime));
            return this;
        }

        public IDromedaryContextBuilder AddComponent(Type component)
        {
            Service.Add(new ServiceDescriptor(component, component));
            return this;
        }

        public IDromedaryContextBuilder AddComponent(Type component, ServiceLifetime lifetime)
        {
            Service.Add(new ServiceDescriptor(component, component, lifetime));
            return this;
        }

        public IDromedaryContextBuilder AddComponent(Type component, Type implementation)
        {
            Service.Add(new ServiceDescriptor(component, implementation));
            return this;
        }

        public IDromedaryContextBuilder AddComponent(Type component, Type implementation, ServiceLifetime lifetime)
        {
            Service.Add(new ServiceDescriptor(component, implementation, lifetime));
            return this;
        }

        public IDromedaryContextBuilder AddRoute(Action<IRouteBuilder> builder)
        {
            _routes.Add(builder);
            return this;
        }

        #endregion

        #region Builder

        public IDromedaryContext Build()
        {
            Service.TryAddSingleton(Build);
            return Service.BuildServiceProvider().GetRequiredService<IDromedaryContext>();
        }

        public IDromedaryContext Build(IServiceProvider provider)
        {
            var context =  new DefaultDromedaryContext(_id, _name, _version, provider);
            foreach (var route in _routes)
            {
                context.AddRoute(route);
            }

            return context;
        }

        #endregion
        
    }
}
