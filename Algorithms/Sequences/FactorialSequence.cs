using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences
{
    /// <summary>
    /// <para>
    ///     Sequence of factorial numbers.
    /// </para>
    /// <para>
    ///     Wikipedia: https://en.wikipedia.org/wiki/Factorial.
    /// </para>
    /// <para>
    ///     OEIS: https://oeis.org/A000142.
    /// </para>
    /// </summary>
    public class FactorialSequence : ISequence
    {
        /// <summary>
        /// Gets sequence of factorial numbers.
        /// </summary>
        public IEnumerable<BigInteger> Sequence => GenerateFactorialSequence();

        private static IEnumerable<BigInteger> GenerateFactorialSequence()
        {
            var k = 0;

            while (true)
            {
                yield return Factorial(k++);
            }
        }

        private static BigInteger Factorial(long n)
        {
            if (n == 0)
            {
                return 1;
            }

            return n * Factorial(n - 1);
        }
    }
}
