using System.Threading;
using System.Threading.Tasks;

namespace Dromedary
{
    public interface IProcessor
    {
        ValueTask ExecuteAsync(IExchange exchange, CancellationToken cancellationToken = default);
    }
}
