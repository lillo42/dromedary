using System;
using System.Threading.Tasks;

namespace Dromedary.Statements
{
    public class DefaultStatement : IStatement
    {
        public DefaultStatement(Statement statement, Type component, Action<IDromedaryComponent> configureComponent)
        {
            Component = component ?? throw new ArgumentNullException(nameof(component));
            Statement = statement;
            ConfigureComponent = configureComponent ?? throw new ArgumentNullException(nameof(configureComponent));
        }

        public DefaultStatement(Statement statement, Type component, Func<IDromedaryComponent, Task> configureComponent)
        {
            Component = component ?? throw new ArgumentNullException(nameof(component));
            Statement = statement;
            ConfigureComponentAsync = configureComponent ?? throw new ArgumentNullException(nameof(configureComponent));
        }
        
        public Statement Statement { get; }
        public Type Component { get; }
        public Action<IDromedaryComponent> ConfigureComponent { get; }
        public Func<IDromedaryComponent, Task> ConfigureComponentAsync { get; }
    }

    public class DefaultStatement<T> : IStatement<T>
        where T : IDromedaryComponent
    {
        private readonly Action<IDromedaryComponent> _configureComponent;
        private readonly Func<IDromedaryComponent, Task> _configureComponentAsync;
        public DefaultStatement(Statement statement, Action<T> configureComponent)
        {
            Statement = statement;
            ConfigureComponent = configureComponent ?? throw new ArgumentNullException(nameof(configureComponent));
            _configureComponent = c => ConfigureComponent((T)c);
        }

        public DefaultStatement(Statement statement, Func<T, Task> configureComponent)
        {
            Statement = statement;
            ConfigureComponentAsync = configureComponent ?? throw new ArgumentNullException(nameof(configureComponent));
            _configureComponentAsync = c => ConfigureComponentAsync((T)c);
        }
        
        public Statement Statement { get; }
        public Type Component { get; } = typeof(T);
        
        public Action<T> ConfigureComponent { get; }

        public Func<T, Task> ConfigureComponentAsync { get; }

        Action<IDromedaryComponent> IStatement.ConfigureComponent => _configureComponent;

        Func<IDromedaryComponent, Task> IStatement.ConfigureComponentAsync => _configureComponentAsync;
    }
}
