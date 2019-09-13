using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dromedary
{
    public interface IDromedaryBuilder
    {
        IServiceCollection Service { get; }


        IDromedaryBuilder AddServiceProvider(IServiceCollection serviceCollection);
        IDromedaryBuilder AddLogging(Action<ILoggingBuilder> configure);
        
        IDromedaryBuilder AddComponent<TService>()
            where TService : class;
        
        IDromedaryBuilder AddComponent<TService, TImplementation>()
            where TImplementation : TService;
        
        IDromedary Build();
    }
}
