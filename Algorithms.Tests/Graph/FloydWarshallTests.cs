using Algorithms.Graph;
using DataStructures.Graph;
using NUnit.Framework;
using FluentAssertions;

namespace Algorithms.Tests.Graph;

public class FloydWarshallTests
{
    [Test]
    public void CorrectMatrixTest()
    {
        var graph = new DirectedWeightedGraph<int>(10);

        var vertex1 = graph.AddVertex(1);

        var vertex2 = graph.AddVertex(2);

        var vertex3 = graph.AddVertex(3);

        var vertex4 = graph.AddVertex(4);

        var vertex5 = graph.AddVertex(5);

        graph.AddEdge(vertex1, vertex2, 3);

        graph.AddEdge(vertex1, vertex5, -4);

        graph.AddEdge(vertex1, vertex3, 8);

        graph.AddEdge(vertex2, vertex5, 7);

        graph.AddEdge(vertex2, vertex4, 1);

        graph.AddEdge(vertex3, vertex2, 4);

        graph.AddEdge(vertex4, vertex3, -5);

        graph.AddEdge(vertex4, vertex1, 2);

        graph.AddEdge(vertex5, vertex4, 6);

        var actualDistances = new double[,]
        {
            { 0, 1, -3, 2, -4 },
            { 3, 0, -4, 1, -1 },
            { 7, 4, 0, 5, 3 },
            { 2, -1, -5, 0, -2 },
            { 8, 5, 1, 6, 0 },
        };

        var floydWarshaller = new FloydWarshall<int>();
        floydWarshaller.Run(graph).Should().BeEquivalentTo(actualDistances);
    }
}
