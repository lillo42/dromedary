using Dromedary.Commands;

namespace Dromedary.Statements
{
    public interface IStatement
    {
        ICommand Command { get; }
        Statement Statement { get; }
    }
    
    
}
