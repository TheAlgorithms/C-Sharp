using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of Fermat numbers: a(n) = 2^(2^n) + 1.
///     </para>
///     <para>
///         Wikipedia: https://wikipedia.org/wiki/Fermat_number.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A000215.
///     </para>
/// </summary>
public class FermatNumbersSequence : ISequence
{
    /// <summary>
    /// Gets sequence of Fermat numbers.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var n = new BigInteger(2);

            while (true)
            {
                yield return n + 1;
                n *= n;
            }
        }
    }
}
