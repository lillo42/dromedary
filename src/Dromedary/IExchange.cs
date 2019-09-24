using System;
using System.Collections.Generic;

namespace Dromedary
{
    public interface IExchange : IEquatable<IExchange>, ICloneable<IExchange>
    {
        string Id { get; }

        IDromedaryContext Context { get; }
        
        IDictionary<string, object> Properties { get; set; }
        
        bool HasProperties { get; }
        
        Exception Exception { get; set; }

        T GetException<T>()
            where T : Exception;
        
        bool IsFailed { get; }
        
        DateTime Created { get; }
    }
}
