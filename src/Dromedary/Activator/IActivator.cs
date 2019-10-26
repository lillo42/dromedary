using System;

namespace Dromedary.Activator
{
    public interface IActivator
    {
        T CreateInstance<T>();
        object CreateInstance(Type type);
    }
}
