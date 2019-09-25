using Dromedary.Statements;

namespace Dromedary.Builder
{
    public interface IRouteGraphBuilder
    {
        IRouteGraphBuilder Add(IStatement statement);
        IRouteGraph Build();
    }
}
