using Algorithms.Sequences;

namespace Algorithms.Tests.Sequences;

public class NumberOfBooleanFunctionsSequenceTests
{
    [Test]
    public void First5ElementsCorrect()
    {
        var sequence = new NumberOfBooleanFunctionsSequence().Sequence.Take(5);
        sequence.SequenceEqual(new BigInteger[] { 2, 4, 16, 256, 65536 })
            .Should().BeTrue();
    }
}
