using System;
using System.Threading.Tasks;
using Dromedary.Commands;
using Dromedary.Factories;
using Dromedary.Statements;

namespace Dromedary.Builder
{
    public class DefaultRouteBuilder : IRouteBuilder
    {
        private readonly IDromedaryContext _context;
        private readonly ICommandFactory _commandFactory;
        private readonly IStatementFactory _statementFactory;
        private readonly IRouteGraphBuilder _graphBuilder;

        private string _id = Guid.NewGuid().ToString();
        private string _description;

        public DefaultRouteBuilder(IDromedaryContext context, 
            ICommandFactory commandFactory, 
            IStatementFactory statementFactory, 
            IRouteGraphBuilder graphBuilder)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
            _statementFactory = statementFactory ?? throw new ArgumentNullException(nameof(statementFactory));
            _graphBuilder = graphBuilder ?? throw new ArgumentNullException(nameof(graphBuilder));
        }

        #region Configure

        public IRouteBuilder SetId(string id)
        {
            _id = id;
            return this;
        }

        public IRouteBuilder SetDescription(string description)
        {
            _description = description;
            return this;
        }

        #endregion

        #region From

        public IRouteBuilder From(string uri)
        {
            throw new NotImplementedException();
        }

        public IRouteBuilder From<T>(Action<T> configure)
            where T : class, IDromedaryComponent
        {
            AddFrom(_commandFactory.CreateCommand(configure));
            return this;
        }

        public IRouteBuilder From<T>(Action<T, IExchange> configure) 
            where T : class, IDromedaryComponent
        {
            AddFrom(_commandFactory.CreateCommand(configure));
            return this;
        }

        public IRouteBuilder From(Action<IDromedaryComponent> configure, Type componentType)
        {
            AddFrom(_commandFactory.CreateCommand(configure, componentType));
            return this;
        }

        public IRouteBuilder From(Action<IDromedaryComponent, IExchange> configure, Type componentType)
        {
            AddFrom(_commandFactory.CreateCommand(configure, componentType));
            return this;
        }

        public IRouteBuilder From<T>(Func<T, Task> configure) 
            where T : class, IDromedaryComponent
        {
            AddFrom(_commandFactory.CreateCommand(configure));
            return this;
        }

        public IRouteBuilder From<T>(Func<T, IExchange, Task> configure) 
            where T : class, IDromedaryComponent
        {
            AddFrom(_commandFactory.CreateCommand(configure));
            return this;
        }

        public IRouteBuilder From(Func<IDromedaryComponent, Task> configure, Type componentType)
        {
            AddFrom(_commandFactory.CreateCommand(configure, componentType));
            return this;
        }

        public IRouteBuilder From(Func<IDromedaryComponent, IExchange, Task> configure, Type componentType)
        {
            AddFrom(_commandFactory.CreateCommand(configure, componentType));
            return this;
        }

        private void AddFrom(IConfigureComponent configureComponent) 
            => _graphBuilder.Add(_statementFactory.Create(configureComponent, Statement.From));

        #endregion

        #region To

        public IRouteBuilder To(string uri)
        {
            throw new NotImplementedException();
        }

        public IRouteBuilder To<T>(Action<T> configure) 
            where T : class, IDromedaryComponent
        {
            AddTo(_commandFactory.CreateCommand(configure));
            return this;
        }

        public IRouteBuilder To<T>(Action<T, IExchange> configure) 
            where T : class, IDromedaryComponent
        {
            AddTo(_commandFactory.CreateCommand(configure));
            return this;
        }

        public IRouteBuilder To(Action<IDromedaryComponent, IExchange> configure, Type componentType)
        {
            AddTo(_commandFactory.CreateCommand(configure, componentType));
            return this;
        }

        public IRouteBuilder To<T>(Func<T, Task> configure) 
            where T : class, IDromedaryComponent
        {
            AddTo(_commandFactory.CreateCommand(configure));
            return this;
        }

        public IRouteBuilder To<T>(Func<T, IExchange, Task> configure) 
            where T : class, IDromedaryComponent
        {
            AddTo(_commandFactory.CreateCommand(configure));
            return this;
        }

        public IRouteBuilder To(Func<IDromedaryComponent, IExchange, Task> configure, Type componentType)
        {
            AddTo(_commandFactory.CreateCommand(configure, componentType));
            return this;
        }
        
        private void AddTo(IConfigureComponent configureComponent)
        {
            var statement = _statementFactory.Create(configureComponent, Statement.To);
            _graphBuilder.Add(statement);
        }

        #endregion
        
        #region Build

        public IRoute Build() 
            => new DefaultRoute(_id, _description, _graphBuilder.Build(), _context);

        #endregion
    }
}
