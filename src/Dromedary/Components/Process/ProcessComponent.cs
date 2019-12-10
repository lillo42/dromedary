using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dromedary.Components.Process
{
    public class ProcessComponent : IProcessComponent
    {
        public Type? ProcessType { get; set; }
        public Action<IExchange>? Process { get; set; }
        public Func<IExchange, Task>? AsyncProcess { get; set; }
        public IEndpoint CreateEndpoint()
        {
            throw new NotImplementedException();
        }

        public void ConfigureProperties(Action<IDromedaryComponent> config)
        {
            config(this);
        }

        public Task ConfigurePropertiesAsync(Func<IDromedaryComponent, Task> config)
        {
            throw new NotImplementedException();
        }

        public void ConfigureProperties(IDictionary<string, object> config)
        {
            throw new NotImplementedException();
        }
    }
}
