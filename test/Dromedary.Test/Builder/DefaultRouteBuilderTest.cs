using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Dromedary.Builder;
using Dromedary.Commands;
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
        private readonly IDromedaryContext _context;
        private readonly ICommandFactory _commandFactory;
        private readonly IStatementFactory _statementFactory;
        private readonly IRouteGraphBuilder _graphBuilder;
        private readonly IRouteBuilder _builder;

        public DefaultRouteBuilderTest()
        {
            _fixture = new Fixture();
            
            _context = Substitute.For<IDromedaryContext>();
            _commandFactory = Substitute.For<ICommandFactory>();
            _statementFactory = Substitute.For<IStatementFactory>();
            _graphBuilder = Substitute.For<IRouteGraphBuilder>();
            
            _builder = new DefaultRouteBuilder(_context, 
                _commandFactory,
                _statementFactory,
                _graphBuilder);
        }

        [Fact]
        public void Create_Should_Throw_When_ContextIsNull() 
            => Throws<ArgumentNullException>(() => new DefaultRouteBuilder(null, null,
                null, null));
        
        [Fact]
        public void Create_Should_Throw_When_CommandFactoryIsNull() 
            => Throws<ArgumentNullException>(() => new DefaultRouteBuilder(_context, null,
                null, null));
        
        [Fact]
        public void Create_Should_Throw_When_StatementFactoryIsNull() 
            => Throws<ArgumentNullException>(() => new DefaultRouteBuilder(_context, _commandFactory,
                null, null));
        
        [Fact]
        public void Create_Should_Throw_When_GraphBuilderIsNull() 
            => Throws<ArgumentNullException>(() => new DefaultRouteBuilder(_context, _commandFactory,
                _statementFactory, null));
        
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

            route.Context.Should().NotBeNull();
            route.Context.Should().Be(_context);

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

            route.Context.Should().NotBeNull();
            route.Context.Should().Be(_context);

            route.RouteGraph.Should().NotBeNull();
            route.RouteGraph.Should().Be(graph);

            _graphBuilder
                .Received(1)
                .Build();
        }

        [Fact]
        public void From_Should_AddFrom_When_CallFromAction()
        {
            var command = Substitute.For<IConfigureComponent<IFakeComponent>>();
            _commandFactory.CreateCommand(Arg.Any<Action<IFakeComponent>>())
                .Returns(command);

            var statement = Substitute.For<IStatement>();
            statement.Statement.Returns(Statement.From);

            _statementFactory.Create(command, Statement.From)
                .Returns(statement);

            _graphBuilder.Add(statement)
                .Returns(_graphBuilder);
            
            _builder
                .From<IFakeComponent>(component =>
                {
                    component.Text = _fixture.Create<string>();
                });

            _commandFactory
                .Received(1)
                .CreateCommand(Arg.Any<Action<IFakeComponent>>());
            
            _statementFactory
                .Received(1)
                .Create(command, Statement.From);

            _graphBuilder
                .Received(1)
                .Add(statement);
        }
        
        [Fact]
        public void From_Should_AddFrom_When_CallFromActionWithExchange()
        {
            var command = Substitute.For<IConfigureComponent<IFakeComponent>>();
            _commandFactory.CreateCommand(Arg.Any<Action<IFakeComponent, IExchange>>())
                .Returns(command);

            var statement = Substitute.For<IStatement>();
            statement.Statement.Returns(Statement.From);

            _statementFactory.Create(command, Statement.From)
                .Returns(statement);

            _graphBuilder.Add(statement)
                .Returns(_graphBuilder);
            
            _builder
                .From<IFakeComponent>((component, exchange) =>
                {
                    component.Text = _fixture.Create<string>();
                });

            _commandFactory
                .Received(1)
                .CreateCommand(Arg.Any<Action<IFakeComponent, IExchange>>());
            
            _statementFactory
                .Received(1)
                .Create(command, Statement.From);

            _graphBuilder
                .Received(1)
                .Add(statement);
        }
        
        [Fact]
        public void From_Should_AddFrom_When_CallFromActionWithType()
        {
            var command = Substitute.For<IConfigureComponent>();
            _commandFactory.CreateCommand(Arg.Any<Action<IDromedaryComponent>>(), typeof(IFakeComponent))
                .Returns(command);

            var statement = Substitute.For<IStatement>();
            statement.Statement.Returns(Statement.From);

            _statementFactory.Create(command, Statement.From)
                .Returns(statement);

            _graphBuilder.Add(statement)
                .Returns(_graphBuilder);

            Action<IDromedaryComponent> config = (c) => { };
            _builder
                .From(config, typeof(IFakeComponent));

            _commandFactory
                .Received(1)
                .CreateCommand(Arg.Any<Action<IDromedaryComponent>>(), typeof(IFakeComponent));
            
            _statementFactory
                .Received(1)
                .Create(command, Statement.From);

            _graphBuilder
                .Received(1)
                .Add(statement);
        }
        
        [Fact]
        public void From_Should_AddFrom_When_CallFromActionWithExchangeAndType()
        {
            var command = Substitute.For<IConfigureComponent>();
            _commandFactory.CreateCommand(Arg.Any<Action<IDromedaryComponent, IExchange>>(), typeof(IFakeComponent))
                .Returns(command);

            var statement = Substitute.For<IStatement>();
            statement.Statement.Returns(Statement.From);

            _statementFactory.Create(command, Statement.From)
                .Returns(statement);

            _graphBuilder.Add(statement)
                .Returns(_graphBuilder);

            Action<IDromedaryComponent, IExchange> config = (c, e) => { };
            _builder
                .From(config, typeof(IFakeComponent));

            _commandFactory
                .Received(1)
                .CreateCommand(Arg.Any<Action<IDromedaryComponent, IExchange>>(), typeof(IFakeComponent));
            
            _statementFactory
                .Received(1)
                .Create(command, Statement.From);

            _graphBuilder
                .Received(1)
                .Add(statement);
        }
        
        [Fact]
        public void From_Should_AddFrom_When_CallFromFunc()
        {
            var command = Substitute.For<IConfigureComponent<IFakeComponent>>();
            _commandFactory.CreateCommand(Arg.Any<Func<IFakeComponent, Task>>())
                .Returns(command);

            var statement = Substitute.For<IStatement>();
            statement.Statement.Returns(Statement.From);

            _statementFactory.Create(command, Statement.From)
                .Returns(statement);

            _graphBuilder.Add(statement)
                .Returns(_graphBuilder);
            
            _builder
                .From<IFakeComponent>(component =>
                {
                    component.Text = _fixture.Create<string>();
                    
                    return Task.CompletedTask;
                });

            _commandFactory
                .Received(1)
                .CreateCommand(Arg.Any<Action<IFakeComponent>>());
            
            _statementFactory
                .Received(1)
                .Create(command, Statement.From);

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
            public FakeComponent(IDromedaryContext context)
            {
                Context = context;
            }

            public string Text { get; set; }

            public IDromedaryContext Context { get; }
            
            public IEndpoint CreateEndpoint()
            {
                throw new NotImplementedException();
            }
        }
    }
}
