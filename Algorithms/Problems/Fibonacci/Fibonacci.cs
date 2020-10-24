using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Algorithms.Problems.Fibonacci
{
    public class Fibonacci
    {
        /// <summary>
        /// The Fibonacci sequence is a sequence of numbers where
        /// a number is the addition of the last two numbers.
        /// Given a number n, this function returns that corresponding
        /// Fibonacci number in O(2^n) time.
        ///
        /// For example, if n = 6 this function will return the 5th
        /// Fibonacci number which woud be 8 (0, 1, 1, 2, 3, 5, 8).
        ///
        /// </summary>
        /// <param name="n">The nth Fibonacci number to find.</param>
        /// <returns>The nth Fibonacci number.</returns>
        public static BigInteger GetFibonacciNumber(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("n must be greater than 0.");
            }

            if (n <= 1)
            {
                return n;
            }

            return GetFibonacciNumber(n - 1) + GetFibonacciNumber(n - 2);
        }
    }
}
