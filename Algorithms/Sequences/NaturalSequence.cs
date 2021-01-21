using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences
{
    /// <summary>
    /// <para>
    ///     Sequence of natural numbers.
    /// </para>
    /// <para>
    ///     Wikipedia: https://wikipedia.org/wiki/Natural_number.
    /// </para>
    /// <para>
    ///     OEIS: https://oeis.org/A000027.
    /// </para>
    /// </summary>
    public class NaturalSequence : ISequence
    {
        private readonly long sequenceLength;

        public NaturalSequence(long sequenceLength) => this.sequenceLength = sequenceLength;

        /// <summary>
        /// Gets sequence of natural numbers.
        /// </summary>
        public IEnumerable<BigInteger> Sequence => GenerateSequence(sequenceLength);

        private static IEnumerable<BigInteger> GenerateSequence(long size)
        {
            var current = new BigInteger(1);

            while (current <= size)
            {
                yield return current;
                current++;
            }
        }
    }
}
