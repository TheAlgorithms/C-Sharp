using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of powers of 10: a(n) = 10^n.
///     </para>
///     <para>
///         Wikipedia: https://wikipedia.org/wiki/Power_of_10.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A011557.
///     </para>
/// </summary>
public class PowersOf10Sequence : ISequence
{
    /// <summary>
    /// Gets sequence of powers of 10.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var n = new BigInteger(1);

            while (true)
            {
                yield return n;
                n *= 10;
            }
        }
    }
}
