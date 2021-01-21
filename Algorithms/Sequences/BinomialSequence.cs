using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences
{
    /// <summary>
    /// <para>
    ///     Sequence of binomial coefficients.
    /// </para>
    /// <para>
    ///     Wikipedia: https://en.wikipedia.org/wiki/Binomial_coefficient.
    /// </para>
    /// <para>
    ///     OEIS: http://oeis.org/A007318.
    /// </para>
    /// </summary>
    public class BinomialSequence : ISequence
    {
        private readonly long sequenceLength;

        public BinomialSequence(long sequenceLength) => this.sequenceLength = sequenceLength;

        /// <summary>
        /// Gets sequence of binomial coefficients.
        /// </summary>
        public IEnumerable<BigInteger> Sequence => GenerateBinomialSequence(sequenceLength);

        private static IEnumerable<BigInteger> GenerateBinomialSequence(long rowNumbers)
        {
            for (long i = 0; i < rowNumbers; i++)
            {
                var row = GenerateRow(i);
                foreach (var coefficient in row)
                {
                    yield return coefficient;
                }
            }
        }

        private static BigInteger BinomialCoefficient(long n, long k)
        {
            if (k == 0 || k == n)
            {
                return new BigInteger(1);
            }

            if (n < 0)
            {
                return new BigInteger(0);
            }

            return BinomialCoefficient(n - 1, k) + BinomialCoefficient(n - 1, k - 1);
        }

        private static IEnumerable<BigInteger> GenerateRow(long n)
        {
            long k = 0;

            while (k <= n)
            {
                yield return BinomialCoefficient(n, k);
                k++;
            }
        }
    }
}
