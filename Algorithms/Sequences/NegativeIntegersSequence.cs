using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of negative integers.
///     </para>
///     <para>
///         Wikipedia: https://wikipedia.org/wiki/Negative_number.
///     </para>
///     <para>
///         OEIS: http://oeis.org/A001478.
///     </para>
/// </summary>
public class NegativeIntegersSequence : ISequence
{
    /// <summary>
    /// Gets sequence of negative integers.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var n = new BigInteger(-1);

            while (true)
            {
                yield return n--;
            }
        }
    }
}
