using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class FibonacciSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new FibonacciSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 })
            .Should().BeTrue();
    }
}
