using Dromedary.Statements;

namespace Dromedary.Factories
{
    public class DefaultRouteNodeFactory : IRouteNodeFactory
    {
        public IRouteNode Create(IStatement statement) 
            => new DefaultRouteNode(statement);
    }
}
