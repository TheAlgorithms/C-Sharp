using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class DivisorsCountSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        // These values are taken from https://oeis.org/A000005 for comparison.
        var oeisSource = new BigInteger[]
                         {
                             1,  2, 2, 3,  2,  4,  2,  4,  3, 4, 2,  6, 2,
                             4,  4, 5, 2,  6,  2,  6,  4,  4, 2, 8,  3, 4,
                             4,  6, 2, 8,  2,  6,  4,  4,  4, 9, 2,  4, 4,
                             8,  2, 8, 2,  6,  6,  4,  2, 10, 3, 6,  4, 6,
                             2,  8, 4, 8,  4,  4,  2, 12,  2, 4, 6,  7, 4,
                             8,  2, 6, 4,  8,  2, 12,  2,  4, 6, 6,  4, 8,
                             2, 10, 5, 4,  2, 12,  4,  4,  4, 8, 2, 12, 4,
                             6,  4, 4, 4, 12,  2,  6,  6,  9, 2, 8,  2, 8,
                         };

        var sequence = new DivisorsCountSequence().Sequence.Take(oeisSource.Length);
        sequence.SequenceEqual(oeisSource).Should().BeTrue();
    }
}
