using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Graph;

namespace Algorithms.Graph.Dijkstra
{
    public class DijkstraAlgorithm
    {
        /// <summary>
        /// Implementation of the Dijkstra shortest path algorithm.
        /// https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm.
        /// </summary>
        /// <param name="graph">Graph instance.</param>
        /// <param name="startVertex">Starting vertex instance.</param>
        /// <typeparam name="T">Generic Parameter.</typeparam>
        /// <returns>List of distances from current vertex to all other vertices.</returns>
        /// <exception cref="InvalidOperationException">Exception thrown in case when graph is null or start
        /// vertex does not belong to graph instance.</exception>
        public List<DistanceModel<T>> GenerateShortestPath<T>(DirectedWeightedGraph<T> graph, Vertex<T> startVertex)
        {
            if (startVertex.Graph is null)
            {
                throw new InvalidOperationException($"Graph is null {nameof(graph)}.");
            }

            if (!startVertex.Graph.Equals(graph))
            {
                throw new InvalidOperationException($"Vertex does not belong to graph {nameof(startVertex)}.");
            }

            var visitedVertices = new List<Vertex<T>?>();

            var distanceDictionary = new Dictionary<int, DistanceModel<T>>
            {
                [startVertex.Index] = new(startVertex, startVertex, 0),
            };

            foreach (var vertex in graph.Vertices.Where(x => x != null && !x.Equals(startVertex)))
            {
                distanceDictionary.Add(
                    vertex!.Index,
                    new DistanceModel<T>(vertex, null, double.MaxValue));
            }

            var currentVertex = startVertex;

            var currentPath = 0d;

            while (visitedVertices.Count < graph.Count)
            {
                visitedVertices.Add(currentVertex);

                var neighborVertices = graph.GetNeighbors(currentVertex)
                    .Where(x => !visitedVertices.Contains(x))
                    .ToList();

                foreach (var vertex in neighborVertices)
                {
                    if (vertex is null)
                    {
                        throw new InvalidOperationException($"Vertex is null {nameof(vertex)}");
                    }

                    var adjacentDistance = graph.AdjacentDistance(currentVertex, vertex);

                    var tryGetDistance = distanceDictionary.TryGetValue(vertex.Index, out var distance);

                    if (tryGetDistance && distance!.Distance <= currentPath + adjacentDistance)
                    {
                        continue;
                    }

                    distance!.Distance = currentPath + adjacentDistance;
                    distance.PreviousVertex = currentVertex;
                }

                var minimalAdjacentVertex = GetMinimalUnvisitedAdjacentVertex(graph, currentVertex, neighborVertices);

                if (neighborVertices.Count == 0 || minimalAdjacentVertex is null)
                {
                    break;
                }

                currentPath += graph.AdjacentDistance(currentVertex, minimalAdjacentVertex);

                currentVertex = minimalAdjacentVertex;
            }

            return distanceDictionary.Select(x => x.Value).ToList();
        }

        private static Vertex<T>? GetMinimalUnvisitedAdjacentVertex<T>(
            IDirectedWeightedGraph<T> graph,
            Vertex<T> startVertex,
            IEnumerable<Vertex<T>?> adjacentVertices)
        {
            var minDistance = double.MaxValue;
            Vertex<T>? minVertex = default;

            foreach (var vertex in adjacentVertices)
            {
                var currentDistance = graph.AdjacentDistance(startVertex, vertex!);

                if (minDistance <= currentDistance)
                {
                    continue;
                }

                minDistance = currentDistance;
                minVertex = vertex;
            }

            return minVertex;
        }
    }
}
