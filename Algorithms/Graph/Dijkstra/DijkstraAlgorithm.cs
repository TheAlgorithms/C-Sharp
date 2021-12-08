using System;
using System.Collections.Generic;
using System.Linq;
using DataStructures.Graph;

namespace Algorithms.Graph.Dijkstra
{
    public class DijkstraAlgorithm
    {
        public List<DistanceModel<T>> GenerateShortestPath<T>(DirectedWeightedGraph<T> graph, Vertex<T> start)
        {
            var visitedVertices = new List<Vertex<T>?>();

            // 1. Check that vertex belongs to graph
            if (start.Graph != null && !start.Graph.Equals(graph))
            {
                throw new InvalidOperationException("Vertex does not belong to graph.");
            }

            // 2. Initialize distance table with start vertex, set distance to 0
            // According to the algorithm, we should initialize table with infinity distances
            // However, we assume that if list doesn't contains a record for the pair of vertices
            // then it is infinity
            var distanceList = new List<DistanceModel<T>>
            {
                new(start, start, 0),
            };

            var currentVertex = start;

            // 3. Initialize the integer variable, which indicates the current passed path
            var currentPath = 0d;

            while (visitedVertices.Count < graph.Count)
            {
                // 4. Mark current vertex as visited
                visitedVertices.Add(currentVertex);

                if (currentVertex is null)
                {
                    throw new InvalidOperationException();
                }

                // 5. Get the edges to all adjacent vertices
                var neighborVertices = graph.GetNeighbors(currentVertex).ToArray();

                // 6. Examine all the adjacent vertices, but do not mark them as visited
                foreach (var vertex in neighborVertices)
                {
                    var adjacentDistance = graph.AdjacentDistance(currentVertex, vertex!);

                    // 7. If the distance table contains a distance for the vertex and it is greater
                    // then current path: update distance
                    if (ContainsAndGreater(distanceList, vertex, currentPath + adjacentDistance))
                    {
                        var distance = GetDistanceByEndVertex(distanceList, vertex);
                        distance.Distance = currentPath + adjacentDistance;
                        distance.PreviousVertex = currentVertex;
                        continue;
                    }

                    // 8. If the distance table doesn't contain a distance for particular pair of vertices: add it.
                    if (!Contains(distanceList, vertex))
                    {
                        distanceList.Add(new DistanceModel<T>
                        {
                            Vertex = vertex,
                            PreviousVertex = currentVertex,
                            Distance = currentPath + adjacentDistance,
                        });
                    }
                }

                // 9. If there are any adjacent vertices which is unvisited: exit loop
                if (!neighborVertices.Any())
                {
                    break;
                }

                // 10. Get the minimal weighted edge to the adjacent unvisited vertex
                var minimalAdjacentVertex = GetMinimalAdjacentVertex(graph, currentVertex, neighborVertices);

                // 11. Update current path: add to it the weight of minimal edge, leading to the next unvisited vertex.
                currentPath += graph.AdjacentDistance(currentVertex, minimalAdjacentVertex!);

                // 12. Update current vertex
                currentVertex = minimalAdjacentVertex;
            }

            return distanceList;
        }

        private static Vertex<T>? GetMinimalAdjacentVertex<T>(
            IDirectedWeightedGraph<T> graph,
            Vertex<T> startVertex,
            IEnumerable<Vertex<T>?> adjacentVertices)
        {
            var minDistance = double.MaxValue;
            Vertex<T>? minVertex = default;

            foreach (var vertex in adjacentVertices)
            {
                var currentDistance = graph.AdjacentDistance(startVertex, vertex!);

                if (minDistance > currentDistance)
                {
                    minDistance = currentDistance;
                    minVertex = vertex;
                }
            }

            return minVertex;
        }

        private static DistanceModel<T> GetDistanceByEndVertex<T>(
            IEnumerable<DistanceModel<T>> distances,
            Vertex<T>? endVertex)
        {
            return distances.First(x =>
                x.Vertex != null &&
                x.Vertex.Equals(endVertex));
        }

        private static bool ContainsAndGreater<T>(
            IEnumerable<DistanceModel<T>?> distances,
            Vertex<T>? vertex,
            double value)
        {
            return distances.Any(x =>
                x is { Vertex: { } } && x.Vertex.Equals(vertex) && x.Distance > value);
        }

        private static bool Contains<T>(IEnumerable<DistanceModel<T>> distances, Vertex<T>? vertex)
        {
            return distances.Any(x =>
                x.Vertex != null &&
                x.Vertex.Equals(vertex));
        }
    }
}
