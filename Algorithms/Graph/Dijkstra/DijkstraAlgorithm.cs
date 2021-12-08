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
        public Dictionary<int, DistanceModel<T>> GenerateShortestPath<T>(
            DirectedWeightedGraph<T> graph,
            Vertex<T> startVertex)
        {
            if (startVertex.Graph is null)
            {
                throw new InvalidOperationException($"Graph is null {nameof(graph)}.");
            }

            var visitedVertices = new List<Vertex<T>?>();

            // 1. Check that vertex belongs to graph
            if (!startVertex.Graph.Equals(graph))
            {
                throw new InvalidOperationException($"Vertex does not belong to graph {nameof(startVertex)}.");
            }

            // 2. Initialize distance table with start vertex, set distance to 0
            // According to the algorithm, we should initialize table with infinity distances
            // However, we assume that if list doesn't contains a record for the pair of vertices
            // then it is infinity
            var distanceDictionary = new Dictionary<int, DistanceModel<T>>
            {
                [startVertex.Index] = new(startVertex, startVertex, 0),
            };

            foreach (var vertex in graph.Vertices.Where(x => x != null && !x.Equals(startVertex)))
            {
                if (vertex != null)
                {
                    distanceDictionary.Add(
                        vertex.Index,
                        new DistanceModel<T>(vertex, null, double.MaxValue));
                }
            }

            var currentVertex = startVertex;

            // 3. Initialize the float variable which indicates the total passed path
            var currentPath = 0d;

            while (visitedVertices.Count < graph.Count)
            {
                // 4. Mark current vertex as visited
                visitedVertices.Add(currentVertex);

                // 5. Get the edges to all adjacent vertices
                var neighborVertices = graph.GetNeighbors(currentVertex).ToArray();

                // 6. Examine all the adjacent vertices, but do not mark them as visited
                foreach (var vertex in neighborVertices)
                {
                    if (vertex is null)
                    {
                        throw new InvalidOperationException($"Vertex is null {nameof(vertex)}");
                    }

                    var adjacentDistance = graph.AdjacentDistance(currentVertex, vertex);

                    // 7. If the distance table contains a distance for the vertex and it is greater
                    // then current path: update distance
                    if (distanceDictionary.TryGetValue(vertex.Index, out var v) &&
                        v.Distance > currentPath + adjacentDistance)
                    {
                        v.Distance = currentPath + adjacentDistance;
                        v.PreviousVertex = currentVertex;
                    }
                }

                // 8. Get the minimal weighted edge to the adjacent unvisited vertex
                var minimalAdjacentVertex = GetMinimalUnvisitedAdjacentVertex(
                    graph,
                    currentVertex,
                    neighborVertices,
                    visitedVertices);

                // 9. If there are any adjacent vertices which is unvisited: exit loop
                if (neighborVertices.Length == 0 || minimalAdjacentVertex is null)
                {
                    break;
                }

                // 10. Update current path: add to it the weight of minimal edge, leading to the next unvisited vertex.
                currentPath += graph.AdjacentDistance(currentVertex, minimalAdjacentVertex);

                // 11. Update current vertex
                currentVertex = minimalAdjacentVertex;
            }

            return distanceDictionary;
        }

        private static Vertex<T>? GetMinimalUnvisitedAdjacentVertex<T>(
            IDirectedWeightedGraph<T> graph,
            Vertex<T> startVertex,
            IEnumerable<Vertex<T>?> adjacentVertices,
            ICollection<Vertex<T>?> visitedVertices)
        {
            var minDistance = double.MaxValue;
            Vertex<T>? minVertex = default;

            foreach (var vertex in adjacentVertices)
            {
                var currentDistance = graph.AdjacentDistance(startVertex, vertex!);

                if (minDistance > currentDistance && !visitedVertices.Contains(vertex))
                {
                    minDistance = currentDistance;
                    minVertex = vertex;
                }
            }

            return minVertex;
        }
    }
}
