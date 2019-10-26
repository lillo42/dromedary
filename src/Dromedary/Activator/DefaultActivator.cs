using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace Dromedary.Activator
{
    public class DefaultActivator : IActivator
    {
        private static readonly ConcurrentDictionary<Type, ObjectFactory> s_factories = new ConcurrentDictionary<Type, ObjectFactory>();
        private readonly IServiceProvider _provider;

        public DefaultActivator(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }


        public T CreateInstance<T>()
        {
            var service = _provider.GetService<T>();
            if (service == null)
            {
                var factory = s_factories.GetOrAdd(typeof(T),
                    t => ActivatorUtilities.CreateFactory(t, Type.EmptyTypes));
                service = (T)factory(_provider, Array.Empty<object>());
            }

            return service;
        }

        public object CreateInstance(Type type)
        {
            var service = _provider.GetService(type);
            if (service == null)
            {
                var factory = s_factories.GetOrAdd(type,
                    t => ActivatorUtilities.CreateFactory(t, Type.EmptyTypes));
                service = factory(_provider, Array.Empty<object>());
            }

            return service;
        }
    }
}
