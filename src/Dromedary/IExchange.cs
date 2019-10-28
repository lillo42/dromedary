using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Dromedary
{
    public interface IExchange : IEquatable<IExchange>, ICloneable<IExchange>
    {
        string Id { get; }

        IDictionary<string, object>? Properties { get; set; }
        IMessage? Message { get; set; }

        bool HasProperties => Properties != null && Properties.Count > 0;
        
        Exception? Exception { get; set; }

        [return: MaybeNull]
        T? GetException<T>()
            where T : Exception
        {
            return Exception as T;
        }

        bool IsFailed => Exception != null;
        
        DateTime Created { get; }
    }
}
