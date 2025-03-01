using Algorithms.Graph.Dijkstra;
using DataStructures.Graph;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Dijktra.Tests
{
    [TestFixture]
    public class DijkstraUTests
    {
        [Test]
        public void DijkstraTest1_BasicGraph()
        {
            var graph = new DirectedWeightedGraph<char>(4);
            var a = graph.AddVertex('A');
            var b = graph.AddVertex('B');
            var c = graph.AddVertex('C');
            var d = graph.AddVertex('D');

            graph.AddEdge(a, b, 4);
            graph.AddEdge(a, c, 7);
            graph.AddEdge(b, c, 2);
            graph.AddEdge(b, d, 3);
            graph.AddEdge(c, d, 5);


            var shortestPathList = DijkstraAlgorithm.GenerateShortestPath(graph, a);
            shortestPathList.Length.Should().Be(4);

            shortestPathList[0].PreviousVertex.Should().Be(a);
            shortestPathList[0].Vertex.Should().Be(a);
            shortestPathList[0].Distance.Should().Be(0);
            shortestPathList[0].ToString().Should()
                .Be($"From Previous Vertex: {a} to Vertex {a} is Distance: {0}");

            shortestPathList[1].PreviousVertex.Should().Be(a);
            shortestPathList[1].Vertex.Should().Be(b);
            shortestPathList[1].Distance.Should().Be(4);
            shortestPathList[1].ToString().Should()
                .Be($"From Previous Vertex: {a} to Vertex {b} is Distance: {4}");

            shortestPathList[2].PreviousVertex.Should().Be(b);
            shortestPathList[2].Vertex.Should().Be(c);
            shortestPathList[2].Distance.Should().Be(6);
            shortestPathList[2].ToString().Should()
                .Be($"From Previous Vertex: {b} to Vertex {c} is Distance: {6}");

            shortestPathList[3].PreviousVertex.Should().Be(b);
            shortestPathList[3].Vertex.Should().Be(d);
            shortestPathList[3].Distance.Should().Be(7);
            shortestPathList[3].ToString().Should()
                .Be($"From Previous Vertex: {b} to Vertex {d} is Distance: {7}");
        }
    }
}
