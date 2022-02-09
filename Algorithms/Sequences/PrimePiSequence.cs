using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algorithms.Sequences
{
    /// <summary>
    ///     <para>
    ///         Sequence of number of primes less than or equal to n (PrimePi(n)).
    ///     </para>
    ///     <para>
    ///         Wikipedia: https://wikipedia.org/wiki/Prime-counting_function.
    ///     </para>
    ///     <para>
    ///         OEIS: https://oeis.org/A000720.
    ///     </para>
    /// </summary>
    public class PrimePiSequence : ISequence
    {
        /// <summary>
        /// Gets sequence of number of primes.
        /// </summary>
        public IEnumerable<BigInteger> Sequence
        {
            get
            {
                ISequence primes = new PrimesSequence();
                var n = new BigInteger(1);
                var counter = new BigInteger(0);

                while (true)
                {
                    counter += primes.Sequence.SkipWhile(p => p < n).TakeWhile(p => p <= n).Count();

                    yield return counter;
                    n++;
                }
            }
        }
    }
}
