using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dromedary.Component.Console
{
    public class ConsoleComponent : IDromedaryComponent<ConsoleEndpoint>
    {
        public string PromptMessage { get; set; }

        public ConsoleEndpoint CreateEndpoint()
            => new ConsoleEndpoint(PromptMessage);

        IEndpoint IDromedaryComponent.CreateEndpoint()
        {
            return CreateEndpoint();
        }

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
    }
}
