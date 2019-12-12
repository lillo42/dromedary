using System;
using Dromedary.Activator;
using Microsoft.Extensions.DependencyInjection;

namespace Dromedary.Components.Process
{
    internal class ProcessWithType : IConsumer
    {
        private readonly Type _processor;

        public ProcessWithType(Type processor)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        public IProcessor CreateProcessor(IServiceProvider provider)
        {
            var activator = provider.GetRequiredService<IActivator>();
            return (IProcessor)activator.CreateInstance(_processor);
        }
    }
}
