using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class FermatPrimesSequenceTests
{
    [Test]
    public void All5ElementsCorrect()
    {
        var sequence = new FermatPrimesSequence().Sequence.Take(5);
        sequence.SequenceEqual(new BigInteger[] { 3, 5, 17, 257, 65537 })
            .Should().BeTrue();
    }
}
