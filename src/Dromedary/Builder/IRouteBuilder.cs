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
        #endregion

        #region To
        IRouteBuilder To(string uri);

        IRouteBuilder To<T>()
            where T : class, IDromedaryComponent;
        
        IRouteBuilder To<T>(Action<T> configure)
            where T : class, IDromedaryComponent;

        IRouteBuilder To(Action<IDromedaryComponent> configure, Type componentType);
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
        IRouteBuilder Log(LogLevel level, string message);
        IRouteBuilder Log(LogLevel level, string message, params object[] args);
        IRouteBuilder Log(LogLevel level, Func<IExchange, string> message);
        #endregion
        
        #region Critical
        IRouteBuilder LogCritical(string message)
            => Log(LogLevel.Critical, message);
        IRouteBuilder LogCritical(string message, params object[] args)
            => Log(LogLevel.Critical, message, args);
        IRouteBuilder LogCritical(Func<IExchange, string> message)
            => Log(LogLevel.Critical, message);
        #endregion
        
        #region Debug
        IRouteBuilder LogDebug(string message)
            => Log(LogLevel.Debug, message);
        IRouteBuilder LogDebug(string message, params object[] args)
            => Log(LogLevel.Debug, message, args);
        IRouteBuilder LogDebug(Func<IExchange, string> message)
            => Log(LogLevel.Debug, message);
        #endregion
        
        #region Error
        IRouteBuilder LogError(string message)
            => Log(LogLevel.Error, message);
        IRouteBuilder LogError(string message, params object[] args)
            => Log(LogLevel.Error, message, args);
        IRouteBuilder LogError(Func<IExchange, string> message)
            => Log(LogLevel.Error, message);
        #endregion
        
        #region Information
        IRouteBuilder LogInformation(string message)
            => Log(LogLevel.Information, message);
        IRouteBuilder LogInformation(string message, params object[] args)
            => Log(LogLevel.Information, message, args);
        IRouteBuilder LogInformation(Func<IExchange, string> message)
            => Log(LogLevel.Information, message);
        #endregion
        
        #region None
        IRouteBuilder LogNone(string message)
            => Log(LogLevel.None, message);
        IRouteBuilder LogNone(string message, params object[] args)
            => Log(LogLevel.None, message, args);
        IRouteBuilder LogNone(Func<IExchange, string> message)
            => Log(LogLevel.None, message);
        #endregion
        
        #region Trace
        IRouteBuilder LogTrace(string message)
            => Log(LogLevel.Trace, message);
        IRouteBuilder LogTrace(LogLevel level, string message, params object[] args)
            => Log(LogLevel.Trace, message, args);
        IRouteBuilder LogTrace(LogLevel level, Func<IExchange, string> message)
            => Log(LogLevel.Trace, message);
        #endregion
        
        #region Warning
        IRouteBuilder LogWarning(string message)
            => Log(LogLevel.Warning, message);
        IRouteBuilder LogWarning(string message, params object[] args)
            => Log(LogLevel.Warning, message, args);
        IRouteBuilder LogWarning(Func<IExchange, string> message)
            => Log(LogLevel.Warning, message);
        #endregion
        
        #endregion

        #region Build
        IRoute Build();
        #endregion
    }
}
