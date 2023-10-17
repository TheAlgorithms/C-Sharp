namespace Algorithms.Strings.Similarity;

/// <summary>
///  <para>
/// Jaccard distance is a measure of two sets of data are. It is calculated by subtracting the Jaccard similarity
/// coefficient from 1, or, equivalently by dividing the difference of the sizes of the union and intersection of two sets
/// by the size of the union.
/// </para>
/// <para>
/// For example, suppose we have two sets of words:
/// <list type="bullet">
/// <item>
/// A = {apple, banana, cherry, date}
/// </item>
/// <item>
/// B = {banana, cherry, elderberry, fig}
/// </item>
/// </list>
/// </para>
/// <para>
/// The number of common elements in both sets is 2 (banana and cherry). The number of elements in either set is 6
/// (apple, banana, cherry, date, elderberry, fig).
/// </para>
/// <para>
/// The Jaccard similarity coefficient is 2 / 6 = 0.333333 or 33.333% similarity.
/// </para>
/// <para>
/// The Jaccard distance is 1 - 0.33333 = 0.66667. This means that the two sets are about 67% different.
/// </para>
/// <para>
/// Jaccard distance is commonly used to calculate a matrix of clustering and multidimensional scaling of sample tests.
/// </para>
/// </summary>
public class JaccardDistance
{
    private readonly JaccardSimilarity jaccardSimilarity = new();

    /// <summary>
    /// Calculate the Jaccard distance between to strings.
    /// </summary>
    /// <param name="left">The first string.</param>
    /// <param name="right">The second string.</param>
    /// <returns>The Jaccard distance.</returns>
    public double Calculate(string left, string right)
    {
        return 1.0 - jaccardSimilarity.Calculate(left, right);
    }
}
