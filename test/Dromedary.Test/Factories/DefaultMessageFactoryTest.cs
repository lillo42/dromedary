using System;
using AutoFixture;
using Dromedary.Factories;
using Dromedary.Generator;
using FluentAssertions;
using NSubstitute;
using Xunit;
using static Xunit.Assert;

namespace Dromedary.Test.Factories
{
    public class DefaultMessageFactoryTest
    {
        private readonly Fixture _fixture;
        private readonly IMessageIdGenerator _generator;
        private readonly DefaultMessageFactory _factory;

        public DefaultMessageFactoryTest()
        {
            _fixture = new Fixture();
            _generator = Substitute.For<IMessageIdGenerator>();
            _factory = new DefaultMessageFactory(_generator);
        }

        [Fact]
        public void Constructor_Should_Throw_When_IMessageIdGeneratorIsNull() 
            => Throws<ArgumentNullException>(() => new DefaultMessageFactory(null));

        [Fact]
        public void Create_Should_ReturnNewInstance()
        {
            var id = _fixture.Create<string>();
            var exchange = Substitute.For<IExchange>();
            _generator
                .Generate(exchange)
                .Returns(id);

            var message = _factory.Create(exchange);

            message.Should().NotBeNull();
            message.Id.Should().Be(id);
            message.Exchange.Should().Be(exchange);

            _generator
                .Received(1)
                .Generate(exchange);
        }
    }
}
