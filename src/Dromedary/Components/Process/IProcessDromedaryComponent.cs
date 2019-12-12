using System;
using System.Threading.Tasks;

namespace Dromedary.Components.Process
{
    public interface IProcessDromedaryComponent : IDromedaryComponent
    {
        Type? ProcessType { get; set; }
        Action<IExchange>? Process { get; set; }
        Func<IExchange, Task>? AsyncProcess { get; set; }
    }
}
