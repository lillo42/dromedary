using Dromedary.Builder;
using Dromedary.Component.Console;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dromedary.Sample.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureServices(service =>
                {
                    service.AddDromedary()
                        .AddConsoleComponent()
                        .AddRoute(r =>
                        {
                            r.AllowSynchronousContinuations(true)
                                .From<ConsoleComponent>(c => c.PromptMessage = "Text message:")
                                .Process(e => e.Message.Body = e.Message.Body.ToString().ToUpper())
                                .To<ConsoleComponent>();
                        });
                })
                .Build()
                .Run();
        }
    }
}
