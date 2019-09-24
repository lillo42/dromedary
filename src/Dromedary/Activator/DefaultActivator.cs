using System;
using Microsoft.Extensions.DependencyInjection;

namespace Dromedary.Activator
{
    public class DefaultActivator : IActivator
    {
        public T CreateInstance<T>(IServiceProvider provider)
        {
            var service = provider.GetService<T>();
            if (service == null)
            {
                var factory = ActivatorUtilities.CreateFactory(typeof(T), Type.EmptyTypes);
                service = (T)factory(provider, Array.Empty<object>());
            }

            return service;
        }

        public object CreateInstance(IServiceProvider provider, Type type)
        {
            var service = provider.GetService(type);
            if (service == null)
            {
                var factory = ActivatorUtilities.CreateFactory(type, Type.EmptyTypes);
                service = factory(provider, Array.Empty<object>());
            }

            return service;
        }
    }
}
