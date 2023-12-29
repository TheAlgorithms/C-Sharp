using System;
using System.Collections.Generic;
using DataStructures.DisjointSet;

namespace Algorithms.Graph.MinimumSpanningTree;

/// <summary>
///     Algorithm to determine the minimum spanning forest of an undirected graph.
/// </summary>
/// <remarks>
///     Kruskal's algorithm is a greedy algorithm that can determine the
///     minimum spanning tree or minimum spanning forest of any undirected
///     graph. Unlike Prim's algorithm, Kruskal's algorithm will work on
///     graphs that are unconnected. This algorithm will always have a
///     running time of O(E log V) where E is the number of edges and V is
///     the number of vertices/nodes.
///     More information: https://en.wikipedia.org/wiki/Kruskal%27s_algorithm .
///     Pseudocode and analysis: https://www.personal.kent.edu/~rmuhamma/Algorithms/MyAlgorithms/GraphAlgor/primAlgor.htm .
/// </remarks>
public static class Kruskal
{
    /// <summary>
    ///     Determine the minimum spanning tree/forest of the given graph.
    /// </summary>
    /// <param name="adjacencyMatrix">Adjacency matrix representing the graph.</param>
    /// <returns>Adjacency matrix of the minimum spanning tree/forest.</returns>
    public static float[,] Solve(float[,] adjacencyMatrix)
    {
        ValidateGraph(adjacencyMatrix);

        var numNodes = adjacencyMatrix.GetLength(0);
        var set = new DisjointSet<int>();
        var nodes = new Node<int>[numNodes];
        var edgeWeightList = new List<float>();
        var nodeConnectList = new List<(int, int)>();

        // Add nodes to disjoint set
        for (var i = 0; i < numNodes; i++)
        {
            nodes[i] = set.MakeSet(i);
        }

        // Create lists with edge weights and associated connectivity
        for (var i = 0; i < numNodes - 1; i++)
        {
            for (var j = i + 1; j < numNodes; j++)
            {
                if (float.IsFinite(adjacencyMatrix[i, j]))
                {
                    edgeWeightList.Add(adjacencyMatrix[i, j]);
                    nodeConnectList.Add((i, j));
                }
            }
        }

        var edges = Solve(set, nodes, edgeWeightList.ToArray(), nodeConnectList.ToArray());

        // Initialize minimum spanning tree
        var mst = new float[numNodes, numNodes];
        for (var i = 0; i < numNodes; i++)
        {
            mst[i, i] = float.PositiveInfinity;

            for (var j = i + 1; j < numNodes; j++)
            {
                mst[i, j] = float.PositiveInfinity;
                mst[j, i] = float.PositiveInfinity;
            }
        }

        foreach (var (node1, node2) in edges)
        {
            mst[node1, node2] = adjacencyMatrix[node1, node2];
            mst[node2, node1] = adjacencyMatrix[node1, node2];
        }

        return mst;
    }

    /// <summary>
    ///     Determine the minimum spanning tree/forest of the given graph.
    /// </summary>
    /// <param name="adjacencyList">Adjacency list representing the graph.</param>
    /// <returns>Adjacency list of the minimum spanning tree/forest.</returns>
    public static Dictionary<int, float>[] Solve(Dictionary<int, float>[] adjacencyList)
    {
        ValidateGraph(adjacencyList);

        var numNodes = adjacencyList.Length;
        var set = new DisjointSet<int>();
        var nodes = new Node<int>[numNodes];
        var edgeWeightList = new List<float>();
        var nodeConnectList = new List<(int, int)>();

        // Add nodes to disjoint set and create list of edge weights and associated connectivity
        for (var i = 0; i < numNodes; i++)
        {
            nodes[i] = set.MakeSet(i);

            foreach(var (node, weight) in adjacencyList[i])
            {
                edgeWeightList.Add(weight);
                nodeConnectList.Add((i, node));
            }
        }

        var edges = Solve(set, nodes, edgeWeightList.ToArray(), nodeConnectList.ToArray());

        // Create minimum spanning tree
        var mst = new Dictionary<int, float>[numNodes];
        for (var i = 0; i < numNodes; i++)
        {
            mst[i] = new Dictionary<int, float>();
        }

        foreach (var (node1, node2) in edges)
        {
            mst[node1].Add(node2, adjacencyList[node1][node2]);
            mst[node2].Add(node1, adjacencyList[node1][node2]);
        }

        return mst;
    }

    /// <summary>
    ///     Ensure that the given graph is undirected.
    /// </summary>
    /// <param name="adj">Adjacency matrix of graph to check.</param>
    private static void ValidateGraph(float[,] adj)
    {
        if (adj.GetLength(0) != adj.GetLength(1))
        {
            throw new ArgumentException("Matrix must be square!");
        }

        for (var i = 0; i < adj.GetLength(0) - 1; i++)
        {
            for (var j = i + 1; j < adj.GetLength(1); j++)
            {
                if (Math.Abs(adj[i, j] - adj[j, i]) > 1e-6)
                {
                    throw new ArgumentException("Matrix must be symmetric!");
                }
            }
        }
    }

    /// <summary>
    ///     Ensure that the given graph is undirected.
    /// </summary>
    /// <param name="adj">Adjacency list of graph to check.</param>
    private static void ValidateGraph(Dictionary<int, float>[] adj)
    {
        for (var i = 0; i < adj.Length; i++)
        {
            foreach (var edge in adj[i])
            {
                if (!adj[edge.Key].ContainsKey(i) || Math.Abs(edge.Value - adj[edge.Key][i]) > 1e-6)
                {
                    throw new ArgumentException("Graph must be undirected!");
                }
            }
        }
    }

    /// <summary>
    ///     Determine the minimum spanning tree/forest.
    /// </summary>
    /// <param name="set">Disjoint set needed for set operations.</param>
    /// <param name="nodes">List of nodes in disjoint set associated with each node.</param>
    /// <param name="edgeWeights">Weights of each edge.</param>
    /// <param name="connections">Nodes associated with each item in the <paramref name="edgeWeights"/> parameter.</param>
    /// <returns>Array of edges in the minimum spanning tree/forest.</returns>
    private static (int, int)[] Solve(DisjointSet<int> set, Node<int>[] nodes, float[] edgeWeights, (int, int)[] connections)
    {
        var edges = new List<(int, int)>();

        Array.Sort(edgeWeights, connections);

        foreach (var (node1, node2) in connections)
        {
            if (set.FindSet(nodes[node1]) != set.FindSet(nodes[node2]))
            {
                set.UnionSet(nodes[node1], nodes[node2]);
                edges.Add((node1, node2));
            }
        }

        return edges.ToArray();
    }
}
