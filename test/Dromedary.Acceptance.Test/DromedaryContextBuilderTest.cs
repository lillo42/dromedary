using AutoFixture;
using Dromedary.Builder;
using Dromedary.Component.Console;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Dromedary.Acceptance.Test
{
    public class DromedaryContextBuilderTest
    {
        private readonly Fixture _fixture;

        public DromedaryContextBuilderTest()
        {
            _fixture = new Fixture();
        }
        
        [Fact]
        public void Build()
        {
            Host.CreateDefaultBuilder()
                .ConfigureDromedaryContext(builder =>
                {
                    builder.SetId(_fixture.Create<string>())
                        .SetName(_fixture.Create<string>())
                        .SetVersion(_fixture.Create<string>())
                        .AddConsoleComponent()
                        .AddRoute(route =>
                        {
                            route.SetId(_fixture.Create<string>())
                                .SetDescription(_fixture.Create<string>());
//                                .From<ConsoleComponent>(c => c.PromptMessage = "Enter something: ")
//                                .To<ConsoleComponent>(c => { });
                        });
                })
                .Build()
                .Run();
        }
    }
}
