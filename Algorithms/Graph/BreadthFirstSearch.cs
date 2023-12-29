using System;
using System.Collections.Generic;
using DataStructures.Graph;

namespace Algorithms.Graph;

/// <summary>
/// Breadth First Search - algorithm for traversing graph.
/// Algorithm starts from root node that is selected by the user.
/// Algorithm explores all nodes at the present depth.
/// </summary>
/// <typeparam name="T">Vertex data type.</typeparam>
public class BreadthFirstSearch<T> : IGraphSearch<T> where T : IComparable<T>
{
    /// <summary>
    /// Traverses graph from start vertex.
    /// </summary>
    /// <param name="graph">Graph instance.</param>
    /// <param name="startVertex">Vertex that search starts from.</param>
    /// <param name="action">Action that needs to be executed on each graph vertex.</param>
    public void VisitAll(IDirectedWeightedGraph<T> graph, Vertex<T> startVertex, Action<Vertex<T>>? action = default)
    {
        Bfs(graph, startVertex, action, new HashSet<Vertex<T>>());
    }

    /// <summary>
    /// Traverses graph from start vertex.
    /// </summary>
    /// <param name="graph">Graph instance.</param>
    /// <param name="startVertex">Vertex that search starts from.</param>
    /// <param name="action">Action that needs to be executed on each graph vertex.</param>
    /// <param name="visited">Hash set with visited vertices.</param>
    private void Bfs(IDirectedWeightedGraph<T> graph, Vertex<T> startVertex, Action<Vertex<T>>? action, HashSet<Vertex<T>> visited)
    {
        var queue = new Queue<Vertex<T>>();

        queue.Enqueue(startVertex);

        while (queue.Count > 0)
        {
            var currentVertex = queue.Dequeue();

            if (currentVertex == null || visited.Contains(currentVertex))
            {
                continue;
            }

            foreach (var vertex in graph.GetNeighbors(currentVertex))
            {
                queue.Enqueue(vertex!);
            }

            action?.Invoke(currentVertex);

            visited.Add(currentVertex);
        }
    }
}
