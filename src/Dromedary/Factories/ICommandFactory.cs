using System;
using System.Threading.Tasks;
using Dromedary.Commands;

namespace Dromedary.Factories
{
    public interface ICommandFactory
    {
        ICommand CreateCommand(Action<IDromedaryComponent> configure, Type componentType);
        ICommand CreateCommand(Action<IDromedaryComponent, IExchange> configure, Type componentType);
        
        ICommand<T> CreateCommand<T>(Action<T> configure)
            where T : class, IDromedaryComponent;
        ICommand<T> CreateCommand<T>(Action<T, IExchange> configure)
            where T : class, IDromedaryComponent;
        
        ICommand CreateCommand(Func<IDromedaryComponent, Task> configure, Type componentType);
        ICommand CreateCommand(Func<IDromedaryComponent, IExchange, Task> configure, Type componentType);
        
        ICommand<T> CreateCommand<T>(Func<T, Task> configure)
            where T : class, IDromedaryComponent;
        ICommand<T> CreateCommand<T>(Func<T, IExchange, Task> configure)
            where T : class, IDromedaryComponent;
    }
}
