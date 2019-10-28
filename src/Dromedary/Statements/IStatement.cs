using System;
using System.Threading.Tasks;

namespace Dromedary.Statements
{
    public interface IStatement
    {
        Statement Statement { get; }
        Type Component { get; }
        Action<IDromedaryComponent>? ConfigureComponent { get; }
        Func<IDromedaryComponent, Task>? ConfigureComponentAsync { get; }
    }

    public interface IStatement<T> : IStatement
        where T : IDromedaryComponent
    {
        new Action<T>? ConfigureComponent { get; }
        new Func<T, Task>? ConfigureComponentAsync { get; }
    }
}
