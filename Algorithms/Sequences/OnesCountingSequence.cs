using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     <para>
///         1's-counting sequence: number of 1's in binary expression of n.
///     </para>
///     <para>
///         OEIS: https://oeis.org/A000120.
///     </para>
/// </summary>
public class OnesCountingSequence : ISequence
{
    /// <summary>
    ///     <para>
    ///         Gets the generated sequence of the 1's contained in the binary representation of n.
    ///     </para>
    ///     <para>
    ///         The sequence is generated as follows.
    ///         1. The initial 0 value is provided.
    ///         2. A recursively generated sequence is iterated, starting with a length of 1 (i.e., 2^0),
    ///            followed by increasing 2^x length values.
    ///         3. Each sequence starts with the value 1, and a targeted value of depths that it will recurse
    ///            for the specific iteration.
    ///         4. If the call depth to the recursive function is met, it returns the value argument received.
    ///         5. If the call depth has not been met, it recurses to create 2 sequences, one starting with the
    ///            value argument, and the following with the value argument + 1.
    ///         6. Using ':' as a visual separator for each sequence, this results in the following sequences
    ///            that are returned sequentially after the initial 0.
    ///            1 : 1, 2 : 1, 2, 2, 3 : 1, 2, 2, 3, 2, 3, 3, 4.
    ///     </para>
    ///     <remarks>
    ///         <para>
    ///         This one comes from thinking over information contained within the COMMENTS section of the OEIS page.
    ///         </para>
    ///         <para>
    ///             Using the comments provided by Benoit Cloitre, Robert G. Wilson v, and Daniel Forgues, the above
    ///             algorithm was coded.
    ///         </para>
    ///     </remarks>
    /// </summary>
    public IEnumerable<BigInteger> Sequence
    {
        get
        {
            yield return 0;
            var depth = 0;
            while (true)
            {
                foreach (var count in GenerateFractalCount(BigInteger.One, depth))
                {
                    yield return count;
                }

                depth++;
            }
        }
    }

    /// <summary>
    ///     <para>
    ///         Recursive function to generate sequences.
    ///     </para>
    /// </summary>
    /// <param name="i">The value that will start off the current IEnumerable sequence.</param>
    /// <param name="depth">The remaining depth of recursion.  Value of 0 is the stop condition.</param>
    /// <returns>An IEnumerable sequence of BigInteger values that can be iterated over.</returns>
    private static IEnumerable<BigInteger> GenerateFractalCount(BigInteger i, int depth)
    {
        // Terminal condition
        if (depth == 0)
        {
            yield return i;
            yield break;
        }

        foreach (var firstHalf in GenerateFractalCount(i, depth - 1))
        {
            yield return firstHalf;
        }

        foreach (var secondHalf in GenerateFractalCount(i + 1, depth - 1))
        {
            yield return secondHalf;
        }
    }
}
