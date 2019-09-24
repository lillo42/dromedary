using System;
using System.Collections.Generic;

namespace Dromedary
{
    public interface IMessage : IEquatable<IMessage>, ICloneable<IMessage>
    {
        string Id { get; }
        
        IExchange Exchange { get; }
        
        bool HasHeader { get; }
        
        IDictionary<string, object> Headers { get; set; }
        
        object Body { get; set; }
        T GetBody<T>();

    }
}
