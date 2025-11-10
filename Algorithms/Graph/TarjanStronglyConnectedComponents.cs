using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Graph;

/// <summary>
/// Tarjan's algorithm for finding strongly connected components in a directed graph.
/// Uses depth-first search with a stack to identify SCCs in O(V + E) time.
/// </summary>
public class TarjanStronglyConnectedComponents
{
    private readonly List<int>[] graph;
    private readonly int[] ids;
    private readonly int[] low;
    private readonly bool[] onStack;
    private readonly Stack<int> stack;
    private readonly List<List<int>> sccs;
    private int id;

    public TarjanStronglyConnectedComponents(int vertices)
    {
        graph = new List<int>[vertices];
        ids = new int[vertices];
        low = new int[vertices];
        onStack = new bool[vertices];
        stack = new Stack<int>();
        sccs = new List<List<int>>();

        for (int i = 0; i < vertices; i++)
        {
            graph[i] = new List<int>();
            ids[i] = -1;
        }
    }

    /// <summary>
    /// Adds a directed edge from u to v.
    /// </summary>
    public void AddEdge(int u, int v)
    {
        if (u < 0 || u >= graph.Length || v < 0 || v >= graph.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(u), "Vertex indices must be within valid range.");
        }

        graph[u].Add(v);
    }

    /// <summary>
    /// Finds all strongly connected components.
    /// </summary>
    /// <returns>List of SCCs, where each SCC is a list of vertex indices.</returns>
    public List<List<int>> FindSCCs()
    {
        for (int i = 0; i < graph.Length; i++)
        {
            if (ids[i] == -1)
            {
                Dfs(i);
            }
        }

        return sccs;
    }

    /// <summary>
    /// Gets the number of strongly connected components.
    /// </summary>
    public int GetSccCount() => sccs.Count;

    /// <summary>
    /// Checks if two vertices are in the same SCC.
    /// </summary>
    public bool InSameScc(int u, int v)
    {
        if (sccs.Count == 0)
        {
            FindSCCs();
        }

        foreach (var scc in sccs)
        {
            if (scc.Contains(u) && scc.Contains(v))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Gets the SCC containing the given vertex.
    /// </summary>
    public List<int>? GetScc(int vertex)
    {
        if (sccs.Count == 0)
        {
            FindSCCs();
        }

        return sccs.FirstOrDefault(scc => scc.Contains(vertex));
    }

    /// <summary>
    /// Builds the condensation graph (DAG of SCCs).
    /// </summary>
    public List<int>[] BuildCondensationGraph()
    {
        if (sccs.Count == 0)
        {
            FindSCCs();
        }

        var sccIndex = new int[graph.Length];
        for (int i = 0; i < sccs.Count; i++)
        {
            foreach (var vertex in sccs[i])
            {
                sccIndex[vertex] = i;
            }
        }

        var condensation = new List<int>[sccs.Count];
        for (int i = 0; i < sccs.Count; i++)
        {
            condensation[i] = new List<int>();
        }

        var edges = new HashSet<(int, int)>();
        for (int u = 0; u < graph.Length; u++)
        {
            foreach (var v in graph[u])
            {
                int sccU = sccIndex[u];
                int sccV = sccIndex[v];

                if (sccU != sccV && !edges.Contains((sccU, sccV)))
                {
                    condensation[sccU].Add(sccV);
                    edges.Add((sccU, sccV));
                }
            }
        }

        return condensation;
    }

    private void Dfs(int at)
    {
        stack.Push(at);
        onStack[at] = true;
        ids[at] = low[at] = id++;

        foreach (var to in graph[at])
        {
            if (ids[to] == -1)
            {
                Dfs(to);
            }

            if (onStack[to])
            {
                low[at] = Math.Min(low[at], low[to]);
            }
        }

        if (ids[at] == low[at])
        {
            var scc = new List<int>();
            while (true)
            {
                int node = stack.Pop();
                onStack[node] = false;
                scc.Add(node);
                if (node == at)
                {
                    break;
                }
            }

            sccs.Add(scc);
        }
    }
}
