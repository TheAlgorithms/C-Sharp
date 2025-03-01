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
        public void DijkstraTest1_GraphIsNull()
        {
            var graph = new DirectedWeightedGraph<char>(5);
            var a = graph.AddVertex('A');

            Func<DistanceModel<char>[]> action = () => DijkstraAlgorithm.GenerateShortestPath(null!, a);

            action.Should().Throw<ArgumentNullException>()
                .WithMessage($"Value cannot be null. (Parameter '{nameof(graph)}')");
        }

        [Test]
        public void DijkstraTest2_VertexDoesntBelongToGraph()
        {
            var graph = new DirectedWeightedGraph<char>(5);
            var startVertex = graph.AddVertex('A');

            Func<DistanceModel<char>[]> action = () => DijkstraAlgorithm.GenerateShortestPath(
                new DirectedWeightedGraph<char>(5), startVertex);

            action.Should().Throw<ArgumentNullException>()
                .WithMessage($"Value cannot be null. (Parameter '{nameof(graph)}')");
        }

        [Test]
        public void DijkstraTest3_BasicGraph()
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
            shortestPathList[0].PrintDistance().Should()
                .Be($"Start from Vertex: {a} to Vertex {a} is Distance: {0}");

            shortestPathList[1].PreviousVertex.Should().Be(a);
            shortestPathList[1].Vertex.Should().Be(b);
            shortestPathList[1].Distance.Should().Be(4);
            shortestPathList[1].PrintDistance().Should()
                .Be($"Start from Vertex: {a} to Vertex {b} is Distance: {4}");

            shortestPathList[2].PreviousVertex.Should().Be(b);
            shortestPathList[2].Vertex.Should().Be(c);
            shortestPathList[2].Distance.Should().Be(6);
            shortestPathList[2].PrintDistance().Should()
                .Be($"Start from Vertex: {a} to Vertex {c} is Distance: {6}");

            shortestPathList[3].PreviousVertex.Should().Be(b);
            shortestPathList[3].Vertex.Should().Be(d);
            shortestPathList[3].Distance.Should().Be(7);
            shortestPathList[3].PrintDistance().Should()
                .Be($"Start from Vertex: {a} to Vertex {d} is Distance: {7}");
        }

        [Test]
        public void DijkstraTest4_SparseGraph()
        {
            var graph = new DirectedWeightedGraph<char>(6);
            var a = graph.AddVertex('A');
            var b = graph.AddVertex('B');
            var c = graph.AddVertex('C');
            var d = graph.AddVertex('D');
            var e = graph.AddVertex('E');
            var f = graph.AddVertex('F');

            graph.AddEdge(a, f, 4);


            var shortestPathList = DijkstraAlgorithm.GenerateShortestPath(graph, a);
            shortestPathList.Length.Should().Be(6);

            shortestPathList[0].PreviousVertex.Should().Be(a);
            shortestPathList[0].Vertex.Should().Be(a);
            shortestPathList[0].Distance.Should().Be(0);
            shortestPathList[0].PrintDistance().Should()
                .Be($"Start from Vertex: {a} to Vertex {a} is Distance: {0}");

            shortestPathList[1].PreviousVertex.Should().Be(null);
            shortestPathList[1].Vertex.Should().Be(b);
            shortestPathList[1].Distance.Should().Be(double.MaxValue);
            shortestPathList[1].PrintDistance().Should()
                .Be($"Start from Vertex: {a} to Vertex {b} is Distance: {double.MaxValue}");

            shortestPathList[2].PreviousVertex.Should().Be(null);
            shortestPathList[2].Vertex.Should().Be(c);
            shortestPathList[2].Distance.Should().Be(double.MaxValue);
            shortestPathList[2].PrintDistance().Should()
                .Be($"Start from Vertex: {a} to Vertex {c} is Distance: {double.MaxValue}");

            shortestPathList[3].PreviousVertex.Should().Be(null);
            shortestPathList[3].Vertex.Should().Be(d);
            shortestPathList[3].Distance.Should().Be(double.MaxValue);
            shortestPathList[3].PrintDistance().Should()
                .Be($"Start from Vertex: {a} to Vertex {d} is Distance: {double.MaxValue}");

            shortestPathList[4].PreviousVertex.Should().Be(null);
            shortestPathList[4].Vertex.Should().Be(e);
            shortestPathList[4].Distance.Should().Be(double.MaxValue);
            shortestPathList[4].PrintDistance().Should()
                .Be($"Start from Vertex: {a} to Vertex {e} is Distance: {double.MaxValue}");

            shortestPathList[5].PreviousVertex.Should().Be(a);
            shortestPathList[5].Vertex.Should().Be(f);
            shortestPathList[5].Distance.Should().Be(4);
            shortestPathList[5].PrintDistance().Should()
                .Be($"Start from Vertex: {a} to Vertex {f} is Distance: {4}");
        }

        [Test]
        public void DijkstraTest5_DenseGraph()
        {
            var graph = new DirectedWeightedGraph<char>(6);
            var a = graph.AddVertex('A');
            var b = graph.AddVertex('B');
            var c = graph.AddVertex('C');
            var d = graph.AddVertex('D');
            var e = graph.AddVertex('E');
            var f = graph.AddVertex('F');

            graph.AddEdge(a, b, 1);
            graph.AddEdge(a, d, 2);
            graph.AddEdge(b, c, 1);
            graph.AddEdge(b, d, 2);
            graph.AddEdge(b, e, 3);
            graph.AddEdge(c, e, 2);
            graph.AddEdge(c, f, 1);
            graph.AddEdge(d, e, 1);
            graph.AddEdge(e, f, 1);

            var shortestPathList = DijkstraAlgorithm.GenerateShortestPath(graph, a);
            shortestPathList.Length.Should().Be(6);

            shortestPathList[0].PreviousVertex.Should().Be(a);
            shortestPathList[0].Vertex.Should().Be(a);
            shortestPathList[0].Distance.Should().Be(0);
            shortestPathList[0].PrintDistance().Should()
                .Be($"Start from Vertex: {a} to Vertex {a} is Distance: {0}");

            shortestPathList[1].PreviousVertex.Should().Be(a);
            shortestPathList[1].Vertex.Should().Be(b);
            shortestPathList[1].Distance.Should().Be(1);
            shortestPathList[1].PrintDistance().Should()
                .Be($"Start from Vertex: {a} to Vertex {b} is Distance: {1}");

            shortestPathList[2].PreviousVertex.Should().Be(b);
            shortestPathList[2].Vertex.Should().Be(c);
            shortestPathList[2].Distance.Should().Be(2);
            shortestPathList[2].PrintDistance().Should()
                .Be($"Start from Vertex: {a} to Vertex {c} is Distance: {2}");

            shortestPathList[3].PreviousVertex.Should().Be(a);
            shortestPathList[3].Vertex.Should().Be(d);
            shortestPathList[3].Distance.Should().Be(2);
            shortestPathList[3].PrintDistance().Should()
                .Be($"Start from Vertex: {a} to Vertex {d} is Distance: {2}");

            shortestPathList[4].PreviousVertex.Should().Be(d);
            shortestPathList[4].Vertex.Should().Be(e);
            shortestPathList[4].Distance.Should().Be(3);
            shortestPathList[4].PrintDistance().Should()
                .Be($"Start from Vertex: {a} to Vertex {e} is Distance: {3}");

            shortestPathList[5].PreviousVertex.Should().Be(c);
            shortestPathList[5].Vertex.Should().Be(f);
            shortestPathList[5].Distance.Should().Be(3);
            shortestPathList[5].PrintDistance().Should()
                .Be($"Start from Vertex: {a} to Vertex {f} is Distance: {3}");

        }
    }
     
}
