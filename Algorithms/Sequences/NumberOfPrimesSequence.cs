using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algorithms.Sequences
{
    /// <summary>
    ///     <para>
    ///         Sequence of numbers of primes less than 10^n (prime-counting function π(x)).
    ///     </para>
    ///     <para>
    ///         Wikipedia: https://en.wikipedia.org/wiki/Prime-counting_function.
    ///     </para>
    ///     <para>
    ///         OEIS: https://oeis.org/A006880.
    ///     </para>
    /// </summary>
    public class NumberOfPrimesSequence : ISequence
    {
        /// <summary>
        /// Gets sequence of numbers of primes.
        /// </summary>
        public IEnumerable<BigInteger> Sequence
        {
            get
            {
                yield return 0;

                var primes = new List<BigInteger>
                {
                    2,
                };

                var i = new BigInteger(3);
                var n = 1;

                while (true)
                {
                    for (; i < (BigInteger)Math.Pow(10, n);)
                    {
                        if (primes.All(p => i % p != 0))
                        {
                            primes.Add(i);
                        }

                        i += 2;
                    }

                    yield return primes.Count;
                    n++;
                }
            }
        }
    }
}
