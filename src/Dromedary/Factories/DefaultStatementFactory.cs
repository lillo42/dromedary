using Dromedary.Commands;
using Dromedary.Statements;

namespace Dromedary.Factories
{
    public class DefaultStatementFactory : IStatementFactory
    {
        public IStatement Create(ICommand command, Statement statement) 
            => new DefaultStatement(command, statement);
    }
}
