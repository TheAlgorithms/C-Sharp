using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of prime numbers.
///     </para>
///     <para>
///         Wikipedia: https://wikipedia.org/wiki/Prime_number.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A000040.
///     </para>
/// </summary>
public class PrimesSequence : ISequence
{
    /// <summary>
    ///     Gets sequence of prime numbers.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            yield return 2;
            var primes = new List<BigInteger>
            {
                2,
            };
            var n = new BigInteger(3);

            while (true)
            {
                if (primes.All(p => n % p != 0))
                {
                    yield return n;
                    primes.Add(n);
                }

                n += 2;
            }
        }
    }
}
