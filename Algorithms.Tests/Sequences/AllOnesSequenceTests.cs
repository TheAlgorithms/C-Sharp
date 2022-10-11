using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Numerics;

namespace Algorithms.Tests.Sequences;
public class AllOnesSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new AllOnesSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 })
            .Should().BeTrue();
    }
}
