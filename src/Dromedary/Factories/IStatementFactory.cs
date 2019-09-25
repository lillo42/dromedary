using Dromedary.Commands;
using Dromedary.Statements;

namespace Dromedary.Factories
{
    public interface IStatementFactory
    {
        IStatement Create(ICommand command, Statement statement);
    }
}

