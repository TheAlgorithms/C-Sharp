using System;

namespace Algorithms.Strings.Similarity;

public static class DamerauLevenshteinDistance
{
    /// <summary>
    /// Calculates the Damerau-Levenshtein distance between two strings.
    /// The Damerau-Levenshtein distance is a string metric for measuring the difference between two sequences.
    /// It is calculated as the minimum number of operations needed to transform one sequence into the other.
    /// The possible operations are insertion, deletion, substitution, and transposition.
    /// </summary>
    /// <param name="left">The first string.</param>
    /// <param name="right">The second string.</param>
    /// <returns>The Damerau-Levenshtein distance between the two strings.</returns>
    public static int Calculate(string left, string right)
    {
        // Get the lengths of the input strings.
        var leftSize = left.Length;
        var rightSize = right.Length;

        // Initialize a matrix of distances between the two strings.
        var distances = InitializeDistanceArray(leftSize, rightSize);

        // Iterate over each character in the left string.
        for (var i = 1; i < leftSize + 1; i++)
        {
            // Iterate over each character in the right string.
            for (var j = 1; j < rightSize + 1; j++)
            {
                // Calculate the cost of the current operation.
                // If the characters at the current positions are the same, the cost is 0.
                // Otherwise, the cost is 1.
                var cost = left[i - 1] == right[j - 1] ? 0 : 1;

                // Calculate the minimum distance by considering three possible operations:
                // deletion, insertion, and substitution.
                distances[i, j] = Math.Min(
                    Math.Min( // deletion
                        distances[i - 1, j] + 1, // delete the character from the left string
                        distances[i, j - 1] + 1), // insert the character into the right string
                    distances[i - 1, j - 1] + cost); // substitute the character in the left string with the character in the right string

                // If the current character in the left string is the same as the character
                // two positions to the left in the right string and the current character
                // in the right string is the same as the character one position to the right
                // in the left string, then we can also consider a transposition operation.
                if (i > 1 && j > 1 && left[i - 1] == right[j - 2] && left[i - 2] == right[j - 1])
                {
                    distances[i, j] = Math.Min(
                        distances[i, j], // current minimum distance
                        distances[i - 2, j - 2] + cost); // transpose the last two characters
                }
            }
        }

        // Return the distance between the two strings.
        return distances[leftSize, rightSize];
    }

    /// <summary>
    /// Initializes a matrix of distances between two string representations.
    ///
    /// This method creates a matrix of distances where the dimensions are one larger
    /// than the input strings. The first row of the matrix represents the distances
    /// when the left string is empty, and the first column represents the distances
    /// when the right string is empty. The values in the first row and first column
    /// are the lengths of the corresponding strings.
    ///
    /// The matrix is used by the Damerau-Levenshtein algorithm to calculate the
    /// minimum number of single-character edits (insertions, deletions, or substitutions)
    /// required to change one word into the other.
    /// The matrix is initialized with dimensions one larger than the input strings.
    /// The first row of the matrix represents the distances when the left string is empty.
    /// The first column of the matrix represents the distances when the right string is empty.
    /// The values in the first row and first column are the lengths of the corresponding strings.
    /// Initializes a matrix of distances between two strings representations.
    /// </summary>
    /// <param name="leftSize">The size of the left string.</param>
    /// <param name="rightSize">The size of the right string.</param>
    /// <returns>A matrix of distances.</returns>
    private static int[,] InitializeDistanceArray(int leftSize, int rightSize)
    {
        // Initialize a matrix of distances with dimensions one larger than the input strings.
        var matrix = new int[leftSize + 1, rightSize + 1];

        // Set the values in the first row to the lengths of the left string.
        // This represents the distance when the left string is empty.
        for (var i = 1; i < leftSize + 1; i++)
        {
            matrix[i, 0] = i;
        }

        // Set the values in the first column to the lengths of the right string.
        // This represents the distance when the right string is empty.
        for (var i = 1; i < rightSize + 1; i++)
        {
            matrix[0, i] = i;
        }

        // Return the initialized matrix of distances.
        return matrix;
    }
}
