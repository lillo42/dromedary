using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Dromedary.Builder;
using FluentAssertions;
using Xunit;

namespace Dromedary.Acceptance.Test
{
    public partial class DromedaryRouteAcceptanceTest
    {
        private readonly Fixture _fixture;
        private readonly IDromedaryContextBuilder _builder;

        public DromedaryRouteAcceptanceTest()
        {
            _fixture = new Fixture();
            _builder = new DefaultDromedaryContextBuilder();
            _builder.AddComponent<FakeComponent>();
        }

        private IDromedaryContext CreateContext()
        {
            return _builder
                .Build();
        }

        [Fact(Timeout = 1_000)]
        public async Task Execute_With_From_To()
        {
            var context = CreateContext();
            
            var from = new List<string>();
            var to = new List<string>();
            
            context
                .AddRoute(builder =>
                {
                    builder.From<FakeComponent>(c =>
                        {
                            c.Logs = from;
                            c.Text = _fixture.Create<string>();
                            c.MaxExchangeGenerated = 1;
                        })
                        .To<FakeComponent>(c =>
                        {
                            c.Logs = to;
                            c.Text = _fixture.Create<string>();
                        });
                });
            
            var source = new CancellationTokenSource();
            var task = context.ExecuteAsync(source.Token);
            
            source.CancelAfter(200);

            while (to.Count < 1)
            {
                await Task.Delay(1);
            }

            from.Should().HaveCount(1);
            to.Should().HaveCount(1);

            await task;
        }
        
        [Fact(Timeout = 1_000)]
        public async Task Execute_With_From_Process()
        {
            var context = CreateContext();
            
            var from = new List<string>();
            var log = new List<string>();
            
            context
                .AddRoute(builder =>
                {
                    builder.From<FakeComponent>(c =>
                        {
                            c.Logs = from;
                            c.Text = _fixture.Create<string>();
                            c.MaxExchangeGenerated = 1;
                        })
                        .Process(exchange =>
                        {
                            log.Add(_fixture.Create<string>());
                        });
                });
            
            var source = new CancellationTokenSource();
            var task = context.ExecuteAsync(source.Token);
            
            source.CancelAfter(200);

            while (log.Count < 1)
            {
                await Task.Delay(1);
            }

            from.Should().HaveCount(1);
            log.Should().HaveCount(1);

            await task;
        }
    }
}
