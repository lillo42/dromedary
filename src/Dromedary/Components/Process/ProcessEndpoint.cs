using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dromedary.Activator;

namespace Dromedary.Components.Process
{
    internal class ProcessEndpoint : IEndpoint
    {
        private readonly IActivator _activator;
        private readonly Type? _processType;
        private readonly Action<IExchange>? _process;
        private readonly Func<IExchange, Task>? _asyncProcess;
        public ProcessEndpoint(IActivator provider, 
            Type? processType, 
            Action<IExchange>? process, 
            Func<IExchange, Task>? asyncProcess)
        {
            _activator = provider ?? throw new ArgumentNullException(nameof(provider));
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
                return new ProcessConsumer(new ProcessWithAction(_process));
            }

            if(_asyncProcess != null)
            {
                return new ProcessConsumer(new ProcessWithFunc(_asyncProcess));
            }

            if (_processType != null)
            {
                var process = (IProcessor)_activator.CreateInstance(_processType);
                if (process == null)
                {
                    throw new NotImplementedException();
                }
                
                return new ProcessConsumer(process);
            }
            
            throw new NotImplementedException();
        }

        public void ConfigureProperties(IDictionary<string, object> option) 
            => throw new NotImplementedException();
    }
}
