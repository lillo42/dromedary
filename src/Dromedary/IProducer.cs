using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Dromedary
{
    public interface IProducer
    {
        Task ExecuteAsync(ChannelWriter<IExchange> channel, CancellationToken cancellationToken = default);
    }
}
