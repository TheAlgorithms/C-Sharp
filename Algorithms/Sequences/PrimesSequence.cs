using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algorithms.Sequences
{
    /// <summary>
    /// <para>
    ///     Sequence of prime numbers.
    /// </para>
    /// <para>
    ///     Wikipedia: https://wikipedia.org/wiki/Prime_number.
    /// </para>
    /// <para>
    ///     OEIS: https://oeis.org/A000040.
    /// </para>
    /// </summary>
    public class PrimesSequence : ISequence
    {
        private readonly long sequenceLength;

        public PrimesSequence(long sequenceLength) => this.sequenceLength = sequenceLength;

        /// <summary>
        /// Gets sequence of prime numbers.
        /// </summary>
        public IEnumerable<BigInteger> Sequence => GeneratePrimeSequence(sequenceLength);

        private static IEnumerable<BigInteger> GeneratePrimeSequence(long limitValue)
        {
            var list = new List<BigInteger> { 2 };

            for (var i = 3; i < limitValue; i++)
            {
                var current = new BigInteger(i);

                if (list.All(x => current % x != 0))
                {
                    list.Add(current);
                }
            }

            return list;
        }
    }
}
