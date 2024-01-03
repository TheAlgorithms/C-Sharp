using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Recaman's sequence. a(0) = 0; for n > 0, a(n) = a(n-1) - n if nonnegative and not already in the sequence, otherwise a(n) = a(n-1) + n.
///     </para>
///     <para>
///         Wikipedia: https://en.wikipedia.org/wiki/Recam%C3%A1n%27s_sequence.
///     </para>
///     <para>
///         OEIS: http://oeis.org/A005132.
///     </para>
/// </summary>
public class RecamansSequence : ISequence
{
    /// <summary>
    ///     Gets Recaman's sequence.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            yield return 0;
            var elements = new HashSet<BigInteger> { 0 };
            var previous = 0;
            var i = 1;

            while (true)
            {
                var current = previous - i;
                if (current < 0 || elements.Contains(current))
                {
                    current = previous + i;
                }

                yield return current;
                previous = current;
                elements.Add(current);
                i++;
            }
        }
    }
}
