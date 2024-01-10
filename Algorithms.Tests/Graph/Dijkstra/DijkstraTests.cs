using System;
using Algorithms.Graph.Dijkstra;
using DataStructures.Graph;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Graph.Dijkstra;

[TestFixture]
public class DijkstraTests
{
    [Test]
    public void DijkstraTest1_Success()
    {
        // here test case is from https://www.youtube.com/watch?v=pVfj6mxhdMw

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

        var shortestPathList = DijkstraAlgorithm.GenerateShortestPath(graph, a);
        shortestPathList.Length.Should().Be(5);

        shortestPathList[0].Vertex.Should().Be(a);
        shortestPathList[0].Distance.Should().Be(0);
        shortestPathList[0].PreviousVertex.Should().Be(a);
        shortestPathList[0].ToString().Should()
            .Be($"Vertex: {a} - Distance: {0} - Previous: {a}");

        shortestPathList[1].Vertex.Should().Be(b);
        shortestPathList[1].Distance.Should().Be(3);
        shortestPathList[1].PreviousVertex.Should().Be(d);
        shortestPathList[1].ToString().Should()
            .Be($"Vertex: {b} - Distance: {3} - Previous: {d}");

        shortestPathList[2].Vertex.Should().Be(c);
        shortestPathList[2].Distance.Should().Be(7);
        shortestPathList[2].PreviousVertex.Should().Be(e);
        shortestPathList[2].ToString().Should()
            .Be($"Vertex: {c} - Distance: {7} - Previous: {e}");

        shortestPathList[3].Vertex.Should().Be(d);
        shortestPathList[3].Distance.Should().Be(1);
        shortestPathList[3].PreviousVertex.Should().Be(a);
        shortestPathList[3].ToString().Should()
            .Be($"Vertex: {d} - Distance: {1} - Previous: {a}");

        shortestPathList[4].Vertex.Should().Be(e);
        shortestPathList[4].Distance.Should().Be(2);
        shortestPathList[4].PreviousVertex.Should().Be(d);
        shortestPathList[4].ToString().Should()
            .Be($"Vertex: {e} - Distance: {2} - Previous: {d}");
    }

    [Test]
    public void DijkstraTest2_Success()
    {
        var graph = new DirectedWeightedGraph<char>(5);
        var a = graph.AddVertex('A');
        var b = graph.AddVertex('B');
        var c = graph.AddVertex('C');

        graph.AddEdge(a, b, 1);
        graph.AddEdge(b, a, 1);

        graph.AddEdge(b, c, 1);
        graph.AddEdge(c, b, 1);

        graph.AddEdge(a, c, 3);
        graph.AddEdge(c, a, 3);

        var shortestPathList = DijkstraAlgorithm.GenerateShortestPath(graph, a);

        shortestPathList.Length.Should().Be(3);
        shortestPathList[0].Vertex.Should().Be(a);
        shortestPathList[0].Distance.Should().Be(0);
        shortestPathList[0].PreviousVertex.Should().Be(a);
        shortestPathList[0].ToString().Should()
            .Be($"Vertex: {a} - Distance: {0} - Previous: {a}");

        shortestPathList[1].Vertex.Should().Be(b);
        shortestPathList[1].Distance.Should().Be(1);
        shortestPathList[1].PreviousVertex.Should().Be(a);
        shortestPathList[1].ToString().Should()
            .Be($"Vertex: {b} - Distance: {1} - Previous: {a}");

        shortestPathList[2].Vertex.Should().Be(c);
        shortestPathList[2].Distance.Should().Be(2);
        shortestPathList[2].PreviousVertex.Should().Be(b);
        shortestPathList[2].ToString().Should()
            .Be($"Vertex: {c} - Distance: {2} - Previous: {b}");
    }

    [Test]
    public void DijkstraTest3_Success()
    {
        var graph = new DirectedWeightedGraph<char>(5);
        var a = graph.AddVertex('A');
        var b = graph.AddVertex('B');
        var c = graph.AddVertex('C');

        graph.AddEdge(a, b, 1);
        graph.AddEdge(b, a, 1);

        graph.AddEdge(a, c, 3);
        graph.AddEdge(c, a, 3);

        var shortestPathList = DijkstraAlgorithm.GenerateShortestPath(graph, a);

        shortestPathList.Length.Should().Be(3);
        shortestPathList[0].Vertex.Should().Be(a);
        shortestPathList[0].Distance.Should().Be(0);
        shortestPathList[0].PreviousVertex.Should().Be(a);
        shortestPathList[0].ToString().Should()
            .Be($"Vertex: {a} - Distance: {0} - Previous: {a}");

        shortestPathList[1].Vertex.Should().Be(b);
        shortestPathList[1].Distance.Should().Be(1);
        shortestPathList[1].PreviousVertex.Should().Be(a);
        shortestPathList[1].ToString().Should()
            .Be($"Vertex: {b} - Distance: {1} - Previous: {a}");

        shortestPathList[2].Vertex.Should().Be(c);
        shortestPathList[2].Distance.Should().Be(3);
        shortestPathList[2].PreviousVertex.Should().Be(a);
        shortestPathList[2].ToString().Should()
            .Be($"Vertex: {c} - Distance: {3} - Previous: {a}");
    }

    [Test]
    public void DijkstraTest4_Success()
    {
        var graph = new DirectedWeightedGraph<char>(5);
        var a = graph.AddVertex('A');
        var b = graph.AddVertex('B');
        var c = graph.AddVertex('C');
        var d = graph.AddVertex('D');

        graph.AddEdge(a, b, 1);
        graph.AddEdge(b, a, 1);

        graph.AddEdge(a, c, 3);
        graph.AddEdge(c, a, 3);

        graph.AddEdge(c, d, 5);
        graph.AddEdge(d, c, 5);

        var shortestPathList = DijkstraAlgorithm.GenerateShortestPath(graph, a);

        shortestPathList.Length.Should().Be(4);
        shortestPathList[0].Vertex.Should().Be(a);
        shortestPathList[0].Distance.Should().Be(0);
        shortestPathList[0].PreviousVertex.Should().Be(a);
        shortestPathList[0].ToString().Should()
            .Be($"Vertex: {a} - Distance: {0} - Previous: {a}");

        shortestPathList[1].Vertex.Should().Be(b);
        shortestPathList[1].Distance.Should().Be(1);
        shortestPathList[1].PreviousVertex.Should().Be(a);
        shortestPathList[1].ToString().Should()
            .Be($"Vertex: {b} - Distance: {1} - Previous: {a}");

        shortestPathList[2].Vertex.Should().Be(c);
        shortestPathList[2].Distance.Should().Be(3);
        shortestPathList[2].PreviousVertex.Should().Be(a);
        shortestPathList[2].ToString().Should()
            .Be($"Vertex: {c} - Distance: {3} - Previous: {a}");

        // Vertex D won't be visited in this dijkstra implementation which is valid only for cyclic graphs,
        // since it is necessary to backtrack all unvisited vertices and place them
        // to the priority queue, which is not implemented yet in this repository.
        // If algo goes to the next vertex with minimal distance and this vertex is leaf -- algorithm stops.
        shortestPathList[3].Vertex.Should().Be(d);
        shortestPathList[3].Distance.Should().Be(double.MaxValue);
        shortestPathList[3].PreviousVertex.Should().BeNull();
        shortestPathList[3].ToString().Should()
            .Be($"Vertex: {d} - Distance: {double.MaxValue} - Previous: {null}");
    }

    [Test]
    public void DijkstraMethodTest_ShouldThrow_GraphIsNull()
    {
        var graph = new DirectedWeightedGraph<char>(5);
        var a = graph.AddVertex('A');

        Func<DistanceModel<char>[]> action = () => DijkstraAlgorithm.GenerateShortestPath(null!, a);

        action.Should().Throw<ArgumentNullException>()
            .WithMessage($"Value cannot be null. (Parameter '{nameof(graph)}')");
    }

    [Test]
    public void DijkstraMethodTest_ShouldThrow_VertexDoesntBelongToGraph()
    {
        var graph = new DirectedWeightedGraph<char>(5);
        var startVertex = graph.AddVertex('A');

        Func<DistanceModel<char>[]> action = () => DijkstraAlgorithm.GenerateShortestPath(
            new DirectedWeightedGraph<char>(5), startVertex);

        action.Should().Throw<ArgumentNullException>()
            .WithMessage($"Value cannot be null. (Parameter '{nameof(graph)}')");
    }
}
