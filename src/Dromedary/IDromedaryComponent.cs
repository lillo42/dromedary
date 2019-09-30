using System.Collections.Generic;

namespace Dromedary
{
    public interface IDromedaryComponent
    {
        IDromedaryContext Context { get; }
        IEndpoint CreateEndpoint();
    }

    public interface IDromedaryComponent<T> : IDromedaryComponent
        where T : IEndpoint
    {
        new T CreateEndpoint();
    }
}
