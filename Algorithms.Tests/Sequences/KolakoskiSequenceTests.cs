using Algorithms.Sequences;

namespace Algorithms.Tests.Sequences;

public class KolakoskiSequenceTests
{
    [Test]
    public void First100ElementsCorrect()
    {
        // Taken from https://oeis.org/A000002
        BigInteger[] expected =
        [
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
        ];

        var sequence = new KolakoskiSequence().Sequence.Take(100);
        var sequence2 = new KolakoskiSequence2().Sequence.Take(100);

        sequence.Should().Equal(expected);
        sequence2.Should().Equal(expected);
    }
}
