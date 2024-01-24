using Algorithms.Graph;
using DataStructures.Graph;
using NUnit.Framework;
using System.Collections.Generic;

namespace Algorithms.Tests.Graph;

public class DepthFirstSearchTests
{
    [Test]
    public void VisitAll_ShouldCountNumberOfVisitedVertix_ResultShouldBeTheSameAsNumberOfVerticesInGraph()
    {
        //Arrange
        var graph = new DirectedWeightedGraph<int>(10);

        var vertex1 = graph.AddVertex(1);

        var vertex2 = graph.AddVertex(20);

        var vertex3 = graph.AddVertex(40);

        var vertex4 = graph.AddVertex(40);

        graph.AddEdge(vertex1, vertex2, 1);

        graph.AddEdge(vertex2, vertex3, 1);

        graph.AddEdge(vertex2, vertex4, 1);

        graph.AddEdge(vertex4, vertex1, 1);

        var dfsSearcher = new DepthFirstSearch<int>();

        long countOfVisitedVertices = 0;

        //Act
        dfsSearcher.VisitAll(graph, vertex1, _ => countOfVisitedVertices++);

        //Assert
        Assert.That(graph.Count, Is.EqualTo(countOfVisitedVertices));
    }

    [Test]
    public void VisitAll_ShouldCountNumberOfVisitedVertices_TwoSeparatedGraphInOne()
    {
        //Arrange
        var graph = new DirectedWeightedGraph<int>(10);

        var vertex1 = graph.AddVertex(1);

        var vertex2 = graph.AddVertex(20);

        var vertex3 = graph.AddVertex(40);

        var vertex4 = graph.AddVertex(40);

        var vertex5 = graph.AddVertex(40);

        var vertex6 = graph.AddVertex(40);

        graph.AddEdge(vertex1, vertex2, 1);

        graph.AddEdge(vertex2, vertex3, 1);

        graph.AddEdge(vertex4, vertex5, 1);

        graph.AddEdge(vertex5, vertex6, 1);

        var dfsSearcher = new DepthFirstSearch<int>();

        long countOfVisitedVerticesPerFirstGraph = 0;

        long countOfVisitedVerticesPerSecondGraph = 0;

        //Act
        dfsSearcher.VisitAll(graph, vertex1, _ => countOfVisitedVerticesPerFirstGraph++);

        dfsSearcher.VisitAll(graph, vertex4, _ => countOfVisitedVerticesPerSecondGraph++);

        //Assert
        Assert.That(3, Is.EqualTo(countOfVisitedVerticesPerFirstGraph));

        Assert.That(3, Is.EqualTo(countOfVisitedVerticesPerSecondGraph));
    }

    [Test]
    public void VisitAll_ReturnTheSuqenceOfVertices_ShouldBeTheSameAsExpected()
    {
        //Arrange
        var graph = new DirectedWeightedGraph<int>(10);

        var vertex1 = graph.AddVertex(1);

        var vertex2 = graph.AddVertex(20);

        var vertex3 = graph.AddVertex(40);

        var vertex4 = graph.AddVertex(40);

        var vertex5 = graph.AddVertex(40);

        graph.AddEdge(vertex1, vertex2, 1);

        graph.AddEdge(vertex2, vertex3, 1);

        graph.AddEdge(vertex2, vertex4, 1);

        graph.AddEdge(vertex3, vertex5, 1);

        var dfsSearcher = new DepthFirstSearch<int>();

        var expectedSequenceOfVisitedVertices = new List<Vertex<int>>
        {
            vertex1,
            vertex2,
            vertex3,
            vertex5,
            vertex4,
        };

        var sequenceOfVisitedVertices = new List<Vertex<int>>();

        //Act
        dfsSearcher.VisitAll(graph, vertex1, vertex => sequenceOfVisitedVertices.Add(vertex));

        //Assert
        Assert.That(sequenceOfVisitedVertices, Is.EqualTo(expectedSequenceOfVisitedVertices));
    }
}
