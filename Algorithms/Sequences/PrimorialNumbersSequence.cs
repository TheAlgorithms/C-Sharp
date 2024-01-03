using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of primorial numbers: product of first n primes.
///     </para>
///     <para>
///         Wikipedia: https://wikipedia.org/wiki/Primorial.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A002110.
///     </para>
/// </summary>
public class PrimorialNumbersSequence : ISequence
{
    /// <summary>
    /// Gets sequence of primorial numbers.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var primes = new PrimesSequence().Sequence;
            var n = new BigInteger(1);

            foreach (var p in primes)
            {
                yield return n;
                n *= p;
            }
        }
    }
}
