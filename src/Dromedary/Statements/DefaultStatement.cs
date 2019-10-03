using Dromedary.Commands;

namespace Dromedary.Statements
{
    public class DefaultStatement : IStatement
    {
        public DefaultStatement(IConfigureComponent configureComponent, Statement statement)
        {
            ConfigureComponent = configureComponent;
            Statement = statement;
        }

        public IConfigureComponent ConfigureComponent { get; }
        public Statement Statement { get; }
    }
}
