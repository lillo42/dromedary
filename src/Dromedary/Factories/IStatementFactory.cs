using Dromedary.Commands;
using Dromedary.Statements;

namespace Dromedary.Factories
{
    public interface IStatementFactory
    {
        IStatement Create(IConfigureComponent configureComponent, Statement statement);
    }
}

