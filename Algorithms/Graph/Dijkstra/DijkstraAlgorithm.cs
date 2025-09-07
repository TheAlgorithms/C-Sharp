using DataStructures.Graph;

namespace Algorithms.Graph.Dijkstra;

public static class DijkstraAlgorithm
{
    /// <summary>
    /// Implementation of the Dijkstra shortest path algorithm for cyclic graphs.
    /// https://en.wikipedia.org/wiki/Dijkstra%27s_algorithm.
    /// </summary>
    /// <param name="graph">Graph instance.</param>
    /// <param name="startVertex">Starting vertex instance.</param>
    /// <typeparam name="T">Generic Parameter.</typeparam>
    /// <returns>List of distances from current vertex to all other vertices.</returns>
    /// <exception cref="InvalidOperationException">Exception thrown in case when graph is null or start
    /// vertex does not belong to graph instance.</exception>
    public static DistanceModel<T>[] GenerateShortestPath<T>(DirectedWeightedGraph<T> graph, Vertex<T> startVertex)
    {
        ValidateGraphAndStartVertex(graph, startVertex);

        var visitedVertices = new List<Vertex<T>>();

        var distanceArray = InitializeDistanceArray(graph, startVertex);

        var distanceRecord = new PriorityQueue<DistanceModel<T>, double>();

        distanceRecord.Enqueue(distanceArray[0], distanceArray[0].Distance);

        while (visitedVertices.Count != distanceArray.Length && distanceRecord.Count != 0)
        {
            while (visitedVertices.Contains(distanceRecord.Peek().Vertex!))
            {
                distanceRecord.Dequeue();
            }

            var minDistance = distanceRecord.Dequeue();

            var currentPath = minDistance.Distance;

            visitedVertices.Add(minDistance.Vertex!);

            var neighborVertices = graph
                .GetNeighbors(minDistance.Vertex!)
                .Where(x => x != null && !visitedVertices.Contains(x))
                .ToList();

            foreach (var vertex in neighborVertices)
            {
                var adjacentDistance = graph.AdjacentDistance(minDistance.Vertex!, vertex!);

                var distance = distanceArray[vertex!.Index];

                var fullDistance = currentPath + adjacentDistance;

                if (distance.Distance > fullDistance)
                {
                    distance.Distance = fullDistance;
                    distance.PreviousVertex = minDistance.Vertex;
                    distanceRecord.Enqueue(distance, fullDistance);
                }
            }
        }

        return distanceArray;
    }

    private static DistanceModel<T>[] InitializeDistanceArray<T>(
        IDirectedWeightedGraph<T> graph,
        Vertex<T> startVertex)
    {
        var distArray = new DistanceModel<T>[graph.Count];

        distArray[startVertex.Index] = new DistanceModel<T>(startVertex, startVertex, 0);

        foreach (var vertex in graph.Vertices.Where(x => x != null && !x.Equals(startVertex)))
        {
            distArray[vertex!.Index] = new DistanceModel<T>(vertex, null, double.MaxValue);
        }

        return distArray;
    }

    private static void ValidateGraphAndStartVertex<T>(DirectedWeightedGraph<T> graph, Vertex<T> startVertex)
    {
        if (graph is null)
        {
            throw new ArgumentNullException(nameof(graph));
        }

        if (startVertex.Graph != null && !startVertex.Graph.Equals(graph))
        {
            throw new ArgumentNullException(nameof(graph));
        }
    }
}
