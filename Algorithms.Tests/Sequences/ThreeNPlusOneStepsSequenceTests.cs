using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class ThreeNPlusOneStepsSequenceTests {
    [Test]
    public void First50ElementsCorrect() {
        var sequence = new ThreeNPlusOneStepsSequence().Sequence.Take(50);
        var first50 = new BigInteger[] {
            0, 1, 7, 2, 5, 8, 16, 3, 19, 6,
            14, 9, 9, 17, 17, 4, 12, 20, 20, 7,
            7, 15, 15, 10, 23, 10, 111, 18, 18, 18,
            106, 5, 26, 13, 13, 21, 21, 21, 34, 8,
            109, 8, 29, 16, 16, 16, 104, 11, 24, 24
        };
        sequence.SequenceEqual(first50).Should().BeTrue();
    }
}
