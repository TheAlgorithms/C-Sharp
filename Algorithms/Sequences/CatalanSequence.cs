using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Catalan numbers: C[n+1] = (2*(2*n+1)*C[n])/(n+2).
///     </para>
///     <para>
///         Wikipedia: https://en.wikipedia.org/wiki/Catalan_number.
///     </para>
///     <para>
///         OEIS:http://oeis.org/A000108.
///     </para>
/// </summary>
public class CatalanSequence : ISequence
{
    /// <summary>
    ///     Gets sequence of Catalan numbers.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            // initialize the first element (1) and define it's enumerator (0)
            var catalan = new BigInteger(1);
            var n = 0;
            while (true)
            {
                yield return catalan;
                catalan = (2 * (2 * n + 1) * catalan) / (n + 2);
                n++;
            }
        }
    }
}
