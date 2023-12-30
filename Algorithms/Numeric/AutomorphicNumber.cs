using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algorithms.Numeric;

/// <summary>
/// Calculates Automorphic numbers. A number is said to be an Automorphic number
/// if the square of the number will contain the number itself at the end.
/// </summary>
public static class AutomorphicNumber
{
    /// <summary>
    /// Generates a list of automorphic numbers that are between <paramref name="lowerBound"/> and <paramref name="upperBound"/>
    /// inclusive.
    /// </summary>
    /// <param name="lowerBound">The lower bound of the list.</param>
    /// <param name="upperBound">The upper bound of the list.</param>
    /// <returns>A list that contains all of the automorphic numbers between <paramref name="lowerBound"/> and <paramref name="upperBound"/> inclusive.</returns>
    /// <exception cref="ArgumentException">If the <paramref name="lowerBound"/>
    /// or <paramref name="upperBound"/> is not greater than zero
    /// or <paramref name="upperBound"/>is lower than the <paramref name="lowerBound"/>.</exception>
    public static IEnumerable<int> GetAutomorphicNumbers(int lowerBound, int upperBound)
    {
        if (lowerBound < 1)
        {
            throw new ArgumentException($"Lower bound must be greater than 0.");
        }

        if (upperBound < 1)
        {
            throw new ArgumentException($"Upper bound must be greater than 0.");
        }

        if (lowerBound > upperBound)
        {
            throw new ArgumentException($"The lower bound must be less than or equal to the upper bound.");
        }

        return Enumerable.Range(lowerBound, upperBound).Where(IsAutomorphic);
    }

    /// <summary>
    /// Checks if a given natural number is automorphic or not.
    /// </summary>
    /// <param name="number">The number to check.</param>
    /// <returns>True if the number is automorphic, false otherwise.</returns>
    /// <exception cref="ArgumentException">If the number is non-positive.</exception>
    public static bool IsAutomorphic(int number)
    {
        if (number < 1)
        {
            throw new ArgumentException($"An automorphic number must always be positive.");
        }

        BigInteger square = BigInteger.Pow(number, 2);

        // Extract the last digits of both numbers
        while (number > 0)
        {
            if (number % 10 != square % 10)
            {
                return false;
            }

            number /= 10;
            square /= 10;
        }

        return true;
    }
}
