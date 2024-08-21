using System;

namespace Algorithms.Strings.Similarity
{
    /// <summary>
    /// Provides methods to calculate the Optimal String Alignment distance between two strings.
    ///
    /// The Optimal String Alignment distance, also known as the restricted Damerau-Levenshtein distance,
    /// is a string metric used to measure the difference between two sequences. It is similar to the
    /// Levenshtein distance, but it also considers transpositions (swapping of two adjacent characters)
    /// as a single operation. This metric is particularly useful when adjacent characters are commonly
    /// transposed, such as in typographical errors.
    ///
    /// The OSA distance between two strings is defined as the minimum number of operations required to
    /// transform one string into the other, where the operations include:
    ///
    /// 1. Insertion: Adding a single character.
    /// 2. Deletion: Removing a single character.
    /// 3. Substitution: Replacing one character with another.
    /// 4. Transposition: Swapping two adjacent characters (this is what distinguishes OSA from the
    ///    traditional Levenshtein distance).
    ///
    /// The OSA distance algorithm ensures that no operation is applied more than once to the same
    /// character in the same position. This is the main difference between the OSA and the more general
    /// Damerau-Levenshtein distance, which does not have this restriction.
    ///
    /// <example>
    /// Example Usage:
    /// <code>
    /// int distance = OptimalStringAlignmentDistance("example", "exmaple");
    /// Console.WriteLine(distance); // Output: 1
    /// </code>
    /// In this example, the strings "example" and "exmaple" differ by one transposition of adjacent characters ('a' and 'm'),
    /// so the OSA distance is 1.
    ///
    /// <code>
    /// int distance = OptimalStringAlignmentDistance("kitten", "sitting");
    /// Console.WriteLine(distance); // Output: 3
    /// </code>
    /// Here, the strings "kitten" and "sitting" have three differences (substitutions 'k' to 's', 'e' to 'i', and insertion of 'g'),
    /// resulting in an OSA distance of 3.
    /// </example>
    /// </summary>
    /// <remarks>
    /// This algorithm has a time complexity of O(n * m), where n and m are the lengths of the two input strings.
    /// It is efficient for moderate-sized strings but may become computationally expensive for very long strings.
    /// </remarks>
    public static class OptimalStringAlignment
    {
        /// <summary>
        /// Calculates the Optimal String Alignment distance between two strings.
        /// </summary>
        /// <param name="firstString">The first string.</param>
        /// <param name="secondString">The second string.</param>
        /// <returns>The Optimal String Alignment distance between the two strings.</returns>
        /// <exception cref="ArgumentNullException">Thrown when either of the input strings is null.</exception>
        public static double Calculate(string firstString, string secondString)
        {
            ArgumentNullException.ThrowIfNull(nameof(firstString));
            ArgumentNullException.ThrowIfNull(nameof(secondString));

            if (firstString == secondString)
            {
                return 0.0;
            }

            if (firstString.Length == 0)
            {
                return secondString.Length;
            }

            if (secondString.Length == 0)
            {
                return firstString.Length;
            }

            var distanceMatrix = GenerateDistanceMatrix(firstString.Length, secondString.Length);
            distanceMatrix = CalculateDistance(firstString, secondString, distanceMatrix);

            return distanceMatrix[firstString.Length, secondString.Length];
        }

        /// <summary>
        /// Generates the initial distance matrix for the given lengths of the two strings.
        /// </summary>
        /// <param name="firstLength">The length of the first string.</param>
        /// <param name="secondLength">The length of the second string.</param>
        /// <returns>The initialized distance matrix.</returns>
        private static int[,] GenerateDistanceMatrix(int firstLength, int secondLength)
        {
            var distanceMatrix = new int[firstLength + 2, secondLength + 2];

            for (var i = 0; i <= firstLength; i++)
            {
                distanceMatrix[i, 0] = i;
            }

            for (var j = 0; j <= secondLength; j++)
            {
                distanceMatrix[0, j] = j;
            }

            return distanceMatrix;
        }

        /// <summary>
        /// Calculates the distance matrix for the given strings using the Optimal String Alignment algorithm.
        /// </summary>
        /// <param name="firstString">The first string.</param>
        /// <param name="secondString">The second string.</param>
        /// <param name="distanceMatrix">The initial distance matrix.</param>
        /// <returns>The calculated distance matrix.</returns>
        private static int[,] CalculateDistance(string firstString, string secondString, int[,] distanceMatrix)
        {
            for (var i = 1; i <= firstString.Length; i++)
            {
                for (var j = 1; j <= secondString.Length; j++)
                {
                    var cost = 1;

                    if (firstString[i - 1] == secondString[j - 1])
                    {
                        cost = 0;
                    }

                    distanceMatrix[i, j] = Minimum(
                        distanceMatrix[i - 1, j - 1] + cost, // substitution
                        distanceMatrix[i, j - 1] + 1, // insertion
                        distanceMatrix[i - 1, j] + 1); // deletion

                    if (i > 1 && j > 1
                        && firstString[i - 1] == secondString[j - 2]
                        && firstString[i - 2] == secondString[j - 1])
                    {
                        distanceMatrix[i, j] = Math.Min(
                            distanceMatrix[i, j],
                            distanceMatrix[i - 2, j - 2] + cost); // transposition
                    }
                }
            }

            return distanceMatrix;
        }

        /// <summary>
        /// Returns the minimum of three integers.
        /// </summary>
        /// <param name="a">The first integer.</param>
        /// <param name="b">The second integer.</param>
        /// <param name="c">The third integer.</param>
        /// <returns>The minimum of the three integers.</returns>
        private static int Minimum(int a, int b, int c)
        {
            return Math.Min(a, Math.Min(b, c));
        }
    }
}
