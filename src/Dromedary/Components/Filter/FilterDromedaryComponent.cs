using System;

namespace Dromedary.Components.Filter
{
    internal class FilterDromedaryComponent : IFilterDromedaryComponent
    {
        public Func<IExchange, bool> Filter { get; set; }
        public IEndpoint CreateEndpoint()
        {
            return new FilterEndpoint(Filter);
        }
    }
}
