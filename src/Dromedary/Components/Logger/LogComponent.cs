using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Dromedary.Components.Logger
{
    internal class LogComponent : ILoggerDromedaryComponent
    {
        private readonly ILoggerFactory _factory;

        public LogComponent(ILoggerFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public string? Message { get; set; }
        public Func<IExchange, string>? MessageFactory { get; set; }
        public object[] Args { get; set; } = new object[0];
        public LogLevel LogLevel { get; set; }
        
        public IEndpoint CreateEndpoint() 
            => new LogEndpoint(Message, Args, MessageFactory, LogLevel);

        public void ConfigureProperties(Action<IDromedaryComponent> config)
        {
            config(this);
        }

        public Task ConfigurePropertiesAsync(Func<IDromedaryComponent, Task> config)
        {
            return config(this);
        }

        public void ConfigureProperties(IDictionary<string, object> config)
        {
            throw new NotImplementedException();
        }
    }
}
