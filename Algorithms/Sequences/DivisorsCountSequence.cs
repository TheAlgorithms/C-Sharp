using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences
{
    /// <summary>
    ///     <para>
    ///         Sequence of the number of divisors of n, starting with 1.
    ///     </para>
    ///     <para>
    ///         OEIS: https://oeis.org/A000005.
    ///     </para>
    /// </summary>
    public class DivisorsCountSequence : ISequence
    {
        /// <summary>
        ///     Gets sequence of number of divisors for n, starting at 1.
        /// </summary>
        public IEnumerable<BigInteger> Sequence
        {
            get
            {
                var n = new BigInteger(1);
                while (true)
                {
                    var count = 0;
                    for (var k = 0; k <= n; k++)
                    {
                        BigInteger numerator = k * (k + n + 1);
                        BigInteger divisor = k + 1;
                        BigInteger.DivRem(numerator, divisor, out var remainder);
                        if (remainder != 0)
                        {
                            count++;
                        }
                    }

                    yield return count;
                }
            }
        }
    }
}
