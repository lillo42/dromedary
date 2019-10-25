using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dromedary.Components.Process
{
    public class ProcessDromedaryComponent : IProcessDromedaryComponent
    {
        private readonly IServiceProvider _provider;

        public ProcessDromedaryComponent(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public virtual Type ProcessType { get; set; }
        public Action<IExchange> Process { get; set; }
        public Func<IExchange, Task> AsyncProcess { get; set; }

        public IEndpoint CreateEndpoint()
        {
            return new ProcessEndpoint(_provider, ProcessType, Process, AsyncProcess);
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
