using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Dromedary.Builder
{
    public interface IRouteBuilder
    {
        #region Configure
        IRouteBuilder SetId(string id);
        IRouteBuilder SetDescription(string description);
        IRouteBuilder AllowSynchronousContinuations(bool allow);
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
        IRouteBuilder Process(Action<IExchange> process);
        IRouteBuilder Process(Func<IExchange, Task> process);
        #endregion

        #region Log

        #region Level
        IRouteBuilder Log(LogLevel level);
        IRouteBuilder Log(LogLevel level, string message);
        IRouteBuilder Log(LogLevel level, string message, params object[] args);
        IRouteBuilder Log(LogLevel level, Func<IExchange, string> message);
        #endregion
        
        #endregion

        #region Build
        IRoute Build();
        #endregion
    }
}
