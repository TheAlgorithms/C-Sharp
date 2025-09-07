namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         Sequence of number of primes less than or equal to n (PrimePi(n)).
///     </para>
///     <para>
///         Wikipedia: https://wikipedia.org/wiki/Prime-counting_function.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A000720.
///     </para>
/// </summary>
public class PrimePiSequence : ISequence
{
    /// <summary>
    /// Gets sequence of number of primes.
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            ISequence primes = new PrimesSequence();
            var n = new BigInteger(0);
            var counter = new BigInteger(0);

            foreach (var p in primes.Sequence)
            {
                for (n++; n < p; n++)
                {
                    yield return counter;
                }

                yield return ++counter;
            }
        }
    }
}
