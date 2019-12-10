using System;
using Dromedary.Factories;
using FluentAssertions;
using NSubstitute;
using Xunit;
using static Xunit.Assert;

namespace Dromedary.Test.Factories
{
    public class DefaultChannelFactoryTest
    {
        private readonly DefaultChannelFactory _factory;

        public DefaultChannelFactoryTest()
        {
            _factory = new DefaultChannelFactory(Substitute.For<IServiceProvider>());
        }

        [Fact]
        public void Constructor_Should_Throw_When_ServiceProviderIsNull() 
            => Throws<ArgumentNullException>(() => new DefaultChannelFactory(null));

        [Fact]
        public void Create_Should_ReturnCreateChannel()
        {
            var channel =  _factory.Create(Substitute.For<IRouteGraph>());
            channel.Should().NotBeNull();
        }
    }
}
