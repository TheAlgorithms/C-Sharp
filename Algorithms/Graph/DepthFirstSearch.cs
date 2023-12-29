using System;
using System.Collections.Generic;
using DataStructures.Graph;

namespace Algorithms.Graph;

/// <summary>
/// Depth First Search - algorithm for traversing graph.
/// Algorithm starts from root node that is selected by the user.
/// Algorithm explores as far as possible along each branch before backtracking.
/// </summary>
/// <typeparam name="T">Vertex data type.</typeparam>
public class DepthFirstSearch<T> : IGraphSearch<T> where T : IComparable<T>
{
    /// <summary>
    /// Traverses graph from start vertex.
    /// </summary>
    /// <param name="graph">Graph instance.</param>
    /// <param name="startVertex">Vertex that search starts from.</param>
    /// <param name="action">Action that needs to be executed on each graph vertex.</param>
    public void VisitAll(IDirectedWeightedGraph<T> graph, Vertex<T> startVertex, Action<Vertex<T>>? action = default)
    {
        Dfs(graph, startVertex, action, new HashSet<Vertex<T>>());
    }

    /// <summary>
    /// Traverses graph from start vertex.
    /// </summary>
    /// <param name="graph">Graph instance.</param>
    /// <param name="startVertex">Vertex that search starts from.</param>
    /// <param name="action">Action that needs to be executed on each graph vertex.</param>
    /// <param name="visited">Hash set with visited vertices.</param>
    private void Dfs(IDirectedWeightedGraph<T> graph, Vertex<T> startVertex, Action<Vertex<T>>? action, HashSet<Vertex<T>> visited)
    {
        action?.Invoke(startVertex);

        visited.Add(startVertex);

        foreach (var vertex in graph.GetNeighbors(startVertex))
        {
            if (vertex == null || visited.Contains(vertex))
            {
                continue;
            }

            Dfs(graph, vertex!, action, visited);
        }
    }
}
