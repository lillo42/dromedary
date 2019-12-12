using System;

namespace Dromedary.Components.Filter
{
    public interface IFilterDromedaryComponent : IDromedaryComponent
    {
        Func<IExchange, bool> Filter { get; set; }
    }
}
