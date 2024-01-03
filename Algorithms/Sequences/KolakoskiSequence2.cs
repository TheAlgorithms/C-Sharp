using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Kolakoski sequence; n-th element is the length of the n-th run in the sequence itself.
///     </para>
///     <para>
///         Wikipedia: https://en.wikipedia.org/wiki/Kolakoski_sequence.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A000002.
///     </para>
/// </summary>
public class KolakoskiSequence2 : ISequence
{
    /// <summary>
    /// Gets Kolakoski sequence.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            yield return 1;
            yield return 2;
            yield return 2;

            var inner = new KolakoskiSequence2().Sequence.Skip(2);
            var nextElement = 1;
            foreach (var runLength in inner)
            {
                yield return nextElement;
                if (runLength > 1)
                {
                    yield return nextElement;
                }

                nextElement = 1 + nextElement % 2;
            }
        }
    }
}
