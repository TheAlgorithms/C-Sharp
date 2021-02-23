using System;

namespace Algorithms.Numeric
{
    /// <summary>
    /// In number theory, a perfect number is a positive integer that is equal to the sum of its positive
    /// divisors, excluding the number itself.For instance, 6 has divisors 1, 2 and 3 (excluding
    /// itself), and 1 + 2 + 3 = 6, so 6 is a perfect number.
    /// </summary>
    public static class PerfectNumberChecker
    {
        /// <summary>
        /// Checks if a number is a perfect number or not.
        /// </summary>
        /// <param name="number">Number to check.</param>
        /// <returns>True if is a perfect number; False otherwise.</returns>
        /// <exception cref="ArgumentException">Error number is not on interval (0.0; int.MaxValue).</exception>
        public static bool IsPerfectNumber(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException($"{nameof(number)} cannot be negative");
            }

            var sum = 0; /* sum of its positive divisors */
            for (var i = 1; i < number; ++i)
            {
                if (number % i == 0)
                {
                    sum += i;
                }
            }

            return sum == number;
        }
    }
}
