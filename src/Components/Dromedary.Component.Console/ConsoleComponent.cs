using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dromedary.Activator;

namespace Dromedary.Component.Console
{
    public class ConsoleComponent : IDromedaryComponent<ConsoleEndpoint>
    {
        private readonly IActivator _activator;
        public ConsoleComponent(IActivator activator)
        {
            _activator = activator ?? throw new ArgumentNullException(nameof(activator));
        }

        public string PromptMessage { get; set; }

        public ConsoleEndpoint CreateEndpoint()
            => new ConsoleEndpoint(_activator)
            {
                PromptMessage = PromptMessage
            };

        public void ConfigureProperties(Action<IDromedaryComponent> config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            config(this);
        }

        public async Task ConfigurePropertiesAsync(Func<IDromedaryComponent, Task> config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            await config(this);
        }

        public void ConfigureProperties(IDictionary<string, object> config)
        {
            throw new NotImplementedException();
        }

        IEndpoint IDromedaryComponent.CreateEndpoint() 
            => CreateEndpoint();
    }
}
