using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences
{
    /// <summary>
    /// <para>
    ///     Fibonacci sequence.
    /// </para>
    /// <para>
    ///     Wikipedia: https://wikipedia.org/wiki/Fibonacci_number.
    /// </para>
    /// <para>
    ///     OEIS: https://oeis.org/A000045.
    /// </para>
    /// </summary>
    public class FibonacciSequence : ISequence
    {
        /// <summary>
        /// Gets Fibonacci sequence.
        /// </summary>
        public IEnumerable<BigInteger> Sequence => GenerateFibonacciSequence();

        private static IEnumerable<BigInteger> GenerateFibonacciSequence()
        {
            long k = 0;

            while (true)
            {
                yield return Fibonacci(k++);
            }
        }

        private static BigInteger Fibonacci(long n)
        {
            if (n == 0)
            {
                return new BigInteger(0);
            }

            if (n == 1)
            {
                return new BigInteger(1);
            }

            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }
    }
}
