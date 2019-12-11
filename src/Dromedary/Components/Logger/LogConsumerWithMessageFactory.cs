using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dromedary.Components.Logger
{
    internal class LogConsumerWithMessageFactory : IConsumer
    {
        private readonly LogLevel _level;
        private readonly Func<IExchange, string> _messageFactory;

        public LogConsumerWithMessageFactory(LogLevel level, Func<IExchange, string> messageFactory)
        {
            _level = level;
            _messageFactory = messageFactory ?? throw new ArgumentNullException(nameof(messageFactory));
        }
        
        public IProcessor CreateProcessor(IServiceProvider provider)
        {
            var factory = provider.GetRequiredService<ILoggerFactory>();
            return new LogWithMessageFactory(factory.CreateLogger<IRoute>(), _level, _messageFactory);
        }
        
        private class LogWithMessageFactory : IProcessor
        {
            private readonly ILogger _logger;
            private readonly LogLevel _level;
            private readonly Func<IExchange, string> _messageFactory;

            public LogWithMessageFactory(ILogger logger, LogLevel level, Func<IExchange, string> messageFactory)
            {
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _level = level;
                _messageFactory = messageFactory ?? throw new ArgumentNullException(nameof(messageFactory));
            }


            public ValueTask ExecuteAsync(IExchange exchange, CancellationToken cancellationToken = default)
            {
                _logger.Log(_level, _messageFactory(exchange));
                return new ValueTask();
            }
        }
    }
}
