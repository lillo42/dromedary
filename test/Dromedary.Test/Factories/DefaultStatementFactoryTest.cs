using AutoFixture;
using Dromedary.Commands;
using Dromedary.Factories;
using Dromedary.Statements;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Dromedary.Test.Factories
{
    public class DefaultStatementFactoryTest
    {
        private readonly Fixture _fixture;
        private readonly DefaultStatementFactory _factory;

        public DefaultStatementFactoryTest()
        {
            _fixture = new Fixture();
            _factory = new DefaultStatementFactory();
        }

        [Fact]
        public void Create_Should_ReturnNewInstance()
        {
            var component = Substitute.For<IConfigureComponent>();
            var statement = _fixture.Create<Statement>();

            var state = _factory.Create(component, statement);

            state.Should().NotBeNull();
            state.Statement.Should().Be(statement);
            state.ConfigureComponent.Should().Be(component);
        }
    }
}
