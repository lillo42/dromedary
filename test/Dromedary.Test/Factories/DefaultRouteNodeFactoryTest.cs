using Dromedary.Factories;
using Dromedary.Statements;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Dromedary.Test.Factories
{
    public class DefaultRouteNodeFactoryTest
    {
        private readonly DefaultRouteNodeFactory _factory;

        public DefaultRouteNodeFactoryTest()
        {
            _factory = new DefaultRouteNodeFactory();
        }

        [Fact]
        public void Create_Should_ReturnNewInstance()
        {
            var statement = Substitute.For<IStatement>();
            
            var node = _factory.Create(statement);

            node.Should().NotBeNull();
            node.Statement.Should().Be(statement);
            node.Children.Should().NotBeNull();
            node.Children.Should().BeEmpty();
        }
    }
}
