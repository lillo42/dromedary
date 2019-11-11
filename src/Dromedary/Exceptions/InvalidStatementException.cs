using System;
using Dromedary.Statements;

namespace Dromedary.Exceptions
{
    public class InvalidStatementException : DromedaryException
    {
        public IStatement Statement { get; }
        public InvalidStatementException(IStatement statement)
        {
            Statement = statement ?? throw new ArgumentNullException(nameof(statement));
        }
    }
}
