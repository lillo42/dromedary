using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dromedary.Commands
{
    public  class DefaultAsyncCommand : ICommand
    {
        private readonly Func<IDromedaryComponent, Task> _configure;
        private readonly Func<IDromedaryComponent, IExchange, Task> _configureWithExchange;
        
        public DefaultAsyncCommand(Type componentType, Func<IDromedaryComponent, Task> configure)
        {
            ComponentType = componentType ?? throw new ArgumentNullException(nameof(componentType));
            _configure = configure ?? throw new ArgumentNullException(nameof(configure));
            _configureWithExchange = null;
        }
        
        public DefaultAsyncCommand(Type componentType, Func<IDromedaryComponent, IExchange, Task> configure)
        {
            ComponentType = componentType ?? throw new ArgumentNullException(nameof(componentType));
            _configureWithExchange = configure ?? throw new ArgumentNullException(nameof(configure));
            _configure = null;
        } 

        public Type ComponentType { get; }

        public ValueTask<bool> CanExecuteAsync(IExchange exchange, IDromedaryComponent component, CancellationToken cancellationToken = default)
            => new ValueTask<bool>(true);

        public ValueTask ExecuteAsync(IExchange exchange, IDromedaryComponent component, CancellationToken cancellationToken = default)
        {
            if (_configure != null)
            {
                return new ValueTask(_configure.Invoke(component));
            }

            return new ValueTask(_configureWithExchange.Invoke(component, exchange));
        }
    }
    
    public  class DefaultAsyncCommand<T> : ICommand<T>
        where T : class, IDromedaryComponent
    {
        private readonly Func<T, Task> _configure;
        private readonly Func<T, IExchange, Task> _configureWithExchange;
        
        public DefaultAsyncCommand(Func<T, Task> configure)
        {
            _configure = configure ?? throw new ArgumentNullException(nameof(configure));
            _configureWithExchange = null;
        }
        
        public DefaultAsyncCommand(Func<T, IExchange, Task> configure)
        {
            _configureWithExchange = configure ?? throw new ArgumentNullException(nameof(configure));
            _configure = null;
        }

        public Type ComponentType => typeof(T);

        public ValueTask<bool> CanExecuteAsync(IExchange exchange, IDromedaryComponent component, CancellationToken cancellationToken = default) 
            => CanExecuteAsync(exchange, (T)component, cancellationToken);

        public ValueTask ExecuteAsync(IExchange exchange, IDromedaryComponent component,
            CancellationToken cancellationToken = default) 
            => ExecuteAsync(exchange, (T)component, cancellationToken);

        public ValueTask<bool> CanExecuteAsync(IExchange exchange, T component, CancellationToken cancellationToken = default)
            => new ValueTask<bool>(true);

        public ValueTask ExecuteAsync(IExchange exchange, T component, CancellationToken cancellationToken = default)
        {
            if (_configure != null)
            {
                return new ValueTask(_configure.Invoke(component));
            }

            return new ValueTask(_configureWithExchange.Invoke(component, exchange));
        }
    }
}
