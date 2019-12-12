namespace Dromedary.Statements
{
    public interface IStatement
    {
        Statement Statement { get; }
        IEndpoint Endpoint { get; }
    }
}
