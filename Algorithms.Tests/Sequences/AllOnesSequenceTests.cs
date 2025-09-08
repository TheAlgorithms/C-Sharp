using Algorithms.Sequences;

namespace Algorithms.Tests.Sequences;

public class AllOnesSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new AllOnesSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 })
            .Should().BeTrue();
    }
}
