using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Sequences;

/// <summary>
///     Common interface for all integer sequences.
/// </summary>
public interface ISequence
{
    /// <summary>
    ///     Gets sequence as enumerable.
    /// </summary>
    IEnumerable<BigInteger> Sequence { get; }
}
