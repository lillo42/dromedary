using System.Collections.Generic;

namespace Dromedary
{
    public interface IReadOutput
    {
        object Output { get; }
        IEnumerable<object> Outputs { get; }
    }
}
