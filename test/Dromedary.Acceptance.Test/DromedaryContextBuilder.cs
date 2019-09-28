using System;
using AutoFixture;
using Dromedary.Builder;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Dromedary.Acceptance.Test
{
    public class DromedaryContextBuilder
    {
        private readonly Fixture _fixture;

        public DromedaryContextBuilder()
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
                        .AddRoute(route =>
                        {
                            route.SetId(_fixture.Create<string>())
                                .SetDescription(_fixture.Create<string>())
                                .From("")
                                .To("");
                        });
                })
                .Build()
                .Run();
        }
    }
}
