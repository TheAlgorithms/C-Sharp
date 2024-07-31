using System;
using System.Collections.Generic;

namespace Algorithms.Strings.Similarity;

public static class CosineSimilarity
{
    /// <summary>
    /// Calculates the Cosine Similarity between two strings.
    /// Cosine Similarity is a measure of similarity between two non-zero vectors of an inner product space.
    /// It measures the cosine of the angle between the two vectors.
    /// </summary>
    /// <param name="left">The first string.</param>
    /// <param name="right">The second string.</param>
    /// <returns>
    /// A double value between 0 and 1 that represents the similarity
    /// of the two strings.
    /// </returns>
    public static double Calculate(string left, string right)
    {
        // Step 1: Get the vectors for the two strings
        // Each vector represents the frequency of each character in the string.
        var vectors = GetVectors(left.ToLowerInvariant(), right.ToLowerInvariant());
        var leftVector = vectors.leftVector;
        var rightVector = vectors.rightVector;

        // Step 2: Calculate the intersection of the two vectors
        // The intersection is the set of characters that appear in both strings.
        var intersection = GetIntersection(leftVector, rightVector);

        // Step 3: Calculate the dot product of the two vectors
        // The dot product is the sum of the products of the corresponding values of the characters in the intersection.
        var dotProduct = DotProduct(leftVector, rightVector, intersection);

        // Step 4: Calculate the square magnitude of each vector
        // The magnitude is the square root of the sum of the squares of the values in the vector.
        var mLeft = 0.0;
        foreach (var value in leftVector.Values)
        {
            mLeft += value * value;
        }

        var mRight = 0.0;
        foreach (var value in rightVector.Values)
        {
            mRight += value * value;
        }

        // Step 5: Check if either vector is zero
        // If either vector is zero (i.e., all characters are unique), the Cosine Similarity is 0.
        if (mLeft <= 0 || mRight <= 0)
        {
            return 0.0;
        }

        // Step 6: Calculate and return the Cosine Similarity
        // The Cosine Similarity is the dot product divided by the product of the magnitudes.
        return dotProduct / (Math.Sqrt(mLeft) * Math.Sqrt(mRight));
    }

    /// <summary>
    /// Calculates the vectors for the given strings.
    /// </summary>
    /// <param name="left">The first string.</param>
    /// <param name="right">The second string.</param>
    /// <returns>A tuple containing the vectors for the two strings.</returns>
    private static (Dictionary<char, int> leftVector, Dictionary<char, int> rightVector) GetVectors(string left, string right)
    {
        var leftVector = new Dictionary<char, int>();
        var rightVector = new Dictionary<char, int>();

        // Calculate the frequency of each character in the left string
        foreach (var character in left)
        {
            leftVector.TryGetValue(character, out var frequency);
            leftVector[character] = ++frequency;
        }

        // Calculate the frequency of each character in the right string
        foreach (var character in right)
        {
            rightVector.TryGetValue(character, out var frequency);
            rightVector[character] = ++frequency;
        }

        return (leftVector, rightVector);
    }

    /// <summary>
    /// Calculates the dot product between two vectors represented as dictionaries of character frequencies.
    /// The dot product is the sum of the products of the corresponding values of the characters in the intersection of the two vectors.
    /// </summary>
    /// <param name="leftVector">The vector of the left string.</param>
    /// <param name="rightVector">The vector of the right string.</param>
    /// <param name="intersection">The intersection of the two vectors, represented as a set of characters.</param>
    /// <returns>The dot product of the two vectors.</returns>
    private static double DotProduct(Dictionary<char, int> leftVector, Dictionary<char, int> rightVector, HashSet<char> intersection)
    {
        // Initialize the dot product to 0
        double dotProduct = 0;

        // Iterate over each character in the intersection of the two vectors
        foreach (var character in intersection)
        {
            // Calculate the product of the corresponding values of the characters in the left and right vectors
            dotProduct += leftVector[character] * rightVector[character];
        }

        // Return the dot product
        return dotProduct;
    }

    /// <summary>
    /// Calculates the intersection of two vectors, represented as dictionaries of character frequencies.
    /// </summary>
    /// <param name="leftVector">The vector of the left string.</param>
    /// <param name="rightVector">The vector of the right string.</param>
    /// <returns>A HashSet containing the characters that appear in both vectors.</returns>
    private static HashSet<char> GetIntersection(Dictionary<char, int> leftVector, Dictionary<char, int> rightVector)
    {
        // Initialize a HashSet to store the intersection of the two vectors.
        var intersection = new HashSet<char>();

        // Iterate over each key-value pair in the left vector.
        foreach (var kvp in leftVector)
        {
            // If the right vector contains the same key, add it to the intersection.
            if (rightVector.ContainsKey(kvp.Key))
            {
                intersection.Add(kvp.Key);
            }
        }

        return intersection;
    }
}
