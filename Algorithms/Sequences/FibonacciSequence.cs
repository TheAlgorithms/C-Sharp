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
        private readonly long sequenceLength;

        public FibonacciSequence(long sequenceLength) => this.sequenceLength = sequenceLength;

        /// <summary>
        /// Gets Fibonacci sequence.
        /// </summary>
        public IEnumerable<BigInteger> Sequence => GenerateFibonacciSequence(sequenceLength);

        private static IEnumerable<BigInteger> GenerateFibonacciSequence(long size)
        {
            var seq = new BigInteger[size];
            seq[1] = 1;

            for (var i = 2; i < size; i++)
            {
                seq[i] = seq[i - 1] + seq[i - 2];
            }

            return seq;
        }
    }
}
