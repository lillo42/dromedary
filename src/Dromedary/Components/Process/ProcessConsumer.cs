namespace Dromedary.Components.Process
{
    internal class ProcessConsumer : IConsumer
    {
        public ProcessConsumer(IProcessor processor)
        {
            Processor = processor;
        }

        public IProcessor Processor { get; }
    }
}
