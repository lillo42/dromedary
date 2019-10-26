using System;
using Dromedary.Builder;
using Dromedary.Exceptions;
using Dromedary.Factories;
using Dromedary.Statements;
using FluentAssertions;
using NSubstitute;
using Xunit;

using static Xunit.Assert;

namespace Dromedary.Test.Builder
{
    public class DefaultRouteGraphBuilderTest
    {
        private readonly IRouteNodeFactory _factory;
        private readonly DefaultRouteGraphBuilder _builder;

        public DefaultRouteGraphBuilderTest()
        {
            _factory = Substitute.For<IRouteNodeFactory>();
            _builder = new DefaultRouteGraphBuilder(_factory);
        }

        [Fact]
        public void Create_Should_Throw_When_FactoryIsNull()
            => Throws<ArgumentNullException>(() => new DefaultRouteGraphBuilder(null));


        [Fact]
        public void Add_Should_Work_When_FromOnly()
        {
            var from = Substitute.For<IRouteNode>();
            var statement = Substitute.For<IStatement>();
            statement.Statement.Returns(Statement.From);
            
            _factory.Create(statement)
                .Returns(from);

            var graph = _builder
                .Add(statement)
                .Build();

            graph.Should().NotBeNull();
            graph.Root.Should().NotBeNull();
            graph.Root.Should().Be(from);

            _factory
                .Received(1)
                .Create(statement);
        }

        [Fact]
        public void Add_Should_Throw_When_StatementIsNull()
            => Throws<ArgumentNullException>(() => _builder.Add(null));

        [Fact]
        public void Add_Should_Throw_When_ToOnly()
        {
            var to = Substitute.For<IRouteNode>();
            var statement = Substitute.For<IStatement>();
            statement.Statement.Returns(Statement.To);
            
            _factory.Create(statement)
                .Returns(to);

            Throws<DromedaryRouteGraphBuilderException>(() => _builder
                .Add(statement));
        }
        
        [Fact]
        public void Add_Should_Work_When_AddFromTo()
        {
            var @from = Substitute.For<IRouteNode>();
            var fromStatement = Substitute.For<IStatement>();
            fromStatement.Statement.Returns(Statement.From);
            
            _factory.Create(fromStatement)
                .Returns(@from);
            
            var to = Substitute.For<IRouteNode>();
            var toStatement = Substitute.For<IStatement>();
            toStatement.Statement.Returns(Statement.To);
            
            _factory.Create(toStatement)
                .Returns(to);

            var graph = _builder
                .Add(fromStatement)
                .Add(toStatement)
                .Build();
            
            graph.Should().NotBeNull();
            graph.Root.Should().NotBeNull();
            
            _factory
                .Received(1)
                .Create(fromStatement);
            
            _factory
                .Received(1)
                .Create(toStatement);

            @from
                .Received(1)
                .Add(to);
        }
    }
}
