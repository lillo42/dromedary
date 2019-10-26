using System;
using Dromedary.Activator;
using Dromedary.Factories;

namespace Dromedary.Component.Console
{
    public class ConsoleEndpoint : IEndpoint
    {
        private readonly IActivator _service;
        public ConsoleEndpoint(IActivator activator)
        {
            _service = activator ?? throw new ArgumentNullException(nameof(activator));
        }

        public string PromptMessage { get; set; }

        public IProducer CreateProducer()
            => new ConsoleProducer(
                _service.CreateInstance<IExchangeFactory>())
            {
                PromptMessage = PromptMessage
            };

        public IConsumer CreateConsumer() 
            => new ConsoleConsumer();
    }
}
