using System;
using System.Threading.Tasks;
using Dromedary.Commands;

namespace Dromedary.Factories
{
    public class DefaultCommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(Action<IDromedaryComponent> configure, Type componentType) 
            => new DefaultCommand(componentType, configure);

        public ICommand CreateCommand(Action<IDromedaryComponent, IExchange> configure, Type componentType) 
            => new DefaultCommand(componentType, configure);

        public ICommand<T> CreateCommand<T>(Action<T> configure) 
            where T : class, IDromedaryComponent 
            => new DefaultCommand<T>(configure);

        public ICommand<T> CreateCommand<T>(Action<T, IExchange> configure) 
            where T : class, IDromedaryComponent 
            => new DefaultCommand<T>(configure);

        public ICommand CreateCommand(Func<IDromedaryComponent, Task> configure, Type componentType) 
            => new DefaultAsyncCommand(componentType, configure);

        public ICommand CreateCommand(Func<IDromedaryComponent, IExchange, Task> configure, Type componentType) 
            => new DefaultAsyncCommand(componentType, configure);

        public ICommand<T> CreateCommand<T>(Func<T, Task> configure) 
            where T : class, IDromedaryComponent 
            => new DefaultAsyncCommand<T>(configure);

        public ICommand<T> CreateCommand<T>(Func<T, IExchange, Task> configure) 
            where T : class, IDromedaryComponent 
            => new DefaultAsyncCommand<T>(configure);
    }
}
