using System;

namespace Dromedary.Activator
{
    public interface IActivator
    {
        T CreateInstance<T>(IServiceProvider provider);
        object CreateInstance(IServiceProvider provider, Type type);
    }
}
