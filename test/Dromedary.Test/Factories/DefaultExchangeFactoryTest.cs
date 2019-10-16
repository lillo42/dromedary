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
    public class DefaultExchangeFactoryTest
    {
        private readonly Fixture _fixture;
        private readonly DefaultExchangeFactory _factory;
        private readonly IExchangeIdGenerator _generator;
        private readonly IMessageFactory _messageFactory;

        public DefaultExchangeFactoryTest()
        {
            _fixture = new Fixture();
            _generator = Substitute.For<IExchangeIdGenerator>();
            _messageFactory = Substitute.For<IMessageFactory>();
            _factory = new DefaultExchangeFactory( _generator, _messageFactory);
        }

        [Fact]
        public void Constructor_Should_Throw_When_GeneratorIsNull() 
            => Throws<ArgumentNullException>(() => new DefaultExchangeFactory(null, _messageFactory));
        
        [Fact]
        public void Constructor_Should_Throw_When_MessageFactoryIsNull() 
            => Throws<ArgumentNullException>(() => new DefaultExchangeFactory(_generator, null));

        [Fact]
        public void Create_Should_ReturnNewInstance()
        {
            var id = _fixture.Create<string>();
            
            _generator.Generate()
                .Returns(id);
            
            var message = Substitute.For<IMessage>();
            _messageFactory.Create(Arg.Any<IExchange>())
                .Returns(message);
            
            var result = _factory.Create();
            
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.Message.Should().Be(message);

            _generator
                .Received(1)
                .Generate();

            _messageFactory
                .Received(1)
                .Create(Arg.Any<IExchange>());
        }
    }
}
