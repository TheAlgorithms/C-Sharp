using System;
using System.Collections.Generic;
using DataStructures.Graph;

namespace Algorithms.Graph
{
    /// <summary>
    /// Bellman-Ford algorithm on directed weighted graph.
    /// </summary>
    /// <typeparam name="T">Generic type of data in the graph.</typeparam>
    public class BellmanFord<T>
    {
        /// <summary>
        /// Runs the Bellman-Ford algorithm.
        /// </summary>
        /// <param name="graph">Graph upon which to run.</param>
        /// <param name="sourceVertex">Source vertex for shortest path calculation.</param>
        /// <returns>
        /// A dictionary containing the shortest distances from the source vertex to all other vertices.
        /// If a vertex is unreachable from the source, it will have a value of double.PositiveInfinity.
        /// </returns>
        public Dictionary<Vertex<T>, double> Run(DirectedWeightedGraph<T> graph, Vertex<T> sourceVertex)
        {
            int vertexCount = graph.Count;
            var distances = new Dictionary<Vertex<T>, double>();
            var predecessors = new Dictionary<Vertex<T>, Vertex<T>?>();

            // Initialize distances to all vertices as positive infinity except the source.
            foreach (var vertex in graph.Vertices)
            {
                if(vertex == null)
                {
                    continue;
                }

                distances[vertex] = double.PositiveInfinity;
                predecessors[vertex] = null;
            }

            distances[sourceVertex] = 0;

            // Relax edges repeatedly to find the shortest paths.
            for (int i = 0; i < vertexCount - 1; i++)
            {
                foreach (var vertex in graph.Vertices)
                {
                    if(vertex == null)
                    {
                        continue;
                    }

                    var u = vertex;
                    foreach (var neighbor in graph.GetNeighbors(vertex))
                    {
                        if (neighbor == null)
                        {
                            continue;
                        }

                        var v = neighbor;
                        var weight = graph.AdjacentDistance(u, v);
                        if (distances[u] + weight < distances[v])
                        {
                            distances[v] = distances[u] + weight;
                            predecessors[v] = u;
                        }
                    }
                }
            }

            // Check for negative weight cycles.
            foreach (var vertex in graph.Vertices)
            {
                if (vertex == null)
                {
                    continue;
                }

                var u = vertex;
                foreach (var neighbor in graph.GetNeighbors(vertex))
                {
                    if (neighbor == null)
                    {
                        continue;
                    }

                    var v = neighbor;
                    var weight = graph.AdjacentDistance(u, v);
                    if (distances[u] + weight < distances[v])
                    {
                        throw new Exception("Graph contains a negative weight cycle.");
                    }
                }
            }

            return distances;
        }
    }
}
