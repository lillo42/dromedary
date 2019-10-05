using AutoFixture;
using Dromedary.Factories;
using Dromedary.Generator;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Dromedary.Test.Factories
{
    public class ExchangeFactory
    {
        private readonly Fixture _fixture;
        private readonly IExchangeFactory _factory;
        private readonly IDromedaryContext _context;
        private readonly IExchangeIdGenerator _generator;

        public ExchangeFactory()
        {
            _fixture = new Fixture();
            _context = Substitute.For<IDromedaryContext>();
            _generator = Substitute.For<IExchangeIdGenerator>();
            _factory = new DefaultExchangeFactory(_context, _generator);
        }

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
