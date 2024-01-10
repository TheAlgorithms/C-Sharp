using Algorithms.Graph;
using DataStructures.Graph;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Tests.Graph;

public class KosarajuTests
{

    [Test]
    public void GetRepresentativesTest()
    {
        // Create a graph with some SCC. 
        var graph = new DirectedWeightedGraph<int>(10);

        var vertex1 = graph.AddVertex(1);
        var vertex2 = graph.AddVertex(2);
        var vertex3 = graph.AddVertex(3);
        var vertex4 = graph.AddVertex(4);
        var vertex5 = graph.AddVertex(5);
        var vertex6 = graph.AddVertex(6);
        var vertex7 = graph.AddVertex(7);

        graph.AddEdge(vertex1, vertex2, 1);
        graph.AddEdge(vertex2, vertex3, 1);
        graph.AddEdge(vertex3, vertex1, 1);
        graph.AddEdge(vertex3, vertex2, 1);
        graph.AddEdge(vertex2, vertex4, 1);
        graph.AddEdge(vertex4, vertex5, 1);
        graph.AddEdge(vertex5, vertex4, 1);
        graph.AddEdge(vertex5, vertex6, 1);

        // Run the agorithm and obtain the representative vertex of the SCC to which each vertex belongs.
        Dictionary<Vertex<int>,Vertex<int>> result = Kosaraju<int>.GetRepresentatives(graph);

        // Check every Vertex belongs to a SCC
        result.Should().ContainKey(vertex1);
        result.Should().ContainKey(vertex2);
        result.Should().ContainKey(vertex3);
        result.Should().ContainKey(vertex4);
        result.Should().ContainKey(vertex5);
        result.Should().ContainKey(vertex6);
        result.Should().ContainKey(vertex7);

        // There should be 4 SCC: {1,2,3}, {4,5}, {6} and {7}
        // Vertices 1, 2 and 3 are a SCC
        result[vertex1].Should().Be(result[vertex2]).And.Be(result[vertex3]);

        // Vertices 4 and 5 are another SCC
        result[vertex4].Should().Be(result[vertex5]);

        // And the should have a different representative vertex
        result[vertex1].Should().NotBe(result[vertex4]);

        // Vertices 6 and 7 are their own SCC
        result[vertex6].Should().Be(vertex6);
        result[vertex7].Should().Be(vertex7);
    }

    [Test]
    public void GetSccTest()
    {
        // Create a graph with some SCC. 
        var graph = new DirectedWeightedGraph<int>(10);

        var vertex1 = graph.AddVertex(1);
        var vertex2 = graph.AddVertex(2);
        var vertex3 = graph.AddVertex(3);
        var vertex4 = graph.AddVertex(4);
        var vertex5 = graph.AddVertex(5);
        var vertex6 = graph.AddVertex(6);
        var vertex7 = graph.AddVertex(7);

        graph.AddEdge(vertex1, vertex2, 1);
        graph.AddEdge(vertex2, vertex3, 1);
        graph.AddEdge(vertex3, vertex1, 1);
        graph.AddEdge(vertex3, vertex2, 1);
        graph.AddEdge(vertex2, vertex4, 1);
        graph.AddEdge(vertex4, vertex5, 1);
        graph.AddEdge(vertex5, vertex4, 1);
        graph.AddEdge(vertex5, vertex6, 1);

        // Run the algorithm and get SCC as lists of vertices. 
        var scc = Kosaraju<int>.GetScc(graph);

        // There should be 4 SCC: {1,2,3}, {4,5}, {6} and {7}
        scc.Should().HaveCount(4);

        // Vertices 1, 2 and 3 are a SCC
        scc.First(c => c.Contains(vertex1)).Should().Contain(vertex2).And.Contain(vertex3);

        // Vertices 4 and 5 are another SCC
        scc.First(c => c.Contains(vertex4)).Should().Contain(vertex5);

        // Vertices 6 and 7 are their own SCC
        scc.First(c => c.Contains(vertex6)).Should().HaveCount(1);
        scc.First(c => c.Contains(vertex7)).Should().HaveCount(1);
    }
}
