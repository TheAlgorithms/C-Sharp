using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of number of triangles in triangular matchstick arrangement of side n for n>=0.
///     </para>
///     <para>
///         M. E. Larsen, The eternal triangle – a history of a counting problem, College Math. J., 20 (1989), 370-392.
///         https://web.math.ku.dk/~mel/mel.pdf.
///     </para>
///     <para>
///         OEIS: http://oeis.org/A002717.
///     </para>
/// </summary>
public class MatchstickTriangleSequence : ISequence
{
    /// <summary>
    ///     <para>
    ///         Gets number of triangles contained in an triangular arrangement of matchsticks of side length n.
    ///     </para>
    ///     <para>
    ///         This also counts the subset of smaller triangles contained within the arrangement.
    ///     </para>
    ///     <para>
    ///         Based on the PDF referenced above, the sequence is derived from step 8, using the resulting equation
    ///         of f(n) = (n(n+2)(2n+1) -(delta)(n)) / 8.  Using BigInteger values, we can effectively remove
    ///         (delta)(n) from the previous by using integer division instead.
    ///     </para>
    ///     <para>
    ///         Examples follow.
    /// <pre>
    ///   .
    ///  / \   This contains 1 triangle of size 1.
    /// .---.
    ///
    ///     .
    ///    / \     This contains 4 triangles of size 1.
    ///   .---.    This contains 1 triangle of size 2.
    ///  / \ / \   This contains 5 triangles total.
    /// .---.---.
    ///
    ///       .
    ///      / \      This contains 9 triangles of size 1.
    ///     .---.     This contains 3 triangles of size 2.
    ///    / \ / \    This contains 1 triangles of size 3.
    ///   .---.---.
    ///  / \ / \ / \  This contains 13 triangles total.
    /// .---.---.---.
    /// </pre>
    ///     </para>
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            var index = BigInteger.Zero;
            var eight = new BigInteger(8);
            while (true)
            {
                var temp = index * (index + 2) * (index * 2 + 1);
                var result = BigInteger.Divide(temp, eight);
                yield return result;
                index++;
            }
        }
    }
}
