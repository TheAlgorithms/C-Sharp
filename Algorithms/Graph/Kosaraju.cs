using System.Collections.Generic;
using System.Linq;
using DataStructures.Graph;

namespace Algorithms.Graph;

/// <summary>
/// Implementation of Kosaraju-Sharir's algorithm (also known as Kosaraju's algorithm) to find the
/// strongly connected components (SCC) of a directed graph.
/// See https://en.wikipedia.org/wiki/Kosaraju%27s_algorithm.
/// </summary>
/// <typeparam name="T">Vertex data type.</typeparam>
public static class Kosaraju<T>
{
    /// <summary>
    /// First DFS for Kosaraju algorithm: traverse the graph creating a reverse order explore list <paramref name="reversed"/>.
    /// </summary>
    /// <param name="v">Vertex to explore.</param>
    /// <param name="graph">Graph instance.</param>
    /// <param name="visited">List of already visited vertex.</param>
    /// <param name="reversed">Reversed list of vertex for the second DFS.</param>
    public static void Visit(Vertex<T> v, IDirectedWeightedGraph<T> graph, HashSet<Vertex<T>> visited, Stack<Vertex<T>> reversed)
    {
        if (visited.Contains(v))
        {
            return;
        }

        // Set v as visited
        visited.Add(v);

        // Push v in the stack.
        // This can also be done with a List, inserting v at the begining of the list
        // after visit the neighbors.
        reversed.Push(v);

        // Visit neighbors
        foreach (var u in graph.GetNeighbors(v))
        {
            Visit(u!, graph, visited, reversed);
        }
    }

    /// <summary>
    /// Second DFS for Kosaraju algorithm. Traverse the graph in reversed order
    /// assigning a root vertex for every vertex that belong to the same SCC.
    /// </summary>
    /// <param name="v">Vertex to assign.</param>
    /// <param name="root">Root vertext, representative of the SCC.</param>
    /// <param name="graph">Graph with vertex and edges.</param>
    /// <param name="roots">
    /// Dictionary that assigns to each vertex the root of the SCC to which it corresponds.
    /// </param>
    public static void Assign(Vertex<T> v, Vertex<T> root, IDirectedWeightedGraph<T> graph, Dictionary<Vertex<T>, Vertex<T>> roots)
    {
        // If v already has a representative vertex (root) already assigned, do nothing.
        if (roots.ContainsKey(v))
        {
            return;
        }

        // Assign the root to the vertex.
        roots.Add(v, root);

        // Assign the current root vertex to v neighbors.
        foreach (var u in graph.GetNeighbors(v))
        {
            Assign(u!, root, graph, roots);
        }
    }

    /// <summary>
    /// Find the representative vertex of the SCC for each vertex on the graph.
    /// </summary>
    /// <param name="graph">Graph to explore.</param>
    /// <returns>A dictionary that assigns to each vertex a root vertex of the SCC they belong. </returns>
    public static Dictionary<Vertex<T>, Vertex<T>> GetRepresentatives(IDirectedWeightedGraph<T> graph)
    {
        HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();
        Stack<Vertex<T>> reversedL = new Stack<Vertex<T>>();
        Dictionary<Vertex<T>, Vertex<T>> representatives = new Dictionary<Vertex<T>, Vertex<T>>();

        foreach (var v in graph.Vertices)
        {
            if (v != null)
            {
                Visit(v, graph, visited, reversedL);
            }
        }

        visited.Clear();

        while (reversedL.Count > 0)
        {
            Vertex<T> v = reversedL.Pop();
            Assign(v, v, graph, representatives);
        }

        return representatives;
    }

    /// <summary>
    /// Get the Strongly Connected Components for the graph.
    /// </summary>
    /// <param name="graph">Graph to explore.</param>
    /// <returns>An array of SCC.</returns>
    public static IEnumerable<Vertex<T>>[] GetScc(IDirectedWeightedGraph<T> graph)
    {
        var representatives = GetRepresentatives(graph);
        Dictionary<Vertex<T>, List<Vertex<T>>> scc = new Dictionary<Vertex<T>, List<Vertex<T>>>();
        foreach (var kv in representatives)
        {
            // Assign all vertex (key) that have the seem root (value) to a single list.
            if (scc.ContainsKey(kv.Value))
            {
                scc[kv.Value].Add(kv.Key);
            }
            else
            {
                scc.Add(kv.Value, new List<Vertex<T>> { kv.Key });
            }
        }

        return scc.Values.ToArray();
    }
}
