using Dromedary.Builder;
using Dromedary.Component.Console;
using Microsoft.Extensions.Hosting;

namespace Dromedary.Sample.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureDromedaryContext(d => d
                    .SetId("Console-1")
                    .SetName("Console")
                    .AddConsoleComponent()
                    .AddRoute(r =>
                    {
                        r.From<ConsoleComponent>(c => c.PromptMessage = "Text message:")
                            .Process(e => e.Message.Body = e.Message.Body.ToString().ToUpper())
                            .To<ConsoleComponent>();
                    }))
                .Build()
                .Run();
        }
    }
}
