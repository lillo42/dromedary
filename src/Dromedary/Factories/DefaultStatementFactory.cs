using System;
using System.Threading.Tasks;
using Dromedary.Activator;
using Dromedary.Exceptions;
using Dromedary.Statements;

namespace Dromedary.Factories
{
    public class DefaultStatementFactory : IStatementFactory
    {
        private readonly IActivator _activator;

        public DefaultStatementFactory(IActivator provider)
        {
            _activator = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public IStatement Create(Statement statement, Type component, Action<IDromedaryComponent> configureComponent)
        {
            if (configureComponent == null)
            {
                throw new ArgumentNullException(nameof(configureComponent));
            }

            if (component.IsInstanceOfType(typeof(IDromedaryComponent)))
            {
                throw new DromedaryIsNotDromedaryComponent(component);
            }

            var dromedaryComponent = (IDromedaryComponent)_activator.CreateInstance(component);
            configureComponent(dromedaryComponent);
            return new DefaultStatement(dromedaryComponent.CreateEndpoint(), statement);
        }

        public IStatement Create<T>(Statement statement, Action<T> configureComponent) 
            where T : IDromedaryComponent
        {
            if (configureComponent == null)
            {
                throw new ArgumentNullException(nameof(configureComponent));
            }

            var dromedaryComponent = _activator.CreateInstance<T>();
            configureComponent(dromedaryComponent);
            return new DefaultStatement(dromedaryComponent.CreateEndpoint(), statement);
        }

        public IStatement Create(Statement statement, Type component, Func<IDromedaryComponent, Task> configureComponent)
        {
            if (configureComponent == null)
            {
                throw new ArgumentNullException(nameof(configureComponent));
            }

            if (component.IsInstanceOfType(typeof(IDromedaryComponent)))
            {
                throw new DromedaryIsNotDromedaryComponent(component);
            }
            
            var dromedaryComponent = (IDromedaryComponent)_activator.CreateInstance(component);
            configureComponent(dromedaryComponent).GetAwaiter().GetResult();
            return new DefaultStatement(dromedaryComponent.CreateEndpoint(), statement);
        }

        public IStatement Create<T>(Statement statement, Func<T, Task> configureComponent) 
            where T : IDromedaryComponent
        {
            if (configureComponent == null)
            {
                throw new ArgumentNullException(nameof(configureComponent));
            }

            var dromedaryComponent = _activator.CreateInstance<T>();
            configureComponent(dromedaryComponent).GetAwaiter().GetResult();
            return new DefaultStatement(dromedaryComponent.CreateEndpoint(), statement);
        }
    }
}
