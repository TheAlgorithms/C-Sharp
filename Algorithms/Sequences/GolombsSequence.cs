using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Golomb's sequence. a(n) is the number of times n occurs in the sequence, starting with a(1) = 1.
///     </para>
///     <para>
///         Wikipedia: https://en.wikipedia.org/wiki/Golomb_sequence.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A001462.
///     </para>
/// </summary>
public class GolombsSequence : ISequence
{
    /// <summary>
    ///     Gets Golomb's sequence.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            yield return 1;
            yield return 2;
            yield return 2;

            var queue = new Queue<BigInteger>();
            queue.Enqueue(2);

            for (var i = 3; ; i++)
            {
                var repetitions = queue.Dequeue();
                for (var j = 0; j < repetitions; j++)
                {
                    queue.Enqueue(i);
                    yield return i;
                }
            }
        }
    }
}
