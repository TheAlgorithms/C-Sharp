using System;
using DataStructures.Graph;

namespace Algorithms.Graph;

public interface IGraphSearch<T>
{
    /// <summary>
    /// Traverses graph from start vertex.
    /// </summary>
    /// <param name="graph">Graph instance.</param>
    /// <param name="startVertex">Vertex that search starts from.</param>
    /// <param name="action">Action that needs to be executed on each graph vertex.</param>
    void VisitAll(IDirectedWeightedGraph<T> graph, Vertex<T> startVertex, Action<Vertex<T>>? action = null);
}
