using System;
using System.Threading.Tasks;
using Dromedary.Components.Logger;
using Dromedary.Components.Process;
using Dromedary.Factories;
using Dromedary.Statements;
using Microsoft.Extensions.Logging;

namespace Dromedary.Builder
{
    public class DefaultRouteBuilder : IRouteBuilder
    {
        private readonly IStatementFactory _statementFactory;
        private readonly IRouteGraphBuilder _graphBuilder;

        private string _id = Guid.NewGuid().ToString();
        private string? _description;
        private bool _allowSynchronousContinuations;

        public DefaultRouteBuilder(
            IStatementFactory statementFactory,
            IRouteGraphBuilder graphBuilder)
        {
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

        public IRouteBuilder AllowSynchronousContinuations(bool allow)
        {
            _allowSynchronousContinuations = allow;
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
            AddNode(_statementFactory.Create(Statement.From, configure));
            return this;
        }

        public IRouteBuilder From(Action<IDromedaryComponent> configure, Type componentType)
        {
            AddNode(_statementFactory.Create(Statement.From, componentType, configure));
            return this;
        }
        #endregion

        #region To

        public IRouteBuilder To(string uri)
        {
            throw new NotImplementedException();
        }

        public IRouteBuilder To<T>()
            where T : class, IDromedaryComponent
        {
            AddNode(_statementFactory.Create<T>(Statement.To, _ => { }));
            return this;
        }

        public IRouteBuilder To<T>(Action<T> configure)
            where T : class, IDromedaryComponent
        {
            AddNode(_statementFactory.Create(Statement.To, configure));
            return this;
        }

        public IRouteBuilder To(Action<IDromedaryComponent> configure, Type componentType)
        {
            AddNode(_statementFactory.Create(Statement.To, componentType, configure));
            return this;
        }

        public IRouteBuilder To<T>(Func<T, Task> configure)
            where T : class, IDromedaryComponent
        {
            AddNode(_statementFactory.Create(Statement.To, configure));
            return this;
        }
        #endregion

        #region Process

        public IRouteBuilder Process<T>()
            where T : IProcessor
        {
            AddNode(_statementFactory.Create<IProcessComponent>(Statement.Process, p =>
            {
                p.ProcessType = typeof(T);
            }));
            return this;
        }

        public IRouteBuilder Process(Type process)
        {
            AddNode(_statementFactory.Create<IProcessComponent>(Statement.Process, p =>
            {
                p.ProcessType = process;
            }));
            return this;
        }

        public IRouteBuilder Process(Action<IExchange> process)
        {
            AddNode(_statementFactory.Create<IProcessComponent>(Statement.Process, p =>
            {
                p.Process = process;
            }));
            return this;
        }

        public IRouteBuilder Process(Func<IExchange, Task> process)
        {
            AddNode(_statementFactory.Create<IProcessComponent>(Statement.Process, p =>
            {
                p.AsyncProcess = process;
            }));
            return this;
        }

        #endregion

        #region Log
        public IRouteBuilder Log(LogLevel level)
        {
            return this;
        }

        public IRouteBuilder Log(LogLevel level, string message)
        {
            AddNode(_statementFactory.Create<ILoggerDromedaryComponent>(Statement.Log, component =>
            {
                component.LogLevel = level;
                component.Message = message;
            }));

            return this;
        }

        public IRouteBuilder Log(LogLevel level, string message, params object[] args)
        {
            AddNode(_statementFactory.Create<ILoggerDromedaryComponent>(Statement.Log, component =>
            {
                component.LogLevel = level;
                component.Message = message;
                component.Args = args;
            }));

            return this;
        }

        public IRouteBuilder Log(LogLevel level, Func<IExchange, string> message)
        {
            AddNode(_statementFactory.Create<ILoggerDromedaryComponent>(Statement.Log, component =>
            {
                component.LogLevel = level;
                component.MessageFactory = message;
            }));

            return this;
        }
        #endregion
        
        private void AddNode(IStatement statement)
            => _graphBuilder.Add(statement);
        
        #region Build

        public IRoute Build() 
            => new DefaultRoute(_id, _description, _graphBuilder.Build(), _allowSynchronousContinuations);

        #endregion
    }
}
