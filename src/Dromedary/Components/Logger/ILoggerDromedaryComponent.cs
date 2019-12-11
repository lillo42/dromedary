using System;
using Microsoft.Extensions.Logging;

namespace Dromedary.Components.Logger
{
    public interface ILoggerDromedaryComponent : IDromedaryComponent
    {
        string? Message { get; set; }
        Func<IExchange, string>? MessageFactory { get; set; }
        object[] Args { get; set; }
        LogLevel LogLevel { get; set; }
    }
}
