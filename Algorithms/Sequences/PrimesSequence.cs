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
        /// <summary>
        /// Gets sequence of prime numbers.
        /// </summary>
        public IEnumerable<BigInteger> Sequence => GeneratePrimeSequence();

        private static IEnumerable<BigInteger> GeneratePrimeSequence()
        {
            var primeSequence = new List<BigInteger> { 2 };
            yield return 2;

            var k = 3;

            while (true)
            {
                var current = new BigInteger(k);

                if (primeSequence.All(x => current % x != 0))
                {
                    primeSequence.Add(current);
                    yield return current;
                }

                k += 2;
            }
        }
    }
}
