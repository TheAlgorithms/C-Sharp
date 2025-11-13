using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Graph;

/// <summary>
/// Finds bridges (cut edges) in an undirected graph.
/// A bridge is an edge whose removal increases the number of connected components.
/// </summary>
public static class Bridges
{
    /// <summary>
    /// Finds all bridges in an undirected graph.
    /// </summary>
    /// <typeparam name="T">Type of vertex.</typeparam>
    /// <param name="vertices">All vertices in the graph.</param>
    /// <param name="getNeighbors">Function to get neighbors of a vertex.</param>
    /// <returns>Set of bridges as tuples of vertices.</returns>
    public static HashSet<(T From, T To)> Find<T>(
        IEnumerable<T> vertices,
        Func<T, IEnumerable<T>> getNeighbors) where T : notnull
    {
        if (vertices == null)
        {
            throw new ArgumentNullException(nameof(vertices));
        }

        if (getNeighbors == null)
        {
            throw new ArgumentNullException(nameof(getNeighbors));
        }

        var vertexList = vertices.ToList();
        if (vertexList.Count == 0)
        {
            return new HashSet<(T, T)>();
        }

        var bridges = new HashSet<(T, T)>();
        var visited = new HashSet<T>();
        var discoveryTime = new Dictionary<T, int>();
        var low = new Dictionary<T, int>();
        var parent = new Dictionary<T, T?>();
        var time = 0;

        foreach (var vertex in vertexList)
        {
            if (!visited.Contains(vertex))
            {
                var state = new DfsState<T>
                {
                    Visited = visited,
                    DiscoveryTime = discoveryTime,
                    Low = low,
                    Parent = parent,
                    Bridges = bridges,
                };
                Dfs(vertex, ref time, state, getNeighbors);
            }
        }

        return bridges;
    }

    /// <summary>
    /// Checks if an edge is a bridge.
    /// </summary>
    /// <typeparam name="T">Type of vertex.</typeparam>
    /// <param name="from">Source vertex.</param>
    /// <param name="to">Destination vertex.</param>
    /// <param name="vertices">All vertices in the graph.</param>
    /// <param name="getNeighbors">Function to get neighbors of a vertex.</param>
    /// <returns>True if edge is a bridge.</returns>
    public static bool IsBridge<T>(
        T from,
        T to,
        IEnumerable<T> vertices,
        Func<T, IEnumerable<T>> getNeighbors) where T : notnull
    {
        var bridges = Find(vertices, getNeighbors);
        return bridges.Contains((from, to)) || bridges.Contains((to, from));
    }

    /// <summary>
    /// Counts the number of bridges in the graph.
    /// </summary>
    /// <typeparam name="T">Type of vertex.</typeparam>
    /// <param name="vertices">All vertices in the graph.</param>
    /// <param name="getNeighbors">Function to get neighbors of a vertex.</param>
    /// <returns>Number of bridges.</returns>
    public static int Count<T>(
        IEnumerable<T> vertices,
        Func<T, IEnumerable<T>> getNeighbors) where T : notnull
    {
        return Find(vertices, getNeighbors).Count;
    }

    private static void Dfs<T>(
        T u,
        ref int time,
        DfsState<T> state,
        Func<T, IEnumerable<T>> getNeighbors) where T : notnull
    {
        state.Visited.Add(u);
        state.DiscoveryTime[u] = time;
        state.Low[u] = time;
        time++;

        foreach (var v in getNeighbors(u))
        {
            if (!state.Visited.Contains(v))
            {
                state.Parent[v] = u;
                Dfs(v, ref time, state, getNeighbors);

                state.Low[u] = Math.Min(state.Low[u], state.Low[v]);

                // Check if edge u-v is a bridge
                if (state.Low[v] > state.DiscoveryTime[u])
                {
                    state.Bridges.Add((u, v));
                }
            }
            else if (!EqualityComparer<T>.Default.Equals(v, state.Parent.GetValueOrDefault(u)))
            {
                // Back edge: update low value
                state.Low[u] = Math.Min(state.Low[u], state.DiscoveryTime[v]);
            }
            else
            {
                // Edge to parent: no action needed
            }
        }
    }

    /// <summary>
    /// Encapsulates the state for DFS traversal in bridge detection.
    /// </summary>
    /// <typeparam name="T">Type of vertex.</typeparam>
    private sealed class DfsState<T>
        where T : notnull
    {
        /// <summary>
        /// Gets set of visited vertices.
        /// </summary>
        public required HashSet<T> Visited { get; init; }

        /// <summary>
        /// Gets discovery time for each vertex.
        /// </summary>
        public required Dictionary<T, int> DiscoveryTime { get; init; }

        /// <summary>
        /// Gets lowest discovery time reachable from each vertex.
        /// </summary>
        public required Dictionary<T, int> Low { get; init; }

        /// <summary>
        /// Gets parent vertex in DFS tree.
        /// </summary>
        public required Dictionary<T, T?> Parent { get; init; }

        /// <summary>
        /// Gets set of detected bridges.
        /// </summary>
        public required HashSet<(T, T)> Bridges { get; init; }
    }
}
