using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dromedary.Component.Console
{
    public class ConsoleComponent : IDromedaryComponent
    {
        public string PromptMessage { get; set; }

        public IEndpoint CreateEndpoint()
            => new ConsoleEndpoint(PromptMessage);
    }
}
