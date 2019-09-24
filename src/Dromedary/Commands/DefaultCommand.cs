using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dromedary.Commands
{
    public  class DefaultCommand : ICommand
    {
        private readonly Action<IDromedaryComponent> _configure;
        private readonly Action<IDromedaryComponent, IExchange> _configureWithExchange;
        
        public DefaultCommand(Type componentType, Action<IDromedaryComponent> configure)
        {
            ComponentType = componentType ?? throw new ArgumentNullException(nameof(componentType));
            _configure = configure ?? throw new ArgumentNullException(nameof(configure));
            _configureWithExchange = null;
        }
        
        public DefaultCommand(Type componentType, Action<IDromedaryComponent, IExchange> configure)
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
                _configure.Invoke(component);    
            }
            else
            {
                _configureWithExchange.Invoke(component, exchange);
            }
            
            return new ValueTask();
        }
    }
    
    public  class DefaultCommand<T> : ICommand<T>
        where T : class, IDromedaryComponent
    {
        private readonly Action<T> _configure;
        private readonly Action<T, IExchange> _configureWithExchange;
        
        public DefaultCommand(Action<T> configure)
        {
            _configure = configure ?? throw new ArgumentNullException(nameof(configure));
            _configureWithExchange = null;
        }
        
        public DefaultCommand(Action<T, IExchange> configure)
        {
            _configureWithExchange = configure ?? throw new ArgumentNullException(nameof(configure));
            _configure = null;
        }

        public Type ComponentType => typeof(T);

        public ValueTask<bool> CanExecuteAsync(IExchange exchange, IDromedaryComponent component,
            CancellationToken cancellationToken = default) 
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
                _configure.Invoke(component);
            }
            else
            {
                _configureWithExchange.Invoke(component, exchange);
            }

            return new ValueTask();
        }
    }
}
