using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dromedary.Commands
{
    public  class DefaultConfigureComponent : IConfigureComponent
    {
        private readonly Action<IDromedaryComponent> _configure;
        private readonly Action<IDromedaryComponent, IExchange> _configureWithExchange;
        
        public DefaultConfigureComponent(Type componentType, Action<IDromedaryComponent> configure)
        {
            ComponentType = componentType ?? throw new ArgumentNullException(nameof(componentType));
            _configure = configure ?? throw new ArgumentNullException(nameof(configure));
            _configureWithExchange = null;
        }
        
        public DefaultConfigureComponent(Type componentType, Action<IDromedaryComponent, IExchange> configure)
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
                _configure.Invoke(component);
            }
            else
            {
                _configureWithExchange.Invoke(component, exchange);
            }
        }
    }
    
    public  class DefaultConfigureComponent<T> : IConfigureComponent<T>
        where T : class, IDromedaryComponent
    {
        private readonly Action<T> _configure;
        private readonly Action<T, IExchange> _configureWithExchange;
        
        public DefaultConfigureComponent(Action<T> configure)
        {
            _configure = configure ?? throw new ArgumentNullException(nameof(configure));
            _configureWithExchange = null;
        }
        
        public DefaultConfigureComponent(Action<T, IExchange> configure)
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
                _configure.Invoke(component);
            }
            else
            {
                _configureWithExchange.Invoke(component, exchange);
            }
        }
    }
}
