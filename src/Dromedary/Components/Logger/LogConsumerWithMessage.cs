using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dromedary.Components.Logger
{
    internal class LogConsumerWithMessage : IConsumer
    {
        private readonly LogLevel _level;
        private readonly string _message;
        private readonly object[] _args;

        public LogConsumerWithMessage(LogLevel level, string message, object[] args)
        {
            _message = message ?? throw new ArgumentNullException(nameof(message));
            _args = args  ?? throw new ArgumentNullException(nameof(args));
            _level = level;
        }

        public IProcessor CreateProcessor(IServiceProvider provider)
        {
            var factory = provider.GetRequiredService<ILoggerFactory>();
            return new LogMessage(factory.CreateLogger<IRoute>(), _level, _message, _args);
        }
        
        private class LogMessage : IProcessor
        {
            private readonly ILogger _logger;
            private readonly LogLevel _level;
            private readonly string _message;
            private readonly object[] _args;

            public LogMessage(ILogger logger, LogLevel level, string message, object[] args)
            {
                _message = message ?? throw new ArgumentNullException(nameof(message));
                _args = args  ?? throw new ArgumentNullException(nameof(args));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _level = level;
            }
            
            public ValueTask ExecuteAsync(IExchange exchange, CancellationToken cancellationToken = default)
            {
                _logger.Log(_level, _message, _args);
                return new ValueTask();
            }
        } 
    }
}
