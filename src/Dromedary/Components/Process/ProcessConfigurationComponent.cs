using System;
using Dromedary.Commands;

namespace Dromedary.Components.Process
{
    public class ProcessConfigurationComponent : IConfigureComponent
    {
        public Type ComponentType => typeof(ProcessDromedaryComponent);
        public void Configure(IExchange exchange, IDromedaryComponent component)
        {
            throw new NotImplementedException();
        }
    }
}
