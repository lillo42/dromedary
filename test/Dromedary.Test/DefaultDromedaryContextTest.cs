using System;
using AutoFixture;
using Dromedary.Builder;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

using static Xunit.Assert;

namespace Dromedary.Test
{
    public class DefaultDromedaryContextTest
    {
        private readonly Fixture _fixture;
        private readonly DefaultDromedaryContext _context;
        private readonly IServiceProvider _provider;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IServiceScope _scope;
 
        public DefaultDromedaryContextTest()
        {
            _fixture = new Fixture();
            
            _provider = Substitute.For<IServiceProvider>();
            _scopeFactory = Substitute.For<IServiceScopeFactory>();
            
            _scope = Substitute.For<IServiceScope>();
            _scope.ServiceProvider
                .Returns(_provider);
            
            _provider.GetService(typeof(IServiceScopeFactory))
                .Returns(_scopeFactory);

            _scopeFactory.CreateScope()
                .Returns(_scope);
            
            _context = new DefaultDromedaryContext(_fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _provider);
        }

        [Fact]
        public void New_Should_Throw_When_IdIsNull() 
            => Throws<ArgumentNullException>(() => new DefaultDromedaryContext(null, null, 
                null, null));
        
        [Fact]
        public void New_Should_Throw_When_ServiceIsNull() 
            => Throws<ArgumentNullException>(() => new DefaultDromedaryContext(_fixture.Create<string>(), 
                null, null, null));

        [Fact]
        public void AddRoute_Should_Throw_When_ParameterIsNull() 
            => Throws<ArgumentNullException>(() => _context.AddRoute(null));

        [Fact]
        public void AddRoute_Should_CreateRoute_When_ParameterIsNotNull()
        {
            var resolver = Substitute.For<IResolverDromedaryContext>();
            IDromedaryContext context = null;
            resolver.Context = Arg.Do<IDromedaryContext>(arg => context = arg);

            _provider.GetService(typeof(IResolverDromedaryContext))
                .Returns(resolver);

            var builder = Substitute.For<IRouteBuilder>();
            _provider.GetService(typeof(IRouteBuilder))
                .Returns(builder);

            _context.AddRoute(b =>
            {
                b.Should().NotBeNull();
                b.Should().Be(builder);
            });

            _context.Routes.Should().NotBeNull();
            _context.Routes.Should().NotBeNullOrEmpty();
            _context.Routes.Should().HaveCount(1);

            context.Should().NotBeNull();
            context.Should().Be(_context);
        }
    }
}
