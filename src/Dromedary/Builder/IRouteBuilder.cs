using System;
using System.Threading.Tasks;

namespace Dromedary.Builder
{
    public interface IRouteBuilder
    {
        #region Configure
        IRouteBuilder SetId(string id);
        
        IRouteBuilder SetDescription(string description);
        #endregion
        
        #region From
        IRouteBuilder From(string uri);

        IRouteBuilder From<T>(Action<T> configure)
            where T : class, IDromedaryComponent;

        IRouteBuilder From(Action<IDromedaryComponent> configure, Type componentType);

        IRouteBuilder From<T>(Func<T, Task> configure)
            where T : class, IDromedaryComponent;
        
        IRouteBuilder From(Func<IDromedaryComponent, Task> configure, Type componentType);
        #endregion

        #region To
        IRouteBuilder To(string uri);

        IRouteBuilder To<T>()
            where T : class, IDromedaryComponent;
        
        IRouteBuilder To<T>(Action<T> configure)
            where T : class, IDromedaryComponent;

        IRouteBuilder To(Action<IDromedaryComponent> configure, Type componentType);
        
        IRouteBuilder To<T>(Func<T, Task> configure)
            where T : class, IDromedaryComponent;

        IRouteBuilder To(Func<IDromedaryComponent, Task> configure, Type componentType);
        #endregion

        #region Process
        IRouteBuilder Process<T>()
            where T : IProcessor;
        IRouteBuilder Process(Type process);
        IRouteBuilder Processor(Action<IExchange> process);
        IRouteBuilder Processor(Func<IExchange, Task> process);
        #endregion

        #region Build
        IRoute Build();
        #endregion
    }
}
