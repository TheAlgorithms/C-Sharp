using Algorithms.Sequences;

namespace Algorithms.Tests.Sequences;

public class NumberOfPrimesByNumberOfDigitsSequenceTests
{
    [Test]
    public void First5ElementsCorrect()
    {
        var sequence = new NumberOfPrimesByNumberOfDigitsSequence().Sequence.Take(5);
        sequence.SequenceEqual(new BigInteger[] { 0, 4, 21, 143, 1061 })
            .Should().BeTrue();
    }
}
