using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class RecamansSequenceTests
{
    [Test]
    public void First50ElementsCorrect()
    {
        // Taken from http://oeis.org/A005132
        var expected = new BigInteger[]
        {
            0, 1, 3, 6, 2, 7, 13, 20, 12, 21,
            11, 22, 10, 23, 9, 24, 8, 25, 43, 62,
            42, 63, 41, 18, 42, 17, 43, 16, 44, 15,
            45, 14, 46, 79, 113, 78, 114, 77, 39, 78,
            38, 79, 37, 80, 36, 81, 35, 82, 34, 83,
        };

        var sequence = new RecamansSequence().Sequence.Take(50);

        sequence.Should().Equal(expected);
    }
}
