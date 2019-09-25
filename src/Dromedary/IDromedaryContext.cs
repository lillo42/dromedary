using System;
using System.Collections.Generic;
using Dromedary.Builder;

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

        void AddRoute(Action<IRouteBuilder> builder);
    }
}
