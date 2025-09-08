using Algorithms.Sequences;

namespace Algorithms.Tests.Sequences;

public class EuclidNumbersSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new EuclidNumbersSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[]
            { 2, 3, 7, 31, 211, 2311, 30031, 510511, 9699691, 223092871 })
            .Should().BeTrue();
    }
}
