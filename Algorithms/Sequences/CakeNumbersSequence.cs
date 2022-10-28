using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Cake numbers: maximal number of pieces resulting from n planar cuts through a cube
///         (or cake): C(n+1,3) + n + 1.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A000125.
///     </para>
/// </summary>
public class CakeNumbersSequence : ISequence
{
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var n = new BigInteger(0);
            while (true)
            {
                var next = (BigInteger.Pow(n, 3) + 5 * n + 6) / 6;
                n++;
                yield return next;
            }
        }
    }
}
