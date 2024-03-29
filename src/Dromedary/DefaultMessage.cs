using System;
using System.Collections.Generic;

namespace Dromedary
{
    public class DefaultMessage : IMessage
    {
        public DefaultMessage(string id, IExchange exchange)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Exchange = exchange ?? throw new ArgumentNullException(nameof(exchange));
        }

        public virtual string Id { get; }
        public virtual IExchange Exchange { get; }
        public virtual IDictionary<string, object>? Headers { get; set; } = new Dictionary<string, object>();
        public virtual object? Body { get; set; }

        protected virtual bool Equals(DefaultMessage other) 
            => Equals(other as IMessage);

        public bool Equals(IMessage other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }
            
            return Id == other.Id 
                   && Equals(Exchange, other.Exchange)
                   && Equals(Headers, other.Headers) 
                   && Equals(Body, other.Body);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is IMessage message)
            {
                return Equals(message);
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Exchange != null ? Exchange.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Headers != null ? Headers.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Body != null ? Body.GetHashCode() : 0);
                return hashCode;
            }
        }
        
        public IMessage Clone()
        {
            return new DefaultMessage(Id,Exchange)
            {
                Headers = Headers,
                Body = Body
            };
        }
    }
}
