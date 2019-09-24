using System.Collections.Generic;

namespace Dromedary
{
    public interface IDromedaryComponent
    {
        IDromedaryContext Context { get; }
        IEndpoint CreateEndpoint(string uri);
        IEndpoint CreateEndpoint(string uri, IDictionary<string, object> parameters);
    }
}
