using Dromedary.Statements;

namespace Dromedary.Factories
{
    public interface IRouteNodeFactory
    {
        IRouteNode Create(IStatement statement);
    }
}
