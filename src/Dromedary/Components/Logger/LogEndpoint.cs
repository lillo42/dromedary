using System;
using Dromedary.Exceptions;
using Microsoft.Extensions.Logging;

namespace Dromedary.Components.Logger
{
    public class LogEndpoint : IEndpoint
    {
        private readonly IConsumer _consumer;

        public LogEndpoint(string? message, object[] args, Func<IExchange, string>? messageFactory, LogLevel level)
        {
            if (message != null)
            {
                _consumer = new LogConsumerWithMessage(level, message, args);
            }

            if (messageFactory != null)
            {
                _consumer =  new LogConsumerWithMessageFactory(level, messageFactory);
            }

            if (message == null && messageFactory == null)
            {
                throw new ArgumentNullException();
            }
        }

        public IProducer CreateProducer()
        {
            throw new System.NotImplementedException();
        }

        public IConsumer CreateConsumer() 
            => _consumer;
    }
}
