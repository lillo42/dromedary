using System;
using System.Collections.Generic;

namespace Dromedary
{
    public class DefaultExchange : IExchange
    {
        public DefaultExchange(string id)
        {
            Id = id;
        }

        public virtual  string Id { get; }
        public virtual IDictionary<string, object>? Properties { get; set; } = new Dictionary<string, object>();
        public virtual IMessage? Message { get; set; }
        public virtual Exception? Exception { get; set; }

        public virtual DateTime Created { get; } = DateTime.UtcNow;
        
        public virtual IExchange Clone()
        {
            return new DefaultExchange(Id)
            {
                Exception = Exception,
                Properties = Properties
            };
        }

        protected virtual bool Equals(DefaultExchange other)
        {
            return Id == other.Id 
                   && Equals(Properties, other.Properties) 
                   && Equals(Exception, other.Exception) 
                   && Created.Equals(other.Created);
        }

        public virtual bool Equals(IExchange other) 
            => Equals((object)other);

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

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((DefaultExchange) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Id != null ? Id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Properties != null ? Properties.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Exception != null ? Exception.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Created.GetHashCode();
                return hashCode;
            }
        }
    }
}
