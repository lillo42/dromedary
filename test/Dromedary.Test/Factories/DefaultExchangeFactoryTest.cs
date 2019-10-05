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
        private readonly IDromedaryContext _context;
        private readonly IExchangeIdGenerator _generator;

        public DefaultExchangeFactoryTest()
        {
            _fixture = new Fixture();
            _context = Substitute.For<IDromedaryContext>();
            _generator = Substitute.For<IExchangeIdGenerator>();
            _factory = new DefaultExchangeFactory(_context, _generator);
        }

        [Fact]
        public void Constructor_Should_Throw_When_ContextIsNull() 
            => Throws<ArgumentNullException>(() => new DefaultExchangeFactory(null, _generator));
        
        [Fact]
        public void Constructor_Should_Throw_When_GeneratorIsNull() 
            => Throws<ArgumentNullException>(() => new DefaultExchangeFactory(_context, null));

        [Fact]
        public void Create_Should_ReturnNewInstance()
        {
            var id = _fixture.Create<string>();
            
            _generator.Generate()
                .Returns(id);
            
            var result = _factory.Create();
            
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.Context.Should().Be(_context);

            _generator
                .Received(1)
                .Generate();
        }
    }
}
