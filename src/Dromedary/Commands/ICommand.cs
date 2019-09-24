using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dromedary.Commands
{
    public interface ICommand
    {
        Type ComponentType { get; }
        ValueTask<bool> CanExecuteAsync(IExchange exchange, IDromedaryComponent component, CancellationToken cancellationToken = default);
        ValueTask ExecuteAsync(IExchange exchange, IDromedaryComponent component, CancellationToken cancellationToken = default);
    }
    
    public interface ICommand<T> : ICommand
        where T : IDromedaryComponent
    {
        ValueTask<bool> CanExecuteAsync(IExchange exchange, T component, CancellationToken cancellationToken = default);
        ValueTask ExecuteAsync(IExchange exchange, T component, CancellationToken cancellationToken = default);
    }
}
