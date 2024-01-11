using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class PowersOf2SequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new PowersOf2Sequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512 })
            .Should().BeTrue();
    }
}
