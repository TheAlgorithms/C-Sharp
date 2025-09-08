using Algorithms.Sequences;

namespace Algorithms.Tests.Sequences;

public class ZeroSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new ZeroSequence().Sequence.Take(10);
        sequence.SequenceEqual(Enumerable.Repeat(BigInteger.Zero, 10))
                .Should().BeTrue();
    }

}
