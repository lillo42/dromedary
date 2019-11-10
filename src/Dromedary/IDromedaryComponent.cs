using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dromedary
{
    public interface IDromedaryComponent
    {
        IEndpoint CreateEndpoint();
        void ConfigureProperties(Action<IDromedaryComponent> config);
        Task ConfigurePropertiesAsync(Func<IDromedaryComponent, Task> config);
        void ConfigureProperties(IDictionary<string, object> config);
    }

    public interface IDromedaryComponent<T> : IDromedaryComponent
        where T : IEndpoint
    {
        IEndpoint IDromedaryComponent.CreateEndpoint()
            => CreateEndpoint();
        
        new T CreateEndpoint();
    }
}
