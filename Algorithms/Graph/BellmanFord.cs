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
        private DirectedWeightedGraph<T> graph;
        private Dictionary<Vertex<T>, double> distances;
        private Dictionary<Vertex<T>, Vertex<T>?> predecessors;

        public BellmanFord(DirectedWeightedGraph<T> graph)
        {
            this.graph = graph;
            distances = new Dictionary<Vertex<T>, double>();
            predecessors = new Dictionary<Vertex<T>, Vertex<T>?>();
        }

        /// <summary>
        /// Runs the Bellman-Ford algorithm to find the shortest distances from the source vertex to all other vertices.
        /// </summary>
        /// <param name="sourceVertex">Source vertex for shortest path calculation.</param>
        /// <returns>
        /// A dictionary containing the shortest distances from the source vertex to all other vertices.
        /// If a vertex is unreachable from the source, it will have a value of double.PositiveInfinity.
        /// </returns>
        public Dictionary<Vertex<T>, double> Run(Vertex<T> sourceVertex)
        {
            InitializeDistances(sourceVertex);
            RelaxEdges();
            CheckForNegativeCycles();
            return distances;
        }

        private void InitializeDistances(Vertex<T> sourceVertex)
        {
            foreach (var vertex in graph.Vertices)
            {
                if (vertex != null)
                {
                    distances[vertex] = double.PositiveInfinity;
                    predecessors[vertex] = null;
                }
            }

            distances[sourceVertex] = 0;
        }

        private void RelaxEdges()
        {
            int vertexCount = graph.Count;

            for (int i = 0; i < vertexCount - 1; i++)
            {
                foreach (var vertex in graph.Vertices)
                {
                    if (vertex != null)
                    {
                        var u = vertex;

                        foreach (var neighbor in graph.GetNeighbors(vertex))
                        {
                            if (neighbor != null)
                            {
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
                }
            }
        }

        private void CheckForNegativeCycles()
        {
            foreach (var vertex in graph.Vertices)
            {
                if (vertex != null)
                {
                    var u = vertex;

                    foreach (var neighbor in graph.GetNeighbors(vertex))
                    {
                        if (neighbor != null)
                        {
                            var v = neighbor;
                            var weight = graph.AdjacentDistance(u, v);

                            if (distances[u] + weight < distances[v])
                            {
                                throw new Exception("Graph contains a negative weight cycle.");
                            }
                        }
                    }
                }
            }
        }
    }
}
