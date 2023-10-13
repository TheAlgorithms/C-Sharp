using System;
using System.Collections.Generic;

namespace Algorithms.Strings.Similarity;

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
    /// The Jaccard similarity coefficient is defined as the size of the intersection divided by the size of the union
    /// of two sets.
    /// <para>
    /// This method uses a HashSet to represent the sets of characters in the input strings.
    /// </para>
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
