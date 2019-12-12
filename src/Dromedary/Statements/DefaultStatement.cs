using System;

namespace Dromedary.Statements
{
    public class DefaultStatement : IStatement
    {
        public DefaultStatement(IEndpoint endpoint, Statement statement)
        {
            Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            Statement = statement;
        }

        public Statement Statement { get; }
        public IEndpoint Endpoint { get; }
    }
}
