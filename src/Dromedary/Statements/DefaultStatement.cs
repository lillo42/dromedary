using Dromedary.Commands;

namespace Dromedary.Statements
{
    public class DefaultStatement : IStatement
    {
        public DefaultStatement(ICommand command, Statement statement)
        {
            Command = command;
            Statement = statement;
        }

        public ICommand Command { get; }
        public Statement Statement { get; }
    }
}
