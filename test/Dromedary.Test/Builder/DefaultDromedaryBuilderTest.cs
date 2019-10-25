using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Dromedary.Builder;
using FluentAssertions;
using Xunit;

namespace Dromedary.Test.Builder
{
    public class DefaultDromedaryBuilderTest
    {
        private readonly Fixture _fixture;
        private readonly IDromedaryContextBuilder _contextBuilder;

        public DefaultDromedaryBuilderTest()
        {
            _fixture = new Fixture();
            _contextBuilder = new DefaultDromedaryContextBuilder();
        }

        [Fact]
        public void Build_Should_ReturnNewInstance_When_NothingIsSet()
        {
            var context = _contextBuilder.Build();
            
            context.Should().NotBeNull();
            
            context.Id.Should().NotBeNullOrEmpty();
            
            context.Name.Should().BeNull();
            
            context.Version.Should().NotBeNullOrEmpty();
            context.Version.Should().Be("1.0.0");
            
            context.Routes.Should().NotBeNull();
            context.Routes.Should().BeEmpty();
            
            context.Service.Should().NotBeNull();
            context.UpTime.Should().BeLessThan(TimeSpan.FromSeconds(1));
        }
        
        [Fact]
        public void Build_Should_ReturnNewInstance_When_IdIsSet()
        {
            var id = _fixture.Create<string>();
            var context = _contextBuilder
                .SetId(id)
                .Build();
            context.Should().NotBeNull();
            
            context.Id.Should().NotBeNullOrEmpty();
            context.Id.Should().Be(id);
            
            context.Name.Should().BeNull();
            
            context.Version.Should().NotBeNullOrEmpty();
            context.Version.Should().Be("1.0.0");
            
            context.Routes.Should().NotBeNull();
            context.Routes.Should().BeEmpty();
            
            context.Service.Should().NotBeNull();
            context.UpTime.Should().BeLessThan(TimeSpan.FromSeconds(1));
        }
        
        [Fact]
        public void Build_Should_ReturnNewInstance_When_NameIsSet()
        {
            var id = _fixture.Create<string>();
            var name = _fixture.Create<string>();
            
            var context = _contextBuilder
                .SetId(id)
                .SetName(name)
                .Build();
            
            context.Should().NotBeNull();
            
            context.Id.Should().NotBeNullOrEmpty();
            context.Id.Should().Be(id);
            
            context.Name.Should().NotBeNull();
            context.Name.Should().Be(name);
            
            context.Version.Should().NotBeNullOrEmpty();
            context.Version.Should().Be("1.0.0");
            
            context.Routes.Should().NotBeNull();
            context.Routes.Should().BeEmpty();
            
            context.Service.Should().NotBeNull();
            context.UpTime.Should().BeLessThan(TimeSpan.FromSeconds(1));
        }
        
        [Fact]
        public void Build_Should_ReturnNewInstance_When_VersionIsSet()
        {
            var id = _fixture.Create<string>();
            var name = _fixture.Create<string>();
            var version = _fixture.Create<string>();
            
            var context = _contextBuilder
                .SetId(id)
                .SetName(name)
                .SetVersion(version)
                .Build();
            
            context.Should().NotBeNull();
            
            context.Id.Should().NotBeNullOrEmpty();
            context.Id.Should().Be(id);
            
            context.Name.Should().NotBeNull();
            context.Name.Should().Be(name);

            context.Version.Should().NotBeNullOrEmpty();
            context.Version.Should().Be(version);
            
            
            context.Routes.Should().NotBeNull();
            context.Routes.Should().BeEmpty();
            
            context.Service.Should().NotBeNull();
            context.UpTime.Should().BeLessThan(TimeSpan.FromSeconds(1));
        }
        
        private interface IFakeComponent : IDromedaryComponent
        {
            
        }
        
        private class FakeComponent : IFakeComponent
        {
            public IEndpoint CreateEndpoint()
            {
                throw new NotImplementedException();
            }

            public void ConfigureProperties(Action<IDromedaryComponent> config)
            {
                
            }

            public Task ConfigurePropertiesAsync(Func<IDromedaryComponent, Task> config)
            {
                throw new NotImplementedException();
            }

            public void ConfigureProperties(IDictionary<string, object> config)
            {
                throw new NotImplementedException();
            }
        }
    }
}
