using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Tribonacci numbers: a(n) = a(n-1) + a(n-2) + a(n-3) with a(0)=a(1)=a(2)=1.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A000213.
///     </para>
/// </summary>
public class TribonacciNumbersSequence : ISequence
{
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            yield return 1;
            yield return 1;
            yield return 1;
            BigInteger beforePrevious = 1;
            BigInteger previous = 1;
            BigInteger current = 1;
            while (true)
            {
                var next = beforePrevious + previous + current;
                beforePrevious = previous;
                previous = current;
                current = next;
                yield return next;
            }
        }
    }
}
