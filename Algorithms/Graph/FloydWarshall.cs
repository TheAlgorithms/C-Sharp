using System;
using DataStructures.Graph;

namespace Algorithms.Graph;

/// <summary>
/// Floyd Warshall algorithm on directed weighted graph.
/// </summary>
/// <typeparam name="T">generic type of data in graph.</typeparam>
public class FloydWarshall<T>
{
    /// <summary>
    /// runs the algorithm.
    /// </summary>
    /// <param name="graph">graph upon which to run.</param>
    /// <returns>
    /// a 2D array of shortest paths between any two vertices.
    /// where there is no path between two vertices - double.PositiveInfinity is placed.
    /// </returns>
    public double[,] Run(DirectedWeightedGraph<T> graph)
    {
        var distances = SetupDistances(graph);
        var vertexCount = distances.GetLength(0);
        for (var k = 0; k < vertexCount; k++)
        {
            for (var i = 0; i < vertexCount; i++)
            {
                for (var j = 0; j < vertexCount; j++)
                {
                    distances[i, j] = distances[i, j] > distances[i, k] + distances[k, j]
                    ? distances[i, k] + distances[k, j]
                    : distances[i, j];
                }
            }
        }

        return distances;
    }

    /// <summary>
    /// setup adjacency matrix for use by main algorithm run.
    /// </summary>
    /// <param name="graph">graph to dissect adjacency matrix from.</param>
    /// <returns>the adjacency matrix in the format mentioned in Run.</returns>
    private double[,] SetupDistances(DirectedWeightedGraph<T> graph)
    {
        var distances = new double[graph.Count, graph.Count];
        for (int i = 0; i < distances.GetLength(0); i++)
        {
            for (var j = 0; j < distances.GetLength(0); j++)
            {
                var dist = graph.AdjacentDistance(graph.Vertices[i] !, graph.Vertices[j] !);
                distances[i, j] = dist != 0 ? dist : double.PositiveInfinity;
            }
        }

        for (var i = 0; i < distances.GetLength(0); i++)
        {
            distances[i, i] = 0;
        }

        return distances;
    }
}
