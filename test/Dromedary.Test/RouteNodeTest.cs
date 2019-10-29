using System;
using System.Collections.Generic;
using Dromedary.Statements;
using FluentAssertions;
using NSubstitute;
using Xunit;
using static Xunit.Assert;

namespace Dromedary.Test
{
    public class RouteNodeTest
    {
        [Fact]
        public void CreateInstance_Should_Throw_When_StatementsIsNull() 
            => Throws<ArgumentNullException>(() => new DefaultRouteNode(null));

        [Fact]
        public void Add()
        {
            var statement = Substitute.For<IStatement>();
            var node = new DefaultRouteNode(statement);

            node.Children.Should().BeEmpty();

            var node2 = Substitute.For<IRouteNode>();
            node.Add(node2);
            
            node.Children.Should().NotBeEmpty();
            node.Children.Should().HaveCount(1);
            
            var node3 = Substitute.For<IRouteNode>();
            node.Add(node3);
            
            node.Children.Should().HaveCount(2);
            node.Children.Should().BeEquivalentTo(new List<IRouteNode>
            {
                node2,
                node3
            });
        }
    }
}
