using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dromedary
{
    public interface IRoute : IDisposable
    {
        string Id { get; }
        string Description { get; }

        ValueTask ExecuteAsync(CancellationToken cancellationToken);
    }
}
