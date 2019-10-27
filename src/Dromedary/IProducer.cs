using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Dromedary.Factories;

namespace Dromedary
{
    public interface IProducer
    {
        IExchangeFactory Factory { get; set; }
        Task ExecuteAsync(ChannelWriter<IExchange> channel, CancellationToken cancellationToken = default);
    }
}
