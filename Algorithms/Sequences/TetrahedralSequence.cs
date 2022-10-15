using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of tetrahedral (triangular pyramids) counts for n >= 0.
///     </para>
///     <para>
///         OEIS: http://oeis.org/A000292.
///     </para>
///     <para>
///         Wikipedia: https://en.wikipedia.org/wiki/Tetrahedral_number.
///     </para>
/// </summary>
public class TetrahedralSequence : ISequence
{
    /// <summary>
    ///     <para>
    ///         Gets the value of packing spheres in a regular tetrahedron
    ///         with increasing by 1 triangular numbers under each layer.
    ///     </para>
    ///     <para>
    ///         It can be reviewed by starting at the 4th row of Pascal's Triangle
    ///         following the diagonal values going into the triangle.
    ///     </para>
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var index = BigInteger.Zero;
            var six = new BigInteger(6);
            while (true)
            {
                yield return BigInteger.Divide(index * (index + 1) * (index + 2), six);
                index++;
            }
        }
    }
}
