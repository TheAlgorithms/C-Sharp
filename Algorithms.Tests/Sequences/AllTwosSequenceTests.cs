using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;
using System.Numerics;

namespace Algorithms.Tests.Sequences;
public class AllTwosSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new AllTwosSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 })
            .Should().BeTrue();
    }
}
