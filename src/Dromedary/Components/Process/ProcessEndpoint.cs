using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dromedary.Activator;
using Dromedary.Exceptions;

namespace Dromedary.Components.Process
{
    internal class ProcessEndpoint : IEndpoint
    {
        private readonly Type? _processType;
        private readonly Action<IExchange>? _process;
        private readonly Func<IExchange, Task>? _asyncProcess;
        public ProcessEndpoint(IActivator provider, 
            Type? processType, 
            Action<IExchange>? process, 
            Func<IExchange, Task>? asyncProcess)
        {
            _processType = processType;
            _process = process;
            _asyncProcess = asyncProcess;

            if (processType == null && process == null && asyncProcess == null)
            {
                throw new ArgumentException("all parameters are null");
            }
        }

        public IProducer CreateProducer() 
            => throw new NotImplementedException();

        public IConsumer CreateConsumer()
        {
            if (_process != null)
            {
                return new ProcessWithAction(_process);
            }

            if(_asyncProcess != null)
            {
                return new ProcessWithFunc(_asyncProcess);
            }

            if (_processType != null)
            {
                return new ProcessWithType(_processType);
            }
            
            throw new DromedaryException();
        }
    }
}
