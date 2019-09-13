using System;

namespace Dromedary.Activator
{
    public interface IActivator
    {
        object CreateInstance(IServiceProvider provider, Type type);
    }
}
