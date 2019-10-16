using System.Threading.Tasks;
using Dromedary.Factories;
using FluentAssertions;
using Xunit;

namespace Dromedary.Test.Factories
{
    public class DefaultCommandFactoryTest
    {
        private readonly DefaultCommandFactory _factory;

        public DefaultCommandFactoryTest()
        {
            _factory = new DefaultCommandFactory();
        }

        [Fact]
        public void CreateCommand_Should_CreateNewInstance()
        {
            var result = _factory.CreateCommand(x => { }, typeof(object));
            result.Should().NotBeNull();
            result.ComponentType.Should().Be(typeof(object));
        }
        
        [Fact]
        public void CreateCommandWithExchange_Should_CreateNewInstance()
        {
            var result = _factory.CreateCommand((c, x) => { }, typeof(object));
            result.Should().NotBeNull();
            result.ComponentType.Should().Be(typeof(object));
        }

        [Fact]
        public void CreateCommandWithAction_Should_CreateNewInstance()
        {
            var result = _factory.CreateCommand<CustomComponent>(c => { });
            result.Should().NotBeNull();
            result.ComponentType.Should().Be(typeof(CustomComponent));
        }
        
        [Fact]
        public void CreateCommandWithActionAndExchange_Should_CreateNewInstance()
        {
            var result = _factory.CreateCommand<CustomComponent>((c, x) => { });
            result.Should().NotBeNull();
            result.ComponentType.Should().Be(typeof(CustomComponent));
        }
        
        [Fact]
        public void CreateCommandTask_Should_CreateNewInstance()
        {
            var result = _factory.CreateCommand(async x => { await Task.Delay(1); }, typeof(object));
            result.Should().NotBeNull();
            result.ComponentType.Should().Be(typeof(object));
        }
        
        [Fact]
        public void CreateCommandWithExchangeTask_Should_CreateNewInstance()
        {
            var result = _factory.CreateCommand(async (c, x) => { await Task.Delay(1);}, typeof(object));
            result.Should().NotBeNull();
            result.ComponentType.Should().Be(typeof(object));
        }

        [Fact]
        public void CreateCommandWithActionTask_Should_CreateNewInstance()
        {
            var result = _factory.CreateCommand<CustomComponent>(async c => { await Task.Delay(1); });
            result.Should().NotBeNull();
            result.ComponentType.Should().Be(typeof(CustomComponent));
        }
        
        [Fact]
        public void CreateCommandWithActionAndExchangeTask_Should_CreateNewInstance()
        {
            var result = _factory.CreateCommand<CustomComponent>(async (c, x) => { await Task.Delay(1); });
            result.Should().NotBeNull();
            result.ComponentType.Should().Be(typeof(CustomComponent));
        }

        internal class CustomComponent : IDromedaryComponent
        {
            public IEndpoint CreateEndpoint()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
