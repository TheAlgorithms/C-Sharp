using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Central polygonal numbers (the Lazy Caterer's sequence): n(n+1)/2 + 1; or, maximal number of pieces
///         formed when slicing a pancake with n cuts.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A000124.
///     </para>
/// </summary>
public class CentralPolygonalNumbersSequence : ISequence
{
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var n = new BigInteger(0);
            while (true)
            {
                var next = n * (n + 1) / 2 + 1;
                n++;
                yield return next;
            }
        }
    }
}
