using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of Lucas number values.
///     </para>
///     <para>
///         Wikipedia: https://en.wikipedia.org/wiki/Lucas_number.
///     </para>
///     <para>
///         OEIS: http://oeis.org/A000032.
///     </para>
/// </summary>
public class LucasNumbersBeginningAt2Sequence : ISequence
{
    /// <summary>
    ///     <para>
    ///         Gets Lucas number sequence.
    ///     </para>
    ///     <para>
    ///         Lucas numbers follow the same type of operation that the Fibonacci (A000045)
    ///         sequence performs with starting values of 2, 1 versus 0,1.  As Fibonacci does,
    ///         the ratio between two consecutive Lucas numbers converges to phi.
    ///     </para>
    ///     <para>
    ///         This implementation is similar to A000204, but starts with the index of 0, thus having the
    ///         initial values being (2,1) instead of starting at index 1 with initial values of (1,3).
    ///     </para>
    ///     <para>
    ///         A simple relationship to Fibonacci can be shown with L(n) = F(n-1) + F(n+1), n>= 1.
    ///
    ///         n |  L(n) | F(n-1) | F(n+1)
    ///         --|-------|--------+--------+
    ///         0 |   2   |        |        |
    ///         1 |   1   |      0 |      1 |
    ///         2 |   3   |      1 |      2 |
    ///         3 |   4   |      1 |      3 |
    ///         4 |   7   |      2 |      5 |
    ///         5 |  11   |      3 |      8 |
    ///         --|-------|--------+--------+.
    ///     </para>
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            yield return 2;
            yield return 1;
            BigInteger previous = 2;
            BigInteger current = 1;
            while (true)
            {
                var next = previous + current;
                previous = current;
                current = next;

                yield return next;
            }
        }
    }
}
