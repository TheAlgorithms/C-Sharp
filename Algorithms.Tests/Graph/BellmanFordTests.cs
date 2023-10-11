using Algorithms.Graph;
using DataStructures.Graph;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using System;

namespace Algorithms.Tests.Graph
{
    public class BellmanFordTests
    {
        [Test]
        public void CorrectDistancesTest()
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

            var expectedDistances = new Dictionary<Vertex<int>, double>
            {
                { vertex1, 0 },
                { vertex2, 1 },
                { vertex3, -3 },
                { vertex4, 2 },
                { vertex5, -4 }
            };

            var bellmanFord = new BellmanFord<int>();

            var calculatedDistances = bellmanFord.Run(graph, vertex1);

            calculatedDistances.Should().BeEquivalentTo(expectedDistances);
        }
    }
}
