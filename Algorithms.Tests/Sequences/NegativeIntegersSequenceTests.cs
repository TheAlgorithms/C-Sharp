using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class NegativeIntegersSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new NegativeIntegersSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] { -1, -2, -3, -4, -5, -6, -7, -8, -9, -10 })
            .Should().BeTrue();
    }
}
