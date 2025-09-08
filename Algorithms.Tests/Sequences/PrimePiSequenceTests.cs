using Algorithms.Sequences;

namespace Algorithms.Tests.Sequences;

public class PrimePiSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new PrimePiSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] { 0, 1, 2, 2, 3, 3, 4, 4, 4, 4 })
            .Should().BeTrue();
    }
}
