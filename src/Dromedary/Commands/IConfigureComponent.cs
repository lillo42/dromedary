using System;

namespace Dromedary.Commands
{
    public interface IConfigureComponent
    {
        Type ComponentType { get; }
        void Configure(IExchange exchange, IDromedaryComponent component);
    }
    
    public interface IConfigureComponent<T> : IConfigureComponent
        where T : IDromedaryComponent
    {
        void Configure(IExchange exchange, T component);
    }
}
