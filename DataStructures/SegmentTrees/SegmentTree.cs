using System;

namespace DataStructures.SegmentTrees;

/// <summary>
///     Goal:   Data structure with which you can quickly perform queries on an array (i.e. sum of subarray)
///     and at the same time efficiently update an entry
///     or apply a distributive operation to a subarray.
///     Idea:   Preprocessing special queries
///     Hint:   The query operation HAS to be associative (in this example addition).
/// </summary>
public class SegmentTree
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SegmentTree" /> class.
    ///     Runtime complexity: O(n) where n equals the array-length.
    /// </summary>
    /// <param name="arr">Array on which the queries should be made.</param>
    public SegmentTree(int[] arr)
    {
        // Calculates next power of two
        var pow = (int)Math.Pow(2, Math.Ceiling(Math.Log(arr.Length, 2)));
        Tree = new int[2 * pow];

        // Transfers the input array into the last half of the segment tree array
        Array.Copy(arr, 0, Tree, pow, arr.Length);

        // Calculates the first half
        for (var i = pow - 1; i > 0; --i)
        {
            Tree[i] = Tree[Left(i)] + Tree[Right(i)];
        }
    }

    /// <summary>Gets the segment tree array.</summary>
    public int[] Tree { get; }

    /// <summary>
    ///     Starts a query.
    ///     Runtime complexity: O(logN) where n equals the array-length.
    /// </summary>
    /// <param name="l">Left border of the query.</param>
    /// <param name="r">Right border of the query.</param>
    /// <returns>Sum of the subarray between <c>l</c> and <c>r</c> (including <c>l</c> and <c>r</c>).</returns>
    // Editing of query start at node with 1.
    // Node with index 1 includes the whole input subarray.
    public int Query(int l, int r) =>
        Query(++l, ++r, 1, Tree.Length / 2, 1);

    /// <summary>
    ///     Calculates the right child of a node.
    /// </summary>
    /// <param name="node">Current node.</param>
    /// <returns>Index of the right child.</returns>
    protected int Right(int node) => 2 * node + 1;

    /// <summary>
    ///     Calculates the left child of a node.
    /// </summary>
    /// <param name="node">Current node.</param>
    /// <returns>Index of the left child.</returns>
    protected int Left(int node) => 2 * node;

    /// <summary>
    ///     Calculates the parent of a node.
    /// </summary>
    /// <param name="node">Current node.</param>
    /// <returns>Index of the parent node.</returns>
    protected int Parent(int node) => node / 2;

    /// <summary>
    ///     Edits a query.
    /// </summary>
    /// <param name="l">Left border of the query.</param>
    /// <param name="r">Right border of the query.</param>
    /// <param name="a">Left end of the subarray enclosed by <c>i</c>.</param>
    /// <param name="b">Right end of the subarray enclosed by <c>i</c>.</param>
    /// <param name="i">Current node.</param>
    /// <returns>Sum of a subarray between <c>l</c> and <c>r</c> (including <c>l</c> and <c>r</c>).</returns>
    protected virtual int Query(int l, int r, int a, int b, int i)
    {
        // If a and b are in the (by l and r) specified subarray
        if (l <= a && b <= r)
        {
            return Tree[i];
        }

        // If a or b are out of the by l and r specified subarray
        if (r < a || b < l)
        {
            // Returns the neutral value of the operation
            // (in this case 0, because x + 0 = x)
            return 0;
        }

        // Calculates index m of the node that cuts the current subarray in half
        var m = (a + b) / 2;

        // Start query of new two subarrays a:m and m+1:b
        // The right and left child cover this intervals
        return Query(l, r, a, m, Left(i)) + Query(l, r, m + 1, b, Right(i));
    }
}
