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
    public class DefaultRouteGraphBuilderTest
    {
        private readonly Fixture _fixture;
        private readonly IRouteNodeFactory _factory;
        private readonly DefaultRouteGraphBuilder _builder;

        public DefaultRouteGraphBuilderTest()
        {
            _fixture = new Fixture();
            _factory = Substitute.For<IRouteNodeFactory>();
            _builder = new DefaultRouteGraphBuilder(_factory);
        }

        [Fact]
        public void Create_Should_Throw_When_FactoryIsNull()
            => Throws<ArgumentNullException>(() => new DefaultRouteGraphBuilder(null));

        
        [Fact]
        public void Add_Should_Throw_When_StatementIsNull()
            => Throws<ArgumentNullException>(() => _builder.Add(null));
        
        [Fact]
        public void Add_Should_Work_FromOnly()
        {
            var from = Substitute.For<IRouteNode>();
            var statement = Substitute.For<IStatement>();
            statement.Statement.Returns(Statement.From);
            
            _factory.Create(statement)
                .Returns(from);

            _builder
                .Add(statement);
        }
        
        [Fact]
        public void Add_Should_Throw_ToOnly()
        {
            var from = Substitute.For<IRouteNode>();
            var statement = Substitute.For<IStatement>();
            statement.Statement.Returns(Statement.To);
            
            _factory.Create(statement)
                .Returns(from);

            _builder
                .Add(statement);
        }
    }
}
