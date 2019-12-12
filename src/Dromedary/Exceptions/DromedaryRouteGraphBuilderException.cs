using System;
using Dromedary.Statements;

namespace Dromedary.Exceptions
{
    public class DromedaryRouteGraphBuilderException : DromedaryException
    {
        public IStatement Statement { get; }

        public DromedaryRouteGraphBuilderException(string message, IStatement statement) 
            : base(message)
        {
            Statement = statement;
        }

        public DromedaryRouteGraphBuilderException(string message, IStatement statement, Exception innerException) 
            : base(message, innerException)
        {
            Statement = statement;
        }
    }
}
