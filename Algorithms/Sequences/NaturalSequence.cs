using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of natural numbers.
///     </para>
///     <para>
///         Wikipedia: https://wikipedia.org/wiki/Natural_number.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A000027.
///     </para>
/// </summary>
public class NaturalSequence : ISequence
{
    /// <summary>
    ///     Gets sequence of natural numbers.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var n = new BigInteger(1);
            while (true)
            {
                yield return n++;
            }
        }
    }
}
