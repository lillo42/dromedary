using Dromedary.Commands;
using Dromedary.Statements;

namespace Dromedary.Factories
{
    public class DefaultStatementFactory : IStatementFactory
    {
        public IStatement Create(IConfigureComponent configureComponent, Statement statement) 
            => new DefaultStatement(configureComponent, statement);
    }
}
