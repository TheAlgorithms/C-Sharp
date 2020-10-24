using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Algorithms.Problems.Fibonacci
{
    public class Fibonacci
    {
        /// <summary>
        /// The Fibonacci sequence is defined as a N = (N -1) + (N - 2).
        /// Given a number n, this function returns that corresponding
        /// Fibonacci number in 2 ^ n time.
        ///
        /// For example, if n = 5 this function will return the 4th
        /// Fibonacci number which woud be 3 (0, 1, 1, 2, 3).
        ///
        /// </summary>
        /// <param name="n">The nth Fibonacci number to find.</param>
        /// <returns>The nth Fibonacci number.</returns>
        public static BigInteger FibonacciNumberFinder(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("n must be greater than 0.");
            }

            if (n <= 1)
            {
                return n;
            }

            return FibonacciNumberFinder(n - 1) + FibonacciNumberFinder(n - 2);
        }
    }
}
