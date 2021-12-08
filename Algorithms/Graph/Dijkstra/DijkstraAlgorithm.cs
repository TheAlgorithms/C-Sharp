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
            // // 1. Check that vertex belongs to graph
            // if (!graph.ContainsVertex(start))
            // {
            //     throw new InvalidOperationException("Vertex does not belong to graph");
            // }
            //
            // // 2. Initialize distance table with start vertex, set distance to 0
            // // According to the algorithm, we should initialize table with infinity distances
            // // However, we assume that if list doesn't contains a record for the pair of vertices
            // // then it is infinity
            // var distanceList = new List<DistanceModel<T>>
            // {
            //     new DistanceModel<T>(start, start, 0)
            // };
            //
            // var currentVertex = start;
            //
            // // 3. Initialize the integer variable, which indicates the current passed path
            // var currentPath = 0;
            //
            // while (graph.UnvisitedVertices().Any())
            // {
            //     // 4. Mark current vertex as visited
            //     currentVertex.Visit();
            //
            //     // 5. Get the edges to all adjacent vertices
            //     var edgesToAdjacentUnvisitedVertices = graph
            //         .EdgesToAdjacentUnvisitedVertices(currentVertex);
            //
            //     // 6. Examine all the adjacent vertices, but do not mark them as visited
            //     foreach (var edge in edgesToAdjacentUnvisitedVertices)
            //     {
            //         // 7. If the distance table contains a distance for the vertex and it is greater
            //         // then current path: update distance
            //         if (ContainsAndGreater(distanceList, edge.EndVertex, currentPath + edge.Weight))
            //         {
            //             var distance = GetDistanceModelByEndVertex(distanceList, edge.EndVertex);
            //             distance.Distance = currentPath + edge.Weight;
            //             distance.PreviousVertex = currentVertex;
            //             continue;
            //         }
            //
            //         // 8. If the distance table doesn't contain a distance for particular pair of vertices: add it.
            //         if (!Contains(distanceList, edge.EndVertex))
            //         {
            //             distanceList.Add(new DistanceModel<T>
            //             {
            //                 Vertex = edge.EndVertex,
            //                 PreviousVertex = currentVertex,
            //                 Distance = currentPath + edge.Weight,
            //             });
            //         }
            //     }
            //
            //     // 9. If there are any adjacent vertices which is unvisited: exit loop
            //     if (!edgesToAdjacentUnvisitedVertices.Any())
            //     {
            //         break;
            //     }
            //
            //     // 10. Get the minimal weighted edge to the adjacent unvisited vertex
            //     var minimalAdjacentEdge = GetMinimalAdjacentEdge(edgesToAdjacentUnvisitedVertices);
            //
            //     // 11. Update current path: add to it the weight of minimal edge, leading to the next unvisited vertex.
            //     currentPath += minimalAdjacentEdge.Weight;
            //
            //     // 12. Update current vertex
            //     currentVertex = minimalAdjacentEdge.EndVertex;
            // }
            //
            // return distanceList;
            throw new NotImplementedException();
        }

        // private static IEdge<T> GetMinimalAdjacentEdge(IReadOnlyCollection<IEdge<T>> edges)
        // {
        //     var minWeight = edges.Min(t => t.Weight);
        //     return edges.First(x => x.Weight == minWeight);
        // }
        //
        // private static DistanceModel<T> GetDistanceModelByEndVertex(IEnumerable<DistanceModel<T>> distances,
        //     IVertex<T> endVertex)
        // {
        //     return distances.First(x => x.Vertex.Equals(endVertex));
        // }
        //
        // private static bool ContainsAndGreater(IEnumerable<DistanceModel<T>> distances, IVertex<T> vertex,
        //     int value)
        // {
        //     return distances.Any(x => x.Vertex.Equals(vertex) && x.Distance > value);
        // }
        //
        // private static bool Contains(IEnumerable<DistanceModel<T>> distances, IVertex<T> vertex)
        // {
        //     return distances.Any(x => x.Vertex.Equals(vertex));
        // }
    }
}
