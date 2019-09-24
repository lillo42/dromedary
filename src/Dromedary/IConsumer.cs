namespace Dromedary
{
    public interface IConsumer
    {
        IProcessor Processor { get; }
    }
}
