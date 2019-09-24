using System;
using System.Collections.Generic;

namespace Dromedary
{
    public interface IDromedaryContext
    {
        string Id { get; }
        string Name { get; }
        string Version { get; }
        DateTime UpTime { get; }
        IReadOnlyCollection<IRoute> Routes { get; }
        IServiceProvider Service { get; }
    }
}
