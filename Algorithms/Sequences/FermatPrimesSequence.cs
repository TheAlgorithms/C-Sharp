using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of Fermat primes: primes of the form 2^(2^k) + 1, for some k >= 0.
///     </para>
///     <para>
///         Wikipedia: https://wikipedia.org/wiki/Fermat_number.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A019434.
///     </para>
/// </summary>
public class FermatPrimesSequence : ISequence
{
    /// <summary>
    /// Gets sequence of Fermat primes.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var fermatNumbers = new FermatNumbersSequence().Sequence.Take(5);

            foreach (var n in fermatNumbers)
            {
                yield return n;
            }
        }
    }
}
