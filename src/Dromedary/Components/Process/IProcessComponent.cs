using System;
using System.Threading.Tasks;

namespace Dromedary.Components.Process
{
    public interface IProcessComponent : IDromedaryComponent
    {
        Type? ProcessType { get; set; }
        Action<IExchange>? Process { get; set; }
        Func<IExchange, Task>? AsyncProcess { get; set; }
    }
}
