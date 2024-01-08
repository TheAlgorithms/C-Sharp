namespace DataStructures.Fenwick;

/// <summary>
/// Represent classical realization of Fenwiсk tree or Binary Indexed tree.
///
/// BITree[0..n] --> Array that represents Binary Indexed Tree.
/// arr[0..n-1] --> Input array for which prefix sum is evaluated.
/// </summary>
public class BinaryIndexedTree
{
    private readonly int[] fenwickTree;

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryIndexedTree"/> class.
    /// Create Binary indexed tree from the given array.
    /// </summary>
    /// <param name="array">Initial array.</param>
    public BinaryIndexedTree(int[] array)
    {
        fenwickTree = new int[array.Length + 1];

        for (var i = 0; i < array.Length; i++)
        {
            UpdateTree(i, array[i]);
        }
    }

    /// <summary>
    /// This method assumes that the array is preprocessed and
    /// partial sums of array elements are stored in BITree[].
    /// </summary>
    /// <param name="index">The position to sum from.</param>
    /// <returns>Returns sum of arr[0..index].</returns>
    public int GetSum(int index)
    {
        var sum = 0;
        var startFrom = index + 1;

        while (startFrom > 0)
        {
            sum += fenwickTree[startFrom];
            startFrom -= startFrom & (-startFrom);
        }

        return sum;
    }

    /// <summary>
    /// Updates a node in Binary Index Tree at given index.
    /// The given value 'val' is added to BITree[i] and all of its ancestors in tree.
    /// </summary>
    /// <param name="index">Given index.</param>
    /// <param name="val">Value to be update on.</param>
    public void UpdateTree(int index, int val)
    {
        var startFrom = index + 1;

        while (startFrom <= fenwickTree.Length)
        {
            fenwickTree[startFrom] += val;
            startFrom += startFrom & (-startFrom);
        }
    }
}
