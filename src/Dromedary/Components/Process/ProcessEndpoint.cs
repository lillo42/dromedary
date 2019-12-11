using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dromedary.Activator;
using Dromedary.Exceptions;

namespace Dromedary.Components.Process
{
    internal class ProcessEndpoint : IEndpoint
    {
        private readonly IConsumer _consumer;
        public ProcessEndpoint(Type? processType, 
            Action<IExchange>? process, 
            Func<IExchange, Task>? asyncProcess)
        {
            if (process != null)
            {
                _consumer = new ProcessWithAction(process);
            }
            
            if (asyncProcess != null)
            {
                _consumer = new ProcessWithFunc(asyncProcess);
            }
            
            if (processType != null)
            {
                _consumer = new ProcessWithType(processType);
            }

            if (processType == null && process == null && asyncProcess == null)
            {
                throw new ArgumentException("all parameters are null");
            }
        }

        public IProducer CreateProducer() 
            => throw new NotImplementedException();

        public IConsumer CreateConsumer() 
            => _consumer;
    }
}
