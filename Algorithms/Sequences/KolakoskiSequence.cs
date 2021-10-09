using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences
{
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
                var list = new List<int> { 1, 2, 2 };
                yield return list[0];
                yield return list[1];
                yield return list[2];

                var nextElement = 1;
                var runIndex = 2;
                while (true)
                {
                    for (var i = 0; i < list[runIndex]; i++)
                    {
                        list.Add(nextElement);
                        yield return nextElement;
                    }

                    nextElement = nextElement == 1 ? 2 : 1;
                    runIndex++;
                }
            }
        }
    }
}
