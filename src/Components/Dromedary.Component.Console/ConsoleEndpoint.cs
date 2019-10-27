namespace Dromedary.Component.Console
{
    public class ConsoleEndpoint : IEndpoint
    {
        private static readonly ConsoleConsumer _consumer = new ConsoleConsumer();
        private readonly string _promptMessage;

        public ConsoleEndpoint(string promptMessage)
        {
            _promptMessage = promptMessage;
        }

        public IProducer CreateProducer()
            => new ConsoleProducer(_promptMessage);

        public IConsumer CreateConsumer()
            => _consumer;
    }
}
