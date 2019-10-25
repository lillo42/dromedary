using System;
using System.Threading.Tasks;
using Dromedary.Statements;

namespace Dromedary.Factories
{
    public interface IStatementFactory
    {
        IStatement Create(Statement statement, Type component, Action<IDromedaryComponent> configureComponent);
        IStatement Create<T>(Statement statement, Action<T> configureComponent)
            where T : IDromedaryComponent;
        
        IStatement Create(Statement statement, Type component, Func<IDromedaryComponent, Task> configureComponent);
        
        IStatement Create<T>(Statement statement, Func<T, Task> configureComponent)
            where T : IDromedaryComponent;
    }
}

