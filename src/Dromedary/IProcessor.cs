using System.Threading;
using System.Threading.Tasks;

namespace Dromedary
{
    public interface IProcessor
    {
        ValueTask<bool> CanExecute(IExchange exchange, CancellationToken cancellationToken = default);
        ValueTask ExecuteAsync(IExchange exchange, CancellationToken cancellationToken = default);
    }
}
