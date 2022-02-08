using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algorithms.Sequences
{
    /// <summary>
    ///     <para>
    ///         Sequence of number of primes less than 10^n (with at most n digits).
    ///     </para>
    ///     <para>
    ///         Wikipedia: https://wikipedia.org/wiki/Prime-counting_function.
    ///     </para>
    ///     <para>
    ///         OEIS: https://oeis.org/A006880.
    ///     </para>
    /// </summary>
    public class NumberOfPrimesByPowersOf10Sequence : ISequence
    {
        /// <summary>
        /// Gets sequence of numbers of primes.
        /// </summary>
        public IEnumerable<BigInteger> Sequence
        {
            get
            {
                ISequence primes = new PrimesSequence();
                var prevPowerOf10 = new BigInteger(0);
                var powerOf10 = new BigInteger(1);
                var counter = new BigInteger(0);

                while (true)
                {
                    counter += primes.Sequence.
                        SkipWhile(p => p < prevPowerOf10).TakeWhile(p => p < powerOf10).Count();

                    yield return counter;
                    prevPowerOf10 = powerOf10;
                    powerOf10 *= 10;
                }
            }
        }
    }
}
