using Dromedary.Generator;
using FluentAssertions;
using Xunit;

namespace Dromedary.Test.Generator
{
    public class ExchangeIdGeneratorTest
    {
        private readonly IExchangeIdGenerator _generator;

        public ExchangeIdGeneratorTest()
        {
            _generator = new DefaultIdGenerator();
        }

        [Fact]
        public void Generate_Should_ReturnNewId()
        {
            var id = _generator.Generate();
            id.Should().NotBeNullOrEmpty();
            
            var id1 = _generator.Generate();
            id1.Should().NotBeNullOrEmpty();
            id.Should().NotBe(id1);
        }
    }
}
