using System;
using System.Threading.Tasks;
using Dromedary.Commands;

namespace Dromedary.Factories
{
    public interface ICommandFactory
    {
        IConfigureComponent CreateCommand(Action<IDromedaryComponent> configure, Type componentType);
        IConfigureComponent CreateCommand(Action<IDromedaryComponent, IExchange> configure, Type componentType);
        
        IConfigureComponent<T> CreateCommand<T>(Action<T> configure)
            where T : class, IDromedaryComponent;
        IConfigureComponent<T> CreateCommand<T>(Action<T, IExchange> configure)
            where T : class, IDromedaryComponent;
        
        IConfigureComponent CreateCommand(Func<IDromedaryComponent, Task> configure, Type componentType);
        IConfigureComponent CreateCommand(Func<IDromedaryComponent, IExchange, Task> configure, Type componentType);
        
        IConfigureComponent<T> CreateCommand<T>(Func<T, Task> configure)
            where T : class, IDromedaryComponent;
        IConfigureComponent<T> CreateCommand<T>(Func<T, IExchange, Task> configure)
            where T : class, IDromedaryComponent;
    }
}
