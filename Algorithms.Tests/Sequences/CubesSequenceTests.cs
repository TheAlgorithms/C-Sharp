using Algorithms.Sequences;

namespace Algorithms.Tests.Sequences;

public class CubesSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new CubesSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] { 0, 1, 8, 27, 64, 125, 216, 343, 512, 729 })
            .Should().BeTrue();
    }
}
