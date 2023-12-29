using System;

namespace Algorithms.Graph.MinimumSpanningTree;

/// <summary>
///     Class that uses Prim's (Jarnik's algorithm) to determine the minimum
///     spanning tree (MST) of a given graph. Prim's algorithm is a greedy
///     algorithm that can determine the MST of a weighted undirected graph
///     in O(V^2) time where V is the number of nodes/vertices when using an
///     adjacency matrix representation.
///     More information: https://en.wikipedia.org/wiki/Prim%27s_algorithm
///     Pseudocode and runtime analysis: https://www.personal.kent.edu/~rmuhamma/Algorithms/MyAlgorithms/GraphAlgor/primAlgor.htm .
/// </summary>
public static class PrimMatrix
{
    /// <summary>
    ///     Determine the minimum spanning tree for a given weighted undirected graph.
    /// </summary>
    /// <param name="adjacencyMatrix">Adjacency matrix for graph to find MST of.</param>
    /// <param name="start">Node to start search from.</param>
    /// <returns>Adjacency matrix of the found MST.</returns>
    public static float[,] Solve(float[,] adjacencyMatrix, int start)
    {
        ValidateMatrix(adjacencyMatrix);

        var numNodes = adjacencyMatrix.GetLength(0);

        // Create array to represent minimum spanning tree
        var mst = new float[numNodes, numNodes];

        // Create array to keep track of which nodes are in the MST already
        var added = new bool[numNodes];

        // Create array to keep track of smallest edge weight for node
        var key = new float[numNodes];

        // Create array to store parent of node
        var parent = new int[numNodes];

        for (var i = 0; i < numNodes; i++)
        {
            mst[i, i] = float.PositiveInfinity;
            key[i] = float.PositiveInfinity;

            for (var j = i + 1; j < numNodes; j++)
            {
                mst[i, j] = float.PositiveInfinity;
                mst[j, i] = float.PositiveInfinity;
            }
        }

        // Ensures that the starting node is added first
        key[start] = 0;

        // Keep looping until all nodes are in tree
        for (var i = 0; i < numNodes - 1; i++)
        {
            GetNextNode(adjacencyMatrix, key, added, parent);
        }

        // Build adjacency matrix for tree
        for (var i = 0; i < numNodes; i++)
        {
            if (i == start)
            {
                continue;
            }

            mst[i, parent[i]] = adjacencyMatrix[i, parent[i]];
            mst[parent[i], i] = adjacencyMatrix[i, parent[i]];
        }

        return mst;
    }

    /// <summary>
    ///     Ensure that the given adjacency matrix represents a weighted undirected graph.
    /// </summary>
    /// <param name="adjacencyMatrix">Adjacency matric to check.</param>
    private static void ValidateMatrix(float[,] adjacencyMatrix)
    {
        // Matrix should be square
        if (adjacencyMatrix.GetLength(0) != adjacencyMatrix.GetLength(1))
        {
            throw new ArgumentException("Adjacency matrix must be square!");
        }

        // Graph needs to be undirected and connected
        for (var i = 0; i < adjacencyMatrix.GetLength(0); i++)
        {
            var connection = false;
            for (var j = 0; j < adjacencyMatrix.GetLength(0); j++)
            {
                if (Math.Abs(adjacencyMatrix[i, j] - adjacencyMatrix[j, i]) > 1e-6)
                {
                    throw new ArgumentException("Adjacency matrix must be symmetric!");
                }

                if (!connection && float.IsFinite(adjacencyMatrix[i, j]))
                {
                    connection = true;
                }
            }

            if (!connection)
            {
                throw new ArgumentException("Graph must be connected!");
            }
        }
    }

    /// <summary>
    ///     Determine which node should be added next to the MST.
    /// </summary>
    /// <param name="adjacencyMatrix">Adjacency matrix of graph.</param>
    /// <param name="key">Currently known minimum edge weight connected to each node.</param>
    /// <param name="added">Whether or not a node has been added to the MST.</param>
    /// <param name="parent">The node that added the node to the MST. Used for building MST adjacency matrix.</param>
    private static void GetNextNode(float[,] adjacencyMatrix, float[] key, bool[] added, int[] parent)
    {
        var numNodes = adjacencyMatrix.GetLength(0);
        var minWeight = float.PositiveInfinity;

        var node = -1;

        // Find node with smallest node with known edge weight not in tree. Will always start with starting node
        for (var i = 0; i < numNodes; i++)
        {
            if (!added[i] && key[i] < minWeight)
            {
                minWeight = key[i];
                node = i;
            }
        }

        // Add node to mst
        added[node] = true;

        // Update smallest found edge weights and parent for adjacent nodes
        for (var i = 0; i < numNodes; i++)
        {
            if (!added[i] && adjacencyMatrix[node, i] < key[i])
            {
                key[i] = adjacencyMatrix[node, i];
                parent[i] = node;
            }
        }
    }
}
