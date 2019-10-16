using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Dromedary.Components.Process
{
    internal class ProcessEndpoint : IEndpoint
    {
        private readonly IServiceProvider _provider;
        public ProcessEndpoint(IServiceProvider provider, 
            Type processType, 
            Action<IExchange> process, 
            Func<IExchange, Task> asyncProcess)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            this._processType = processType;
            _process = process;
            _asyncProcess = asyncProcess;

            if (processType == null && process == null && asyncProcess == null)
            {
                throw new ArgumentException("all parameer are null");
            }
        }

        private readonly Type _processType;
        private readonly Action<IExchange> _process;
        private readonly Func<IExchange, Task> _asyncProcess;
        
        
        public IProducer CreateProducer() 
            => throw new System.NotImplementedException();

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

            var process = (IProcessor) _provider.GetRequiredService(_processType);
            return new ProcessConsumer(process);
        }

        public void ConfigureProperties(IDictionary<string, object> option) 
            => throw new System.NotImplementedException();
    }
}
