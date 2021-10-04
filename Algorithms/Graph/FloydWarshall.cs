using System;
using DataStructures.Graph;

namespace Algorithms.Graph
{
    /// <summary>
    /// Floyd Warshall algorithm on directed weighted graph.
    /// </summary>
    /// <typeparam name="T">generic type of data in graph.</typeparam>
    public class FloydWarshall<T>
    {
        /// <summary>
        /// runs the algorithm.
        /// </summary>
        /// <param name="graph"></param>
        /// <returns>
        /// a 2D array of shortest paths between any two vertices.
        /// where there is no path between two vertices - double.PositiveInfinity is placed.
        /// </returns>
        public double[,] Run(DirectedWeightedGraph<T> graph)
        {
            var distances = new double[graph.Count, graph.Count];
            for (int i = 0; i < distances.GetLength(0); i++)
            {
                for (var j = 0; j < distances.GetLength(0); j++)
                {
                    var dist = graph.AdjacentDistance(
                        graph.Vertices[i] ?? throw new ArgumentNullException($"Vertex was null{graph.Count}."),
                        graph.Vertices[j] ?? throw new ArgumentNullException($"Vertex was null..{graph.Count}"));
                    distances[i, j] = dist != 0 ? dist : double.PositiveInfinity;
                }
            }

            for (var i = 0; i < distances.GetLength(0); i++)
            {
                distances[i, i] = 0;
            }

            for (var k = 0; k < distances.GetLength(0); k++)
            {
                for (var i = 0; i < distances.GetLength(0); i++)
                {
                    for (var j = 0; j < distances.GetLength(0); j++)
                    {
                        if (distances[i, j] > distances[i, k] + distances[k, j])
                        {
                            distances[i, j] = distances[i, k] + distances[k, j];
                        }
                    }
                }
            }

            return distances;
        }
    }
}
