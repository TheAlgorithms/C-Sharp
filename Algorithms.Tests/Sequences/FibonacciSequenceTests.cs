using Algorithms.Sequences;

namespace Algorithms.Tests.Sequences;

public class FibonacciSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new FibonacciSequence().Sequence.Take(10);
        BigInteger[] expected = [0, 1, 1, 2, 3, 5, 8, 13, 21, 34];
        sequence.SequenceEqual(expected)
            .Should().BeTrue();
    }
}
