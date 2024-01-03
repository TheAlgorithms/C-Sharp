using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

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
            yield return BigInteger.One;
            for (var n = new BigInteger(2); ; n++)
            {
                var count = 2;
                for (var k = 2; k < n; k++)
                {
                    BigInteger.DivRem(n, k, out var remainder);
                    if (remainder == 0)
                    {
                        count++;
                    }
                }

                yield return count;
            }
        }
    }
}
