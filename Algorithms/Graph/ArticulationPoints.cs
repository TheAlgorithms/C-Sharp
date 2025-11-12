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
                DFS(vertex, ref time, visited, discoveryTime, low, parent, articulationPoints, getNeighbors);
            }
        }

        return articulationPoints;
    }

    private static void DFS<T>(
        T u,
        ref int time,
        HashSet<T> visited,
        Dictionary<T, int> discoveryTime,
        Dictionary<T, int> low,
        Dictionary<T, T?> parent,
        HashSet<T> articulationPoints,
        Func<T, IEnumerable<T>> getNeighbors) where T : notnull
    {
        visited.Add(u);
        discoveryTime[u] = time;
        low[u] = time;
        time++;

        int children = 0;

        foreach (var v in getNeighbors(u))
        {
            if (!visited.Contains(v))
            {
                children++;
                parent[v] = u;
                DFS(v, ref time, visited, discoveryTime, low, parent, articulationPoints, getNeighbors);

                low[u] = Math.Min(low[u], low[v]);

                // Check if u is an articulation point
                if (!parent.ContainsKey(u) && children > 1)
                {
                    articulationPoints.Add(u);
                }

                if (parent.ContainsKey(u) && low[v] >= discoveryTime[u])
                {
                    articulationPoints.Add(u);
                }
            }
            else if (!EqualityComparer<T>.Default.Equals(v, parent.GetValueOrDefault(u)))
            {
                low[u] = Math.Min(low[u], discoveryTime[v]);
            }
        }
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
}
