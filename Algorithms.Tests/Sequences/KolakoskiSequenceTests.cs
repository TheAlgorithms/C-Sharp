using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class KolakoskiSequenceTests
{
    [Test]
    public void First100ElementsCorrect()
    {
        // Taken from https://oeis.org/A000002
        var expected = new BigInteger[]
        {
            1, 2, 2, 1, 1, 2, 1, 2, 2, 1,
            2, 2, 1, 1, 2, 1, 1, 2, 2, 1,
            2, 1, 1, 2, 1, 2, 2, 1, 1, 2,
            1, 1, 2, 1, 2, 2, 1, 2, 2, 1,
            1, 2, 1, 2, 2, 1, 2, 1, 1, 2,
            1, 1, 2, 2, 1, 2, 2, 1, 1, 2,
            1, 2, 2, 1, 2, 2, 1, 1, 2, 1,
            1, 2, 1, 2, 2, 1, 2, 1, 1, 2,
            2, 1, 2, 2, 1, 1, 2, 1, 2, 2,
            1, 2, 2, 1, 1, 2, 1, 1, 2, 2,
        };

        var sequence = new KolakoskiSequence().Sequence.Take(100);
        var sequence2 = new KolakoskiSequence2().Sequence.Take(100);

        sequence.Should().Equal(expected);
        sequence2.Should().Equal(expected);
    }
}
