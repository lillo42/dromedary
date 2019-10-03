using Dromedary.Commands;

namespace Dromedary.Statements
{
    public interface IStatement
    {
        IConfigureComponent ConfigureComponent { get; }
        Statement Statement { get; }
    }
    
    
}
