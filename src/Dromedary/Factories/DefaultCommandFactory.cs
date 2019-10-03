using System;
using System.Threading.Tasks;
using Dromedary.Commands;

namespace Dromedary.Factories
{
    public class DefaultCommandFactory : ICommandFactory
    {
        public IConfigureComponent CreateCommand(Action<IDromedaryComponent> configure, Type componentType) 
            => new DefaultConfigureComponent(componentType, configure);

        public IConfigureComponent CreateCommand(Action<IDromedaryComponent, IExchange> configure, Type componentType) 
            => new DefaultConfigureComponent(componentType, configure);

        public IConfigureComponent<T> CreateCommand<T>(Action<T> configure) 
            where T : class, IDromedaryComponent 
            => new DefaultConfigureComponent<T>(configure);

        public IConfigureComponent<T> CreateCommand<T>(Action<T, IExchange> configure) 
            where T : class, IDromedaryComponent 
            => new DefaultConfigureComponent<T>(configure);

        public IConfigureComponent CreateCommand(Func<IDromedaryComponent, Task> configure, Type componentType) 
            => new DefaultAsyncConfigureComponent(componentType, configure);

        public IConfigureComponent CreateCommand(Func<IDromedaryComponent, IExchange, Task> configure, Type componentType) 
            => new DefaultAsyncConfigureComponent(componentType, configure);

        public IConfigureComponent<T> CreateCommand<T>(Func<T, Task> configure) 
            where T : class, IDromedaryComponent 
            => new DefaultAsyncConfigureComponent<T>(configure);

        public IConfigureComponent<T> CreateCommand<T>(Func<T, IExchange, Task> configure) 
            where T : class, IDromedaryComponent 
            => new DefaultAsyncConfigureComponent<T>(configure);
    }
}
