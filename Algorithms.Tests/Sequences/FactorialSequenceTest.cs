using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class FactorialSequenceTest
{
    [Test]
    public void First10ItemsCorrect()
    {
        var sequence = new FactorialSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] { 1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880 })
            .Should().BeTrue();
    }
}
