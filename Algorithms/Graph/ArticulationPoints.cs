using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Graph;

/// <summary>
/// Finds articulation points (cut vertices) in an undirected graph.
/// An articulation point is a vertex whose removal increases the number of connected components.
/// </summary>
public static class ArticulationPoints
{
    /// <summary>
    /// Finds all articulation points in an undirected graph.
    /// </summary>
    /// <typeparam name="T">Type of vertex.</typeparam>
    /// <param name="vertices">All vertices in the graph.</param>
    /// <param name="getNeighbors">Function to get neighbors of a vertex.</param>
    /// <returns>Set of articulation points.</returns>
    public static HashSet<T> Find<T>(
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
            return new HashSet<T>();
        }

        var articulationPoints = new HashSet<T>();
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
                    ArticulationPoints = articulationPoints,
                };
                Dfs(vertex, ref time, state, getNeighbors);
            }
        }

        return articulationPoints;
    }

    /// <summary>
    /// Checks if a vertex is an articulation point.
    /// </summary>
    /// <typeparam name="T">Type of vertex.</typeparam>
    /// <param name="vertex">Vertex to check.</param>
    /// <param name="vertices">All vertices in the graph.</param>
    /// <param name="getNeighbors">Function to get neighbors of a vertex.</param>
    /// <returns>True if vertex is an articulation point.</returns>
    public static bool IsArticulationPoint<T>(
        T vertex,
        IEnumerable<T> vertices,
        Func<T, IEnumerable<T>> getNeighbors) where T : notnull
    {
        var articulationPoints = Find(vertices, getNeighbors);
        return articulationPoints.Contains(vertex);
    }

    /// <summary>
    /// Counts the number of articulation points in the graph.
    /// </summary>
    /// <typeparam name="T">Type of vertex.</typeparam>
    /// <param name="vertices">All vertices in the graph.</param>
    /// <param name="getNeighbors">Function to get neighbors of a vertex.</param>
    /// <returns>Number of articulation points.</returns>
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

        int children = 0;

        foreach (var v in getNeighbors(u))
        {
            if (!state.Visited.Contains(v))
            {
                children++;
                state.Parent[v] = u;
                Dfs(v, ref time, state, getNeighbors);

                state.Low[u] = Math.Min(state.Low[u], state.Low[v]);

                // Check if u is an articulation point
                bool isRoot = !state.Parent.ContainsKey(u);
                if (isRoot && children > 1)
                {
                    state.ArticulationPoints.Add(u);
                }

                bool isNonRootArticulation = state.Parent.ContainsKey(u) && state.Low[v] >= state.DiscoveryTime[u];
                if (isNonRootArticulation)
                {
                    state.ArticulationPoints.Add(u);
                }
            }
            else if (!EqualityComparer<T>.Default.Equals(v, state.Parent.GetValueOrDefault(u)))
            {
                // Back edge: update low value
                state.Low[u] = Math.Min(state.Low[u], state.DiscoveryTime[v]);
            }
        }
    }

    /// <summary>
    /// Encapsulates the state for DFS traversal in articulation point detection.
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
        /// Gets set of detected articulation points.
        /// </summary>
        public required HashSet<T> ArticulationPoints { get; init; }
    }
}
