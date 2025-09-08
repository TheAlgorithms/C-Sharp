using Algorithms.Sequences;

namespace Algorithms.Tests.Sequences;

public class BinomialSequenceTests
{
    [Test]
    public void First4RowsCorrect()
    {
        var sequence = new BinomialSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] { 1, 1, 1, 1, 2, 1, 1, 3, 3, 1 })
            .Should().BeTrue();
    }
}
