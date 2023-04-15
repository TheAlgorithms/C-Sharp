using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Numerics;

namespace Algorithms.Tests.Sequences;
public class AllThreesSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new AllThreesSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 })
            .Should().BeTrue();
    }
}
