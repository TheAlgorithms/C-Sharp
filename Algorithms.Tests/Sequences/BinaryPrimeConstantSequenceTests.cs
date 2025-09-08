using Algorithms.Sequences;

namespace Algorithms.Tests.Sequences;

public class BinaryPrimeConstantSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new BinaryPrimeConstantSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] { 0, 1, 1, 0, 1, 0, 1, 0, 0, 0 })
            .Should().BeTrue();
    }
}
