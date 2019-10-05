using Dromedary.Generator;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Dromedary.Test.Generator
{
    public class MessageIdGeneratorTest
    {
        private readonly IMessageIdGenerator _generator;

        public MessageIdGeneratorTest()
        {
            _generator = new DefaultIdGenerator();
        }
        
        [Fact]
        public void Generate_Should_ReturnNewId()
        {
            var exchange = Substitute.For<IExchange>();
            var id = _generator.Generate(exchange);
            id.Should().NotBeNullOrEmpty();
            
            var id1 = _generator.Generate(exchange);
            id1.Should().NotBeNullOrEmpty();
            id.Should().NotBe(id1);
        }
    }
}
