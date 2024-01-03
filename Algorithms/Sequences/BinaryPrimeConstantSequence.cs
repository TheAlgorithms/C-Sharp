using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

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
            var n = new BigInteger(0);

            foreach (var p in primes.Sequence)
            {
                for (n++; n < p; n++)
                {
                    yield return 0;
                }

                yield return 1;
            }
        }
    }
}
