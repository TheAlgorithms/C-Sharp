using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of square numbers.
///     </para>
///     <para>
///         Wikipedia: https://wikipedia.org/wiki/Square_number.
///     </para>
///     <para>
///         OEIS: http://oeis.org/A000290.
///     </para>
/// </summary>
public class SquaresSequence : ISequence
{
    /// <summary>
    /// Gets sequence of square numbers.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var n = new BigInteger(0);

            while (true)
            {
                yield return n * n;
                n++;
            }
        }
    }
}
