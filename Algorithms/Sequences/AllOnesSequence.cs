using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         The all ones sequence.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A000012.
///     </para>
/// </summary>
public class AllOnesSequence : ISequence
{
    /// <summary>
    ///     Gets all ones sequence.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            while (true)
            {
                yield return 1;
            }
        }
    }
}
