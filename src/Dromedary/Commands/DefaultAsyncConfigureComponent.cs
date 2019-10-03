using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dromedary.Commands
{
    public  class DefaultAsyncConfigureComponent : IConfigureComponent
    {
        private readonly Func<IDromedaryComponent, Task> _configure;
        private readonly Func<IDromedaryComponent, IExchange, Task> _configureWithExchange;
        
        public DefaultAsyncConfigureComponent(Type componentType, Func<IDromedaryComponent, Task> configure)
        {
            ComponentType = componentType ?? throw new ArgumentNullException(nameof(componentType));
            _configure = configure ?? throw new ArgumentNullException(nameof(configure));
            _configureWithExchange = null;
        }
        
        public DefaultAsyncConfigureComponent(Type componentType, Func<IDromedaryComponent, IExchange, Task> configure)
        {
            ComponentType = componentType ?? throw new ArgumentNullException(nameof(componentType));
            _configureWithExchange = configure ?? throw new ArgumentNullException(nameof(configure));
            _configure = null;
        } 

        public Type ComponentType { get; }
        
        public void Configure(IExchange exchange, IDromedaryComponent component)
        {
            if (_configure != null)
            {
                _configure.Invoke(component).GetAwaiter().GetResult();
            }
            else
            {
                _configureWithExchange.Invoke(component, exchange).GetAwaiter().GetResult();
            }
        }
    }
    
    public  class DefaultAsyncConfigureComponent<T> : IConfigureComponent<T>
        where T : class, IDromedaryComponent
    {
        private readonly Func<T, Task> _configure;
        private readonly Func<T, IExchange, Task> _configureWithExchange;
        
        public DefaultAsyncConfigureComponent(Func<T, Task> configure)
        {
            _configure = configure ?? throw new ArgumentNullException(nameof(configure));
            _configureWithExchange = null;
        }
        
        public DefaultAsyncConfigureComponent(Func<T, IExchange, Task> configure)
        {
            _configureWithExchange = configure ?? throw new ArgumentNullException(nameof(configure));
            _configure = null;
        }

        public Type ComponentType => typeof(T);

        public void Configure(IExchange exchange, IDromedaryComponent component) 
            => Configure(exchange, (T)component);

        public void Configure(IExchange exchange, T component)
        {
            if (_configure != null)
            {
                _configure.Invoke(component)
                    .GetAwaiter()
                    .GetResult();
            }
            else
            {
                _configureWithExchange.Invoke(component, exchange)
                    .GetAwaiter()
                    .GetResult();
            }
        }
    }
}
