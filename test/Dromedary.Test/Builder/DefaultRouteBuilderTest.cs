using System;
using AutoFixture;
using Dromedary.Builder;
using Dromedary.Factories;
using Dromedary.Statements;
using FluentAssertions;
using NSubstitute;
using Xunit;

using static Xunit.Assert;

namespace Dromedary.Test.Builder
{
    public class DefaultRouteBuilderTest
    {
        private readonly Fixture _fixture;
        private readonly IStatementFactory _statementFactory;
        private readonly IRouteGraphBuilder _graphBuilder;
        private readonly IRouteBuilder _builder;

        public DefaultRouteBuilderTest()
        {
            _fixture = new Fixture();
            
            _statementFactory = Substitute.For<IStatementFactory>();
            _graphBuilder = Substitute.For<IRouteGraphBuilder>();
            
            _builder = new DefaultRouteBuilder(_statementFactory,
                _graphBuilder);
        }

        [Fact]
        public void Create_Should_Throw_When_StatementFactoryIsNull() 
            => Throws<ArgumentNullException>(() => new DefaultRouteBuilder(null, null));
        
        [Fact]
        public void Create_Should_Throw_When_GraphBuilderIsNull() 
            => Throws<ArgumentNullException>(() => new DefaultRouteBuilder(_statementFactory, null));
        
        [Fact]
        public void Build_Should_SetId_When_IdIsSet()
        {
            var id = _fixture.Create<string>();
            var graph = Substitute.For<IRouteGraph>();
            
            _graphBuilder.Build()
                .Returns(graph);

            var route = _builder
                .SetId(id)
                .Build();

            route.Id.Should().NotBeNullOrEmpty();
            route.Id.Should().Be(id);
            
            route.Description.Should().BeNull();

            route.RouteGraph.Should().NotBeNull();
            route.RouteGraph.Should().Be(graph);

            _graphBuilder
                .Received(1)
                .Build();
        }
        
        [Fact]
        public void Build_Should_SetDescription_When_DescriptionIsSet()
        {
            var id = _fixture.Create<string>();
            var description = _fixture.Create<string>();
            var graph = Substitute.For<IRouteGraph>();
            
            _graphBuilder.Build()
                .Returns(graph);

            var route = _builder
                .SetId(id)
                .SetDescription(description)
                .Build();

            route.Id.Should().NotBeNullOrEmpty();
            route.Id.Should().Be(id);
            
            route.Description.Should().NotBeNullOrEmpty();
            route.Description.Should().Be(description);


            route.RouteGraph.Should().NotBeNull();
            route.RouteGraph.Should().Be(graph);

            _graphBuilder
                .Received(1)
                .Build();
        }

        [Fact]
        public void From_Should_AddFrom_When_CallFromAction()
        {
            var statement = Substitute.For<IStatement>();
            statement.Statement.Returns(Statement.From);

            _statementFactory.Create(Statement.From,Arg.Any<Action<IFakeComponent>>())
                .Returns(statement);

            _graphBuilder.Add(statement)
                .Returns(_graphBuilder);
            
            _builder
                .From<IFakeComponent>(component =>
                {
                    component.Text = _fixture.Create<string>();
                });

            
            _statementFactory
                .Received(1)
                .Create(Statement.From,Arg.Any<Action<IFakeComponent>>());

            _graphBuilder
                .Received(1)
                .Add(statement);
        }
        
        [Fact]
        public void From_Should_AddFrom_When_CallFromActionWithType()
        {
            var statement = Substitute.For<IStatement>();
            statement.Statement.Returns(Statement.From);

            _statementFactory.Create(Statement.From, typeof(IFakeComponent),Arg.Any<Action<IDromedaryComponent>>())
                .Returns(statement);

            _graphBuilder.Add(statement)
                .Returns(_graphBuilder);
            
            _builder
                .From(config => { }, typeof(IFakeComponent));

            _statementFactory
                .Received(1)
                .Create(Statement.From, typeof(IFakeComponent),Arg.Any<Action<IDromedaryComponent>>());

            _graphBuilder
                .Received(1)
                .Add(statement);
        }

        internal interface IFakeComponent : IDromedaryComponent
        {
            string Text { get; set; }
        }
        
        internal class FakeComponent : IFakeComponent
        {
            public string Text { get; set; }
            
            public IEndpoint CreateEndpoint()
            {
                throw new NotImplementedException();
            }
        }
    }
}
