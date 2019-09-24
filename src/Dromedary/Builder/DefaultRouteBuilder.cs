using System;
using System.Threading.Tasks;
using Dromedary.Commands;
using Dromedary.Factories;

namespace Dromedary.Builder
{
    public class DefaultRouteBuilder : IRouteBuilder
    {
        private readonly IDromedaryContext _context;
        private readonly ICommandFactory _commandFactory;
        private IChannelNode _lastNode;

        private string _id;
        private string _description;

        public DefaultRouteBuilder(IDromedaryContext context, ICommandFactory commandFactory)
        {
            _context = context;
            _commandFactory = commandFactory;
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
            var command = _commandFactory.CreateCommand<T>(configure);
            _root = new DefaultChannelNode(null, command);
            return this;
        }

        public IRouteBuilder From<T>(Action<T, IExchange> configure) 
            where T : class, IDromedaryComponent
        {
            throw new NotImplementedException();
        }

        public IRouteBuilder From<T>(Action<IDromedaryComponent> configure, Type componentType)
        {
            throw new NotImplementedException();
        }

        public IRouteBuilder From(Action<IDromedaryComponent, IExchange> configure, Type componentType)
        {
            throw new NotImplementedException();
        }

        public IRouteBuilder From<T>(Func<T, Task> configure) 
            where T : class, IDromedaryComponent
        {
            throw new NotImplementedException();
        }

        public IRouteBuilder From<T>(Func<T, IExchange, Task> configure) 
            where T : class, IDromedaryComponent
        {
            throw new NotImplementedException();
        }

        public IRouteBuilder From<T>(Func<IDromedaryComponent, Task> configure, Type componentType)
        {
            throw new NotImplementedException();
        }

        public IRouteBuilder From(Func<IDromedaryComponent, IExchange, Task> configure, Type componentType)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region To

        public IRouteBuilder To(string uri)
        {
            throw new NotImplementedException();
        }

        public IRouteBuilder To<T>(Action<T> configure) 
            where T : class, IDromedaryComponent
        {
            AddNode(_commandFactory.CreateCommand(configure));
            return this;
        }

        public IRouteBuilder To<T>(Action<T, IExchange> configure) 
            where T : class, IDromedaryComponent
        {
            AddNode(_commandFactory.CreateCommand(configure));
            return this;
        }

        public IRouteBuilder To(Action<IDromedaryComponent, IExchange> configure, Type componentType)
        {
            AddNode(_commandFactory.CreateCommand(configure, componentType));
            return this;
        }

        public IRouteBuilder To<T>(Func<T, Task> configure) 
            where T : class, IDromedaryComponent
        {
            AddNode(_commandFactory.CreateCommand(configure));
            return this;
        }

        public IRouteBuilder To<T>(Func<T, IExchange, Task> configure) 
            where T : class, IDromedaryComponent
        {
            AddNode(_commandFactory.CreateCommand(configure));
            return this;
        }

        public IRouteBuilder To(Func<IDromedaryComponent, IExchange, Task> configure, Type componentType)
        {
            AddNode(_commandFactory.CreateCommand(configure, componentType));
            return this;
        }

        #endregion

        private void AddNode(ICommand command)
        {
            if (_lastNode == null)
            {
                _lastNode = new DefaultChannelNode(null, command);
            }
            else
            {
                var node = new DefaultChannelNode(_lastNode, command);
                _lastNode.Children.Add(node);
                _lastNode = node;
            }
        }

        #region Build

        public IRoute Build()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
