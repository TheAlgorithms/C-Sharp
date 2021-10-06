using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algorithms.Sequences
{
    /// <summary>
    ///     <para>
    ///         Sequence of Euler totient function phi(n).
    ///     </para>
    ///     <para>
    ///         Wikipedia: https://en.wikipedia.org/wiki/Euler%27s_totient_function.
    ///     </para>
    ///     <para>
    ///         OEIS: https://oeis.org/A000010.
    ///     </para>
    /// </summary>
    public class EulerTotientSequence : ISequence
    {
        /// <summary>
        ///     <para>
        ///         Gets sequence of Euler totient function phi(n).
        ///     </para>
        /// </summary>
        public IEnumerable<BigInteger> Sequence
        {
            get
            {
                yield return BigInteger.One;

                for (BigInteger i = 2; ; i++)
                {
                    var result = i;
                    var n = i;

                    var factors = PrimeFactors(i);
                    foreach (var factor in factors)
                    {
                        while (n % factor == 0)
                        {
                            n /= factor;
                        }

                        result -= result / factor;
                    }

                    if (n > 1)
                    {
                        result -= result / n;
                    }

                    yield return result;
                }
            }
        }

        /// <summary>
        ///     <para>
        ///         Uses the prime sequence to find all prime factors of the
        ///         number we're looking at.
        ///     </para>
        ///     <para>
        ///         The prime sequence is examined until its value squared is
        ///         less than or equal to target, and checked to make sure it
        ///         evenly divides the target.  If it evenly divides, it's added
        ///         to the result which is returned as a List.
        ///     </para>
        /// </summary>
        /// <param name="target">Number that is being factored.</param>
        /// <returns>List of prime factors of target.</returns>
        private static IEnumerable<BigInteger> PrimeFactors(BigInteger target)
        {
            return new PrimesSequence()
                  .Sequence.TakeWhile(prime => prime * prime <= target)
                  .Select(prime => new { prime, test = target / prime, })
                  .Where(t => t.test * t.prime == target)
                  .Select(t => t.prime)
                  .ToList();
        }
    }
}
