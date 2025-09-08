namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Van Eck's sequence. For n >= 1, if there exists an m &lt; n such that a(m) = a(n), take the largest such m and set a(n+1) = n-m; otherwise a(n+1) = 0. Start with a(1)=0.
///     </para>
///     <para>
///         OEIS: http://oeis.org/A181391.
///     </para>
/// </summary>
public class VanEcksSequence : ISequence
{
    /// <summary>
    ///     Gets Van Eck's sequence.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            yield return 0;
            var dictionary = new Dictionary<BigInteger, BigInteger>();
            BigInteger previous = 0;
            BigInteger currentIndex = 2; // 1-based index
            while (true)
            {
                BigInteger element = 0;
                if (dictionary.TryGetValue(previous, out var previousIndex))
                {
                    element = currentIndex - previousIndex;
                }

                yield return element;

                dictionary[previous] = currentIndex;
                previous = element;
                currentIndex++;
            }
        }
    }
}
