using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Graph;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests.Graph;

[TestFixture]
public class DirectedWeightedGraphTests
{
    [TestCase(-1)]
    [TestCase(-2)]
    [TestCase(-3)]
    public void GraphInitializationTest_ShouldThrowOverflow(int capacity)
    {
        Func<DirectedWeightedGraph<char>> createGraph = () => new DirectedWeightedGraph<char>(capacity);

        createGraph.Should().Throw<InvalidOperationException>()
            .WithMessage("Graph capacity should always be a non-negative integer.");
    }

    [TestCase(1)]
    [TestCase(10)]
    [TestCase(20)]
    [TestCase(30)]
    public void GraphInitializationTest_Success(int capacity)
    {
        Func<DirectedWeightedGraph<char>> createGraph = () => new DirectedWeightedGraph<char>(capacity);

        createGraph.Should().NotThrow();
    }

    [Test]
    public void GraphAddVertexTest_Success()
    {
        var graph = new DirectedWeightedGraph<char>(10);

        graph.AddVertex('A');
        graph.AddVertex('B');
        graph.AddVertex('C');

        graph.Count.Should().Be(3);
    }

    [Test]
    public void GraphAddVertexTest_ShouldThrowOverflow()
    {
        var graph = new DirectedWeightedGraph<char>(10);
        for (var i = 0; i < 10; i++)
        {
            graph.AddVertex('A');
        }

        Action addOverflow = () => graph.AddVertex('A');

        graph.Count.Should().Be(10);
        graph.Vertices.Should().OnlyContain(x => x != null && x.Data == 'A');
        addOverflow.Should().Throw<InvalidOperationException>()
            .WithMessage("Graph overflow.");
    }

    [Test]
    public void GraphRemoveVertexTest_Success()
    {
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = graph.AddVertex('A');
        var vertexB = graph.AddVertex('B');
        var vertexC = graph.AddVertex('C');
        graph.AddEdge(vertexB, vertexA, 5);
        graph.AddEdge(vertexC, vertexA, 5);
        var neighborsB = graph.GetNeighbors(vertexB).ToList();
        var neighborsC = graph.GetNeighbors(vertexC).ToList();

        graph.RemoveVertex(vertexA);

        neighborsB.Should().HaveCount(1);
        neighborsB[0].Should().Be(vertexA);
        neighborsC.Should().HaveCount(1);
        neighborsC[0].Should().Be(vertexA);
        graph.GetNeighbors(vertexB).Should().HaveCount(0);
        graph.GetNeighbors(vertexC).Should().HaveCount(0);
    }

    [Test]
    public void GraphRemoveAndAddVertexTest_Success()
    {
        double weight_A_B = 1;
        double weight_A_C = 2;
        double weight_A_D = 3;
        double weight_B_A = 4;
        double weight_B_C = 5;
        double weight_C_A = 6;
        double weight_C_B = 7;
        double weight_C_D = 8;
        double weight_D_A = 9;
        double weight_D_C = 10;

        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = graph.AddVertex('A');
        var vertexB = graph.AddVertex('B');
        var vertexC = graph.AddVertex('C');
        graph.AddEdge(vertexA, vertexB, weight_A_B);
        graph.AddEdge(vertexA, vertexC, weight_A_C);
        graph.AddEdge(vertexB, vertexA, weight_B_A);
        graph.AddEdge(vertexB, vertexC, weight_B_C);
        graph.AddEdge(vertexC, vertexA, weight_C_A);
        graph.AddEdge(vertexC, vertexB, weight_C_B);

        var vertexA_Index_BeforeUpdate = vertexA.Index;
        vertexA_Index_BeforeUpdate.Should().Be(0);
        var neighborsA_BeforeUpdate = graph.GetNeighbors(vertexA).ToList();
        neighborsA_BeforeUpdate.Should().HaveCount(2);
        neighborsA_BeforeUpdate[0].Should().Be(vertexB);
        neighborsA_BeforeUpdate[1].Should().Be(vertexC);

        var vertexB_Index_BeforeUpdate = vertexB.Index;
        vertexB_Index_BeforeUpdate.Should().Be(1);
        var neighborsB_BeforeUpdate = graph.GetNeighbors(vertexB).ToList();
        neighborsB_BeforeUpdate.Should().HaveCount(2);
        neighborsB_BeforeUpdate[0].Should().Be(vertexA);
        neighborsB_BeforeUpdate[1].Should().Be(vertexC);

        var vertexC_Index_BeforeUpdate = vertexC.Index;
        vertexC_Index_BeforeUpdate.Should().Be(2);
        var neighborsC_BeforeUpdate = graph.GetNeighbors(vertexC).ToList();
        neighborsC_BeforeUpdate.Should().HaveCount(2);
        neighborsC_BeforeUpdate[0].Should().Be(vertexA);
        neighborsC_BeforeUpdate[1].Should().Be(vertexB);

        var weight_A_B_BeforeUpdate = graph.AdjacentDistance(vertexA, vertexB);
        var weight_A_C_BeforeUpdate = graph.AdjacentDistance(vertexA, vertexC);
        var weight_B_A_BeforeUpdate = graph.AdjacentDistance(vertexB, vertexA);
        var weight_B_C_BeforeUpdate = graph.AdjacentDistance(vertexB, vertexC);
        var weight_C_A_BeforeUpdate = graph.AdjacentDistance(vertexC, vertexA);
        var weight_C_B_BeforeUpdate = graph.AdjacentDistance(vertexC, vertexB);
        weight_A_B_BeforeUpdate.Should().Be(weight_A_B);
        weight_A_C_BeforeUpdate.Should().Be(weight_A_C);
        weight_B_A_BeforeUpdate.Should().Be(weight_B_A);
        weight_B_C_BeforeUpdate.Should().Be(weight_B_C);
        weight_C_A_BeforeUpdate.Should().Be(weight_C_A);
        weight_C_B_BeforeUpdate.Should().Be(weight_C_B);

        graph.RemoveVertex(vertexB);
        var vertexD = graph.AddVertex('D');
        graph.AddEdge(vertexA, vertexD, weight_A_D);
        graph.AddEdge(vertexC, vertexD, weight_C_D);
        graph.AddEdge(vertexD, vertexA, weight_D_A);
        graph.AddEdge(vertexD, vertexC, weight_D_C);

        var vertexA_Index_AfterUpdate = vertexA.Index;
        vertexA_Index_AfterUpdate.Should().Be(0);
        var neighborsA_AfterUpdate = graph.GetNeighbors(vertexA).ToList();
        neighborsA_AfterUpdate.Should().HaveCount(2);
        neighborsA_AfterUpdate[0].Should().Be(vertexC);
        neighborsA_AfterUpdate[1].Should().Be(vertexD);

        var vertexC_Index_AfterUpdate = vertexC.Index;
        vertexC_Index_AfterUpdate.Should().Be(1);
        var neighborsC_AfterUpdate = graph.GetNeighbors(vertexC).ToList();
        neighborsC_AfterUpdate.Should().HaveCount(2);
        neighborsC_AfterUpdate[0].Should().Be(vertexA);
        neighborsC_AfterUpdate[1].Should().Be(vertexD);

        var vertexD_Index_AfterUpdate = vertexD.Index;
        vertexD_Index_AfterUpdate.Should().Be(2);
        var neighborsD_AfterUpdate = graph.GetNeighbors(vertexD).ToList();
        neighborsD_AfterUpdate.Should().HaveCount(2);
        neighborsD_AfterUpdate[0].Should().Be(vertexA);
        neighborsD_AfterUpdate[1].Should().Be(vertexC);

        var weight_A_C_AfterUpdate = graph.AdjacentDistance(vertexA, vertexC);
        var weight_A_D_AfterUpdate = graph.AdjacentDistance(vertexA, vertexD);
        var weight_C_A_AfterUpdate = graph.AdjacentDistance(vertexC, vertexA);
        var weight_C_D_AfterUpdate = graph.AdjacentDistance(vertexC, vertexD);
        var weight_D_A_AfterUpdate = graph.AdjacentDistance(vertexD, vertexA);
        var weight_D_C_AfterUpdate = graph.AdjacentDistance(vertexD, vertexC);
        weight_A_D_AfterUpdate.Should().Be(weight_A_D);
        weight_A_C_AfterUpdate.Should().Be(weight_A_C);
        weight_D_A_AfterUpdate.Should().Be(weight_D_A);
        weight_D_C_AfterUpdate.Should().Be(weight_D_C);
        weight_C_A_AfterUpdate.Should().Be(weight_C_A);
        weight_C_D_AfterUpdate.Should().Be(weight_C_D);
    }

    [Test]
    public void GraphRemoveVertexTest_ShouldThrowVertexNotInGraph()
    {
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = new Vertex<char>('A', 0);

        Action removeVertex = () => graph.RemoveVertex(vertexA);

        removeVertex.Should().Throw<InvalidOperationException>()
            .WithMessage($"Vertex does not belong to graph: {vertexA}.");
    }

    [Test]
    public void GraphAddEdgeTest_Success()
    {
        var graph = new DirectedWeightedGraph<char>(10);
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
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = graph.AddVertex('A');
        var vertexB = graph.AddVertex('B');

        Action addZeroEdge = () => graph.AddEdge(vertexA, vertexB, 0);

        addZeroEdge.Should().Throw<InvalidOperationException>()
            .WithMessage("Edge weight cannot be zero.");
    }

    [Test]
    public void GraphAddEdgeTest_ShouldThrowVertexNotInGraph()
    {
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = graph.AddVertex('A');
        var vertexB = new Vertex<char>('B', 1);

        Action addZeroEdge = () => graph.AddEdge(vertexA, vertexB, 0);

        addZeroEdge.Should().Throw<InvalidOperationException>()
            .WithMessage($"Vertex does not belong to graph: {vertexB}.");
    }

    [Test]
    public void GraphAddEdgeTest_ShouldThrowEdgeExists()
    {
        var graph = new DirectedWeightedGraph<char>(10);
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
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = graph.AddVertex('A');
        var vertexB = graph.AddVertex('B');
        graph.AddEdge(vertexA, vertexB, 5);

        graph.RemoveEdge(vertexA, vertexB);

        graph.AreAdjacent(vertexA, vertexB).Should().BeFalse();
    }

    [Test]
    public void GraphRemoveEdgeTest_ShouldThrowVertexNotInGraph()
    {
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = graph.AddVertex('A');
        var vertexB = new Vertex<char>('B', 1);

        Action removeEdge = () => graph.RemoveEdge(vertexA, vertexB);

        removeEdge.Should().Throw<InvalidOperationException>()
            .WithMessage($"Vertex does not belong to graph: {vertexB}.");
    }

    [Test]
    public void GraphGetNeighborsTest_Success()
    {
        var graph = new DirectedWeightedGraph<char>(10);
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
        var graph = new DirectedWeightedGraph<char>(10);
        var vertexA = new Vertex<char>('A', 0);

        Func<List<Vertex<char>?>> getNeighbors = () =>
        {
            var enumerable = graph.GetNeighbors(vertexA);
            return enumerable.ToList();
        };

        getNeighbors.Should().Throw<InvalidOperationException>()
            .WithMessage($"Vertex does not belong to graph: {vertexA}.");
    }
}
