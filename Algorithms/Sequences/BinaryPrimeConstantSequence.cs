using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algorithms.Sequences
{
    /// <summary>
    ///     <para>
    ///         Sequence of binary prime constant
    ///         (Characteristic function of primes: 1 if n is prime, else 0).
    ///     </para>
    ///     <para>
    ///         Wikipedia: https://wikipedia.org/wiki/Prime_constant.
    ///     </para>
    ///     <para>
    ///         OEIS: https://oeis.org/A010051.
    ///     </para>
    /// </summary>
    public class BinaryPrimeConstantSequence : ISequence
    {
        /// <summary>
        /// Gets sequence of binary prime constant.
        /// </summary>
        public IEnumerable<BigInteger> Sequence
        {
            get
            {
                ISequence primes = new PrimesSequence();
                var n = 1;
                var isPrime = 0;

                while (true)
                {
                    isPrime = primes.Sequence.SkipWhile(p => p < n).TakeWhile(p => p <= n).Count();

                    yield return isPrime;
                    n++;
                }
            }
        }
    }
}
