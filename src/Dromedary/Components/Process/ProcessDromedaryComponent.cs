﻿using System;
using System.Threading.Tasks;

namespace Dromedary.Components.Process
{
    public class ProcessDromedaryComponent : IProcessDromedaryComponent
    {
        public Type? ProcessType { get; set; }
        public Action<IExchange>? Process { get; set; }
        public Func<IExchange, Task>? AsyncProcess { get; set; }
        
        public IEndpoint CreateEndpoint()
        {
            return new ProcessEndpoint(ProcessType, Process, AsyncProcess);
        }
    }
}
