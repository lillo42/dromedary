using Dromedary.Component.Console;

namespace Dromedary.Builder
{
    public static class DromedaryContextBuilderExtensions
    {
        public static IDromedaryContextBuilder AddConsoleComponent(this IDromedaryContextBuilder builder)
        {
            builder.AddComponent<ConsoleComponent>();
            return builder;
        }
    }
}
