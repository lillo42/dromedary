using System;

namespace Dromedary.Component.Console
{
    public class ConsoleComponent : IDromedaryComponent<ConsoleEndpoint>
    {
        private readonly IServiceProvider _service;
        public ConsoleComponent(IDromedaryContext context, IServiceProvider service)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public string PromptMessage { get; set; }

        public IDromedaryContext Context { get; }

        public ConsoleEndpoint CreateEndpoint()
            => new ConsoleEndpoint(Context, _service)
            {
                PromptMessage = PromptMessage
            };

        IEndpoint IDromedaryComponent.CreateEndpoint() 
            => CreateEndpoint();
    }
}
