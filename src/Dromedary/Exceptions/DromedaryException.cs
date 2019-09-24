using System;
using System.Runtime.Serialization;

namespace Dromedary.Exceptions
{
    public class DromedaryException : Exception
    {
        public DromedaryException()
        {
        }

        protected DromedaryException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }

        public DromedaryException(string message) 
            : base(message)
        {
        }

        public DromedaryException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
