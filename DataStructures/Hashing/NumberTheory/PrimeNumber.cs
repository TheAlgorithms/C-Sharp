using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Hashing.NumberTheory;

/// <summary>
/// Class for prime number operations.
/// </summary>
/// <remarks>
/// A prime number is a natural number greater than 1 that is not a product of two smaller natural numbers.
/// </remarks>
public static class PrimeNumber
{
    /// <summary>
    /// Checks if a number is prime or not.
    /// </summary>
    /// <param name="number">Number to check.</param>
    /// <returns>True if number is prime, false otherwise.</returns>
    public static bool IsPrime(int number)
    {
        if (number <= 1)
        {
            return false;
        }

        if (number <= 3)
        {
            return true;
        }

        if (number % 2 == 0 || number % 3 == 0)
        {
            return false;
        }

        for (int i = 5; i * i <= number; i += 6)
        {
            if (number % i == 0 || number % (i + 2) == 0)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Gets the next prime number.
    /// </summary>
    /// <param name="number">Number to start from.</param>
    /// <param name="factor">Factor to multiply the number by.</param>
    /// <param name="desc">True to get the previous prime number, false otherwise.</param>
    /// <returns>The next prime number.</returns>
    public static int NextPrime(int number, int factor = 1, bool desc = false)
    {
        number = factor * number;
        int firstValue = number;

        while (!IsPrime(number))
        {
            number += desc ? -1 : 1;
        }

        if (number == firstValue)
        {
            return NextPrime(
                number + (desc ? -1 : 1),
                factor,
                desc);
        }

        return number;
    }
}
