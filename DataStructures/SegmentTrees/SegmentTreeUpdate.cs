namespace DataStructures.SegmentTrees;

/// <summary>
///     This is an extension of a segment tree, which allows the update of a single element.
/// </summary>
public class SegmentTreeUpdate : SegmentTree
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SegmentTreeUpdate" /> class.
    ///     Runtime complexity: O(n) where n equals the array-length.
    /// </summary>
    /// <param name="arr">Array on which the queries should be made.</param>
    public SegmentTreeUpdate(int[] arr)
        : base(arr)
    {
    }

    /// <summary>
    ///     Updates a single element of the input array.
    ///     Changes the leaf first and updates its parents afterwards.
    ///     Runtime complexity: O(logN) where N equals the initial array-length.
    /// </summary>
    /// <param name="node">Index of the node that should be updated.</param>
    /// <param name="value">New Value of the element.</param>
    public void Update(int node, int value)
    {
        Tree[node + Tree.Length / 2] = value;
        Propagate(Parent(node + Tree.Length / 2));
    }

    /// <summary>
    ///     Recalculates the value of node by its children.
    ///     Calls its parent to do the same.
    /// </summary>
    /// <param name="node">Index of current node.</param>
    private void Propagate(int node)
    {
        if (node == 0)
        {
            // passed root
            return;
        }

        Tree[node] = Tree[Left(node)] + Tree[Right(node)];
        Propagate(Parent(node));
    }
}
