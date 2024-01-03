using System.Collections.Generic;
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
public class KolakoskiSequence : ISequence
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

            var queue = new Queue<int>();
            queue.Enqueue(2);
            var nextElement = 1;
            while (true)
            {
                var nextRun = queue.Dequeue();
                for (var i = 0; i < nextRun; i++)
                {
                    queue.Enqueue(nextElement);
                    yield return nextElement;
                }

                nextElement = 1 + nextElement % 2;
            }
        }
    }
}
