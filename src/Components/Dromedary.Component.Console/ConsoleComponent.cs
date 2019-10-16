using System;

namespace Dromedary.Component.Console
{
    public class ConsoleComponent : IDromedaryComponent<ConsoleEndpoint>
    {
        private readonly IServiceProvider _service;
        public ConsoleComponent(IServiceProvider service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public string PromptMessage { get; set; }

        public ConsoleEndpoint CreateEndpoint()
            => new ConsoleEndpoint(_service)
            {
                PromptMessage = PromptMessage
            };

        IEndpoint IDromedaryComponent.CreateEndpoint() 
            => CreateEndpoint();
    }
}
