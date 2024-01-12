using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class PrimesSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new PrimesSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 })
            .Should().BeTrue();
    }
}
