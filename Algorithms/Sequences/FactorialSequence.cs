using System.Collections.Generic;
using System.Linq;
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
        private readonly long sequenceLength;

        public FactorialSequence(long sequenceLength) => this.sequenceLength = sequenceLength;

        /// <summary>
        /// Gets sequence of binomial coefficients.
        /// </summary>
        public IEnumerable<BigInteger> Sequence => GenerateFactorialSequence(sequenceLength);

        private static IEnumerable<BigInteger> GenerateFactorialSequence(long size)
        {
            var k = 0;

            while (k < size)
            {
                yield return Factorial(k);
                k++;
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
