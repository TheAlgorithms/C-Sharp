using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class GolombsSequenceTests
{
    [Test]
    public void First50ElementsCorrect()
    {
        // Taken from https://oeis.org/A001462
        var expected = new BigInteger[] {
            1, 2, 2, 3, 3, 4, 4, 4, 5, 5,
            5, 6, 6, 6, 6, 7, 7, 7, 7, 8,
            8, 8, 8, 9, 9, 9, 9, 9, 10, 10,
            10, 10, 10, 11, 11, 11, 11, 11, 12, 12,
            12, 12, 12, 12, 13, 13, 13, 13, 13, 13};

        var sequence = new GolombsSequence().Sequence.Take(50);

        sequence.SequenceEqual(expected).Should().BeTrue();
    }
}
