using System;

namespace Algorithms.Numeric
{
    /// <summary>
    /// A Krishnamurthy number is a number whose sum of the factorial of digits
    /// is equal to the number itself.
    ///
    /// For example, 145 is a Krishnamurthy number since: 1! + 4! + 5! = 1 + 24 + 120 = 145.
    /// </summary>
    public class KrishnamurthyNumberChecker
    {
        /// <summary>
        /// Check if a number is Krishnamurthy number or not.
        /// </summary>
        /// <param name="n">The number to check.</param>
        /// <returns>True if the number is Krishnamurthy, false otherwise.</returns>
        public static bool IsKMurthyNumber(int n)
        {
            int sumOfFactorials = 0;
            int tmp = n;

            if (n <= 0)
            {
                return false;
            }

            while (n != 0)
            {
                int factorial = Factorial(n % 10);
                sumOfFactorials += factorial;
                n = n / 10;
            }

            return tmp == sumOfFactorials;
        }

        private static int Factorial(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException($"Expected non-negative number. Actual number: {n}");
            }

            return (n == 0 || n == 1) ? 1 : n * Factorial(n - 1);
        }
    }
}
