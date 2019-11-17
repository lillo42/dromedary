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
        IRouteBuilder Log(LogLevel level);

        IRouteBuilder Log(LogLevel level, string message);

        IRouteBuilder Log(LogLevel level, string message, params object[] args);

        IRouteBuilder Log(LogLevel level, Func<IExchange, string> message);

        IRouteBuilder Log(LogLevel level, Func<IExchange, Task<string>> message);

        #region Trace

        IRouteBuilder LogTrace()
        {
            return Log(LogLevel.Trace);
        } 

        IRouteBuilder LogTrace(LogLevel level, string message)
            => Log(LogLevel.Trace, message);

        IRouteBuilder LogTrace(LogLevel level, string message, params object[] args)
            => Log(LogLevel.Trace, message, args);

        IRouteBuilder LogTrace(LogLevel level, Func<IExchange, string> message)
            => Log(LogLevel.Trace, message);

        IRouteBuilder LogTrace(LogLevel level, Func<IExchange, Task<string>> message)
            => Log(LogLevel.Trace, message);

        #endregion

        #region Debug
        IRouteBuilder LogDebug()
            => Log(LogLevel.Debug);

        IRouteBuilder LogDebug(LogLevel level, string message)
            => Log(LogLevel.Debug, message);

        IRouteBuilder LogDebug(LogLevel level, string message, params object[] args)
            => Log(LogLevel.Debug, message, args);

        IRouteBuilder LogDebug(LogLevel level, Func<IExchange, string> message)
            => Log(LogLevel.Debug, message);

        IRouteBuilder LogDebug(LogLevel level, Func<IExchange, Task<string>> message)
            => Log(LogLevel.Debug, message);

        #endregion

        #region Warning

        IRouteBuilder LogWarning()
            => Log(LogLevel.Warning);

        IRouteBuilder LogWarning(LogLevel level, string message)
            => Log(LogLevel.Warning, message);

        IRouteBuilder LogWarning(LogLevel level, string message, params object[] args)
            => Log(LogLevel.Warning, message, args);

        IRouteBuilder LogWarning(LogLevel level, Func<IExchange, string> message)
            => Log(LogLevel.Warning, message);

        IRouteBuilder LogWarning(LogLevel level, Func<IExchange, Task<string>> message)
            => Log(LogLevel.Warning, message);

        #endregion

        #region Error
        IRouteBuilder LogError()
            => Log(LogLevel.Error);

        IRouteBuilder LogError(LogLevel level, string message)
            => Log(LogLevel.Error, message);

        IRouteBuilder LogError(LogLevel level, string message, params object[] args)
            => Log(LogLevel.Error, message, args);

        IRouteBuilder LogError(LogLevel level, Func<IExchange, string> message)
            => Log(LogLevel.Error, message);

        IRouteBuilder LogError(LogLevel level, Func<IExchange, Task<string>> message)
            => Log(LogLevel.Error, message);

        #endregion

        #region Critial
        IRouteBuilder LogCritical()
            => Log(LogLevel.Critical);

        IRouteBuilder LogCritical(LogLevel level, string message)
            => Log(LogLevel.Critical, message);

        IRouteBuilder LogCritical(LogLevel level, string message, params object[] args)
            => Log(LogLevel.Critical, message, args);

        IRouteBuilder LogCritical(LogLevel level, Func<IExchange, string> message)
            => Log(LogLevel.Critical, message);

        IRouteBuilder LogCritical(LogLevel level, Func<IExchange, Task<string>> message)
            => Log(LogLevel.Critical, message);

        #endregion

        #endregion

        #region Build
        IRoute Build();
        #endregion
    }
}
