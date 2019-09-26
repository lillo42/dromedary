using AutoFixture;
using Dromedary.Factories;
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

        public ExchangeFactory()
        {
            _fixture = new Fixture();
            _context = Substitute.For<IDromedaryContext>();
            _factory = new DefaultExchangeFactory();
        }

        [Fact]
        public void Create_Should_ReturnNewInstance()
        {
            var id = _fixture.Create<string>();
            var result = _factory.Create(id, _context);
            
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.Context.Should().Be(_context);
        }
        
    }
}
