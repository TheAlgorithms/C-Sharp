using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class FermatNumbersSequenceTests
{
    [Test]
    public void First5ElementsCorrect()
    {
        var sequence = new FermatNumbersSequence().Sequence.Take(5);
        sequence.SequenceEqual(new BigInteger[] { 3, 5, 17, 257, 65537 })
            .Should().BeTrue();
    }
}
