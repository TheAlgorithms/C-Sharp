using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         The all twos sequence.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A007395.
///     </para>
/// </summary>
public class AllTwosSequence : ISequence
{
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            while (true)
            {
                yield return 2;
            }
        }
    }
}
