using System;
using System.Threading.Tasks;
using Dromedary.Exceptions;
using Dromedary.Statements;

namespace Dromedary.Factories
{
    public class DefaultStatementFactory : IStatementFactory
    {
        public IStatement Create(Statement statement, Type component, Action<IDromedaryComponent> configureComponent)
        {
            if (component.IsInstanceOfType(typeof(IDromedaryComponent)))
            {
                throw new DromedaryIsNotDromedaryComponent(component);
            }
            
            return new DefaultStatement(statement, component, configureComponent);
        }

        public IStatement Create<T>(Statement statement, Action<T> configureComponent) 
            where T : IDromedaryComponent 
            => new DefaultStatement<T>(statement, configureComponent);

        public IStatement Create(Statement statement, Type component, Func<IDromedaryComponent, Task> configureComponent)
        {
            if (component.IsInstanceOfType(typeof(IDromedaryComponent)))
            {
                throw new DromedaryIsNotDromedaryComponent(component);
            }
            
            return new DefaultStatement(statement, component, configureComponent);
        }

        public IStatement Create<T>(Statement statement, Func<T, Task> configureComponent) 
            where T : IDromedaryComponent
            => new DefaultStatement<T>(statement, configureComponent);
    }
}
