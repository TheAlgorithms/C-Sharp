using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Algorithms.Problems.Fibonacci
{
    public class FibonacciMemo
    {
        /// <summary>
        /// The Fibonacci sequence is a sequence of numbers where
        /// a number is the addition of the last two numbers.
        ///
        /// This is the memoized solution to the Fibonacci problem.
        /// This solution runs in O(N) time.
        ///
        /// </summary>
        /// <param name="n">The nth Fibonacci number to find.</param>
        /// <returns>The nth Fibonacci number.</returns>
        public static BigInteger GetFibonacciNumberMemo(int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("N must be greater than or equal to 0");
            }

            List<BigInteger> memo = new List<BigInteger> { 0, 1 };

            if (n < 2)
            {
                return memo[n];
            }

            for (int i = 2; i <= n; i++)
            {
                BigInteger nextFibNumber = memo[i - 1] + memo[i - 2];
                memo.Add(nextFibNumber);
            }

            return memo.Last();
        }
    }
}
