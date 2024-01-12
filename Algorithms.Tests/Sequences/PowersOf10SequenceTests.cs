using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class PowersOf10SequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new PowersOf10Sequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] 
            { 1, 10, 100, 1000, 10000, 100000, 1000000, 10000000, 100000000, 1000000000 })
            .Should().BeTrue();
    }
}
