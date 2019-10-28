using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Dromedary
{
    public interface IMessage : IEquatable<IMessage>, ICloneable<IMessage>
    {
        string Id { get; }
        
        IExchange Exchange { get; }

        bool HasHeader => Headers != null && Headers.Count > 0;
        
        IDictionary<string, object>? Headers { get; set; }
        
        object? Body { get; set; }

        [return: MaybeNull]
        T GetBody<T>()
        {
            if (Body == null)
            {
                return default;
            }
            
            var type = typeof(T);
            if (type.IsInstanceOfType(Body))
            {
                return (T)Body;
            }
            
            return  (T) TypeDescriptor.GetConverter(type).ConvertFrom(Body);
        }
    }
}
