using System;
using Dromedary.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Dromedary.Component.Console
{
    public class ConsoleEndpoint : IEndpoint
    {
        private readonly IServiceProvider _service;
        public ConsoleEndpoint(IServiceProvider serviceProvider)
        {
            _service = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public string PromptMessage { get; set; }

        public IProducer CreateProducer()
            => new ConsoleProducer(
                _service.GetRequiredService<IExchangeFactory>())
            {
                PromptMessage = PromptMessage
            };

        public IConsumer CreateConsumer() 
            => new ConsoleConsumer();
    }
}
