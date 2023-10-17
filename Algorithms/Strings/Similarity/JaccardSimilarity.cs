using System;
using System.Collections.Generic;

namespace Algorithms.Strings.Similarity;

/// <summary>
///  <para>
/// Jaccard similarity is a statistic that measures how similar two sets of data are. It is calculated by dividing
/// the number of common elements in both sets by the number of elements in either set. More formally, it is the
/// quotient of the division of the size of the size of the intersection divided by the size of the union of two sets.
/// </para>
/// <para>
/// The result is a value between 0 and 1, where 0 means no similarity and 1 means perfect similarity.
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
/// </summary>
public class JaccardSimilarity
{
    /// <summary>
    /// Calculates the Jaccard similarity coefficient between two strings.
    /// </summary>
    /// <param name="left">The first string to compare.</param>
    /// <param name="right">The second string to compare.</param>
    /// <returns>A double value between 0 and 1 that represents the similarity of the two strings.</returns>
    /// <exception cref="ArgumentNullException">Thrown when either the input is null.</exception>
    /// <remarks>
    /// This method uses a HashSet to represent the sets of characters in the input strings.
    /// </remarks>
    public double Calculate(string left, string right)
    {
        // Validate the input strings before proceeding.
        ValidateInput(left, right);

        // Get the lengths of the input strings.
        var leftLength = left.Length;
        var rightLength = right.Length;

        // If both strings are empty, return 1.0 as the similarity coefficient.
        if (leftLength == 0 && rightLength == 0)
        {
            return 1.0d;
        }

        // If either string is empty, return 0.0 as the similarity coefficient.
        if (leftLength == 0 || rightLength == 0)
        {
            return 0.0d;
        }

        // Get the unique characters in each string.
        var leftSet = new HashSet<char>(left);
        var rightSet = new HashSet<char>(right);

        // Get the union of the two strings.
        var unionSet = new HashSet<char>(leftSet);
        foreach (var c in rightSet)
        {
            unionSet.Add(c);
        }

        // Calculate the intersection size of the two strings.
        var intersectionSize = leftSet.Count + rightSet.Count - unionSet.Count;

        // Return the Jaccard similarity coefficient as the ratio of intersection to union.
        return 1.0d * intersectionSize / unionSet.Count;
    }

    /// <summary>
    /// Validates the input strings and throws an exception if either is null.
    /// </summary>
    /// <param name="left">The first string to validate.</param>
    /// <param name="right">The second string to validate.</param>
    private void ValidateInput(string left, string right)
    {
        if (left == null || right == null)
        {
            var paramName = left == null ? nameof(left) : nameof(right);
            throw new ArgumentNullException(paramName, "Input cannot be null");
        }
    }
}
