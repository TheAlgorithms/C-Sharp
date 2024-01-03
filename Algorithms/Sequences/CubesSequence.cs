using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of cube numbers.
///     </para>
///     <para>
///         Wikipedia: https://en.wikipedia.org/wiki/Cube_(algebra).
///     </para>
///     <para>
///         OEIS: https://oeis.org/A000578.
///     </para>
/// </summary>
public class CubesSequence : ISequence
{
    /// <summary>
    /// Gets sequence of cube numbers.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var n = BigInteger.Zero;

            while (true)
            {
                yield return n * n * n;
                n++;
            }
        }
    }
}
