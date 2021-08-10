using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Graph;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests.Graph
{
    [TestFixture]
    public class DirectedWeightedGraphTests
    {
        [Test]
        public void GraphAddVertexTest_Success()
        {
            var graph = new DirectedWeightedGraph<char>();

            graph.AddVertex('A');
            graph.AddVertex('B');
            graph.AddVertex('C');

            graph.Count.Should().Be(3);
        }

        [Test]
        public void GraphAddVertexTest_ShouldThrowOverflow()
        {
            var graph = new DirectedWeightedGraph<char>();
            for (var i = 0; i < 10; i++)
            {
                graph.AddVertex('A');
            }

            Action addOverflow = () => graph.AddVertex('A');

            graph.Count.Should().Be(10);
            graph.Vertices.Should().OnlyContain(x => x.Data == 'A');
            addOverflow.Should().Throw<OverflowException>()
                .WithMessage("Graph overflow.");
        }

        [Test]
        public void GraphRemoveVertexTest_Success()
        {
            var graph = new DirectedWeightedGraph<char>();
            var vertexA = graph.AddVertex('A');
            var vertexB = graph.AddVertex('B');
            var vertexC = graph.AddVertex('C');
            graph.AddEdge(vertexB, vertexA, 5);
            graph.AddEdge(vertexC, vertexA, 5);
            var adjacentBa = graph.AreAdjacent(vertexB, vertexA);
            var adjacentCa = graph.AreAdjacent(vertexC, vertexA);

            graph.RemoveVertex(vertexA);
            vertexA.SetGraph(graph);

            adjacentBa.Should().BeTrue();
            adjacentCa.Should().BeTrue();
            graph.AreAdjacent(vertexB, vertexA).Should().BeFalse();
            graph.AreAdjacent(vertexC, vertexA).Should().BeFalse();
        }

        [Test]
        public void GraphRemoveVertexTest_ShouldThrowVertexNotInGraph()
        {
            var graph = new DirectedWeightedGraph<char>();
            var vertexA = new Vertex<char>('A', 0);

            Action removeVertex = () => graph.RemoveVertex(vertexA);

            removeVertex.Should().Throw<InvalidOperationException>()
                .WithMessage($"Vertex does not belong to graph: {vertexA}.");
        }

        [Test]
        public void GraphAddEdgeTest_Success()
        {
            var graph = new DirectedWeightedGraph<char>();
            var vertexA = graph.AddVertex('A');
            var vertexB = graph.AddVertex('B');
            var vertexC = graph.AddVertex('C');

            graph.AddEdge(vertexA, vertexB, 5);

            graph.AreAdjacent(vertexA, vertexB).Should().BeTrue();
            graph.AreAdjacent(vertexA, vertexC).Should().BeFalse();
        }

        [Test]
        public void GraphAddEdgeTest_ShouldThrowZeroWeight()
        {
            var graph = new DirectedWeightedGraph<char>();
            var vertexA = graph.AddVertex('A');
            var vertexB = graph.AddVertex('B');

            Action addZeroEdge = () => graph.AddEdge(vertexA, vertexB, 0);

            addZeroEdge.Should().Throw<InvalidOperationException>()
                .WithMessage("Edge weight cannot be zero.");
        }

        [Test]
        public void GraphAddEdgeTest_ShouldThrowVertexNotInGraph()
        {
            var graph = new DirectedWeightedGraph<char>();
            var vertexA = graph.AddVertex('A');
            var vertexB = new Vertex<char>('B', 1);

            Action addZeroEdge = () => graph.AddEdge(vertexA, vertexB, 0);

            addZeroEdge.Should().Throw<InvalidOperationException>()
                .WithMessage($"Vertex does not belong to graph: {vertexB}.");
        }

        [Test]
        public void GraphAddEdgeTest_ShouldThrowEdgeExists()
        {
            var graph = new DirectedWeightedGraph<char>();
            var vertexA = graph.AddVertex('A');
            var vertexB = graph.AddVertex('B');
            const int currentEdgeWeight = 5;
            graph.AddEdge(vertexA, vertexB, currentEdgeWeight);

            Action addZeroEdge = () => graph.AddEdge(vertexA, vertexB, 10);

            addZeroEdge.Should().Throw<InvalidOperationException>()
                .WithMessage($"Vertex already exists: {currentEdgeWeight}");
        }

        [Test]
        public void GraphRemoveEdgeTest_Success()
        {
            var graph = new DirectedWeightedGraph<char>();
            var vertexA = graph.AddVertex('A');
            var vertexB = graph.AddVertex('B');
            graph.AddEdge(vertexA, vertexB, 5);

            graph.RemoveEdge(vertexA, vertexB);

            graph.AreAdjacent(vertexA, vertexB).Should().BeFalse();
        }

        [Test]
        public void GraphRemoveEdgeTest_ShouldThrowVertexNotInGraph()
        {
            var graph = new DirectedWeightedGraph<char>();
            var vertexA = graph.AddVertex('A');
            var vertexB = new Vertex<char>('B', 1);

            Action removeEdge = () => graph.RemoveEdge(vertexA, vertexB);

            removeEdge.Should().Throw<InvalidOperationException>()
                .WithMessage($"Vertex does not belong to graph: {vertexB}.");
        }

        [Test]
        public void GraphGetNeighborsTest_Success()
        {
            var graph = new DirectedWeightedGraph<char>();
            var vertexA = graph.AddVertex('A');
            var vertexB = graph.AddVertex('B');
            var vertexC = graph.AddVertex('C');
            var vertexD = graph.AddVertex('D');
            graph.AddEdge(vertexA, vertexB, 5);
            graph.AddEdge(vertexA, vertexC, 5);
            graph.AddEdge(vertexA, vertexD, 5);

            var neighborsA = graph.GetNeighbors(vertexA).ToArray();

            neighborsA.Should().HaveCount(3);
            neighborsA[0].Should().Be(vertexB);
            neighborsA[1].Should().Be(vertexC);
            neighborsA[2].Should().Be(vertexD);
        }

        [Test]
        public void GraphGetNeighborsTest_ShouldThrowVertexNotInGraph()
        {
            var graph = new DirectedWeightedGraph<char>();
            var vertexA = new Vertex<char>('A', 0);

            Func<List<Vertex<char>>> getNeighbors = () =>
            {
                var enumerable = graph.GetNeighbors(vertexA);
                return enumerable.ToList();
            };

            getNeighbors.Should().Throw<InvalidOperationException>()
                .WithMessage($"Vertex does not belong to graph: {vertexA}.");
        }
    }
}
