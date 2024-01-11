using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class SquaresSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new SquaresSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] { 0, 1, 4, 9, 16, 25, 36, 49, 64, 81 })
            .Should().BeTrue();
    }
}
