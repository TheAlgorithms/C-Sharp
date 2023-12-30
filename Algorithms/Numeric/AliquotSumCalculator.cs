using System;

namespace Algorithms.Numeric;

/// <summary>
///     In number theory, the aliquot sum s(n) of a positive integer n is the sum of all proper divisors
///     of n, that is, all divisors of n other than n itself. For example, the proper divisors of 15
///     (that is, the positive divisors of 15 that are not equal to 15) are 1, 3 and 5, so the aliquot
///     sum of 15 is 9 i.e. (1 + 3 + 5). Wikipedia: https://en.wikipedia.org/wiki/Aliquot_sum.
/// </summary>
public static class AliquotSumCalculator
{
    /// <summary>
    ///     Finds the aliquot sum of an integer number.
    /// </summary>
    /// <param name="number">Positive number.</param>
    /// <returns>The Aliquot Sum.</returns>
    /// <exception cref="ArgumentException">Error number is not on interval (0.0; int.MaxValue).</exception>
    public static int CalculateAliquotSum(int number)
    {
        if (number < 0)
        {
            throw new ArgumentException($"{nameof(number)} cannot be negative");
        }

        var sum = 0;
        for (int i = 1, limit = number / 2; i <= limit; ++i)
        {
            if (number % i == 0)
            {
                sum += i;
            }
        }

        return sum;
    }
}
