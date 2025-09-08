using Algorithms.Sequences;

namespace Algorithms.Tests.Sequences;

public class NumberOfPrimesByPowersOf10SequenceTests
{
    [Test]
    public void First5ElementsCorrect()
    {
        var sequence = new NumberOfPrimesByPowersOf10Sequence().Sequence.Take(5);
        sequence.SequenceEqual(new BigInteger[] { 0, 4, 25, 168, 1229 })
            .Should().BeTrue();
    }
}
