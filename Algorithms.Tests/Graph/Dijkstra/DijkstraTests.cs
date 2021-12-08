using Algorithms.Graph.Dijkstra;
using DataStructures.Graph;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Graph.Dijkstra
{
    [TestFixture]
    public class DijkstraTests
    {
        [Test]
        public void DijkstraMethodTest()
        {
            // here test case is from https://youtu.be/pVfj6mxhdMw

            var graph = new DirectedWeightedGraph<char>(5);
            var a = graph.AddVertex('A');
            var b = graph.AddVertex('B');
            var c = graph.AddVertex('C');
            var d = graph.AddVertex('D');
            var e = graph.AddVertex('E');

            graph.AddEdge(a, b, 6);
            graph.AddEdge(b, a, 6);

            graph.AddEdge(a, d, 1);
            graph.AddEdge(d, a, 1);

            graph.AddEdge(d, e, 1);
            graph.AddEdge(e, d, 1);

            graph.AddEdge(d, b, 2);
            graph.AddEdge(b, d, 2);

            graph.AddEdge(e, b, 2);
            graph.AddEdge(b, e, 2);

            graph.AddEdge(e, c, 5);
            graph.AddEdge(c, e, 5);

            graph.AddEdge(c, b, 5);
            graph.AddEdge(b, c, 5);

            var shortestPathList = new DijkstraAlgorithm().GenerateShortestPath(graph, a);
            shortestPathList.Count.Should().Be(5);

            shortestPathList[0].Vertex.Should().Be(a);
            shortestPathList[0].Distance.Should().Be(0);
            shortestPathList[0].PreviousVertex.Should().Be(a);

            shortestPathList[1].Vertex.Should().Be(b);
            shortestPathList[1].Distance.Should().Be(3);
            shortestPathList[1].PreviousVertex.Should().Be(d);

            shortestPathList[2].Vertex.Should().Be(d);
            shortestPathList[2].Distance.Should().Be(1);
            shortestPathList[2].PreviousVertex.Should().Be(a);

            shortestPathList[3].Vertex.Should().Be(e);
            shortestPathList[3].Distance.Should().Be(2);
            shortestPathList[3].PreviousVertex.Should().Be(d);

            shortestPathList[4].Vertex.Should().Be(c);
            shortestPathList[4].Distance.Should().Be(7);
            shortestPathList[4].PreviousVertex.Should().Be(e);
        }
    }
}
