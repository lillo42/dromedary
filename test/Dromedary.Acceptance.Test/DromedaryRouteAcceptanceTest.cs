using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Dromedary.Acceptance.Test
{
    public partial class DromedaryRouteAcceptanceTest
    {
        private readonly Fixture _fixture;
        private readonly IHostBuilder _hostBuilder;

        public DromedaryRouteAcceptanceTest()
        {
            _fixture = new Fixture();
            _hostBuilder = Host.CreateDefaultBuilder()
                .ConfigureServices(service =>
                {
                    service
                        .AddDromedary()
                        .AddComponent<FakeComponent>();
                });
        }

        [Fact(Timeout = 1_000)]
        public async Task Execute_With_From_To()
        {
            var completionFrom = new TaskCompletionSource<object>();
            var completionTo = new TaskCompletionSource<object>();

            var host = _hostBuilder.ConfigureServices(service =>
            {
                service
                    .AddRoute(builder =>
                    {
                        builder
                            .AllowSynchronousContinuations(true)
                            .From<FakeComponent>(c =>
                            {
                                c.CompletionSource = completionFrom;
                                c.Object = _fixture.Create<string>();
                                c.MaxExchangeGenerated = 1;
                            })
                            .To<FakeComponent>(c =>
                            {
                                c.CompletionSource = completionTo;
                                c.Object = _fixture.Create<string>();
                            });
                    });
            }).Build();
            
            
            var source = new CancellationTokenSource();
            host.StartAsync(source.Token);
            source.CancelAfter(200);

            await completionFrom.Task;
            await completionTo.Task;

            await host.StopAsync();
        }
        
//        [Fact(Timeout = 1_000)]
//        public async Task Execute_With_From_Process()
//        {
//            var from = new List<string>();
//            var log = new List<string>();
//            
//            var host = _hostBuilder.ConfigureServices(service =>
//            {
//                service.AddRoute(builder =>
//                {
//                    builder.From<FakeComponent>(c =>
//                        {
//                            c.CompletionSource = from;
//                            c.Text = _fixture.Create<string>();
//                            c.MaxExchangeGenerated = 1;
//                        })
//                        .Process(exchange =>
//                        {
//                            log.Add(_fixture.Create<string>());
//                        });
//                });
//            }).Build();
//            
//            var source = new CancellationTokenSource();
//            host.StartAsync(source.Token);
//            
//            source.CancelAfter(200);
//
//            while (log.Count < 1)
//            {
//                await Task.Delay(1);
//            }
//
//            from.Should().HaveCount(1);
//            log.Should().HaveCount(1);
//
//            await host.StopAsync();
//        }
    }
}
