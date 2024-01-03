using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of powers of 2: a(n) = 2^n.
///     </para>
///     <para>
///         Wikipedia: https://wikipedia.org/wiki/Power_of_two.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A000079.
///     </para>
/// </summary>
public class PowersOf2Sequence : ISequence
{
    /// <summary>
    /// Gets sequence of powers of 2.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var n = new BigInteger(1);

            while (true)
            {
                yield return n;
                n *= 2;
            }
        }
    }
}
