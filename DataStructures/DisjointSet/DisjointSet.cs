using System.Collections;

namespace DataStructures.DisjointSet;

/// <summary>
/// Implementation of Disjoint Set with Union By Rank and Path Compression heuristics.
/// </summary>
/// <typeparam name="T"> generic type for implementation.</typeparam>
public class DisjointSet<T>
{
    /// <summary>
    /// make a new set and return its representative.
    /// </summary>
    /// <param name="x">element to add in to the DS.</param>
    /// <returns>representative of x.</returns>
    public Node<T> MakeSet(T x) => new(x);

    /// <summary>
    /// find the representative of a certain node.
    /// </summary>
    /// <param name="node">node to find representative.</param>
    /// <returns>representative of x.</returns>
    public Node<T> FindSet(Node<T> node)
    {
        if (node != node.Parent)
        {
            node.Parent = FindSet(node.Parent);
        }

        return node.Parent;
    }

    /// <summary>
    /// merge two sets.
    /// </summary>
    /// <param name="x">first set member.</param>
    /// <param name="y">second set member.</param>
    public void UnionSet(Node<T> x, Node<T> y)
    {
        Node<T> nx = FindSet(x);
        Node<T> ny = FindSet(y);
        if (nx == ny)
        {
            return;
        }

        if (nx.Rank > ny.Rank)
        {
            ny.Parent = nx;
        }
        else if (ny.Rank > nx.Rank)
        {
            nx.Parent = ny;
        }
        else
        {
            nx.Parent = ny;
            ny.Rank++;
        }
    }
}
