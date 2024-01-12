using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

[TestFixture]
public static class MatchstickTriangleSequenceTests
{
    private static BigInteger[] _testList = {
                                               0, 1, 5, 13, 27, 48, 78, 118, 170, 235, 315, 411, 525, 658,
                                               812, 988, 1188, 1413, 1665, 1945, 2255, 2596, 2970, 3378,
                                               3822, 4303, 4823, 5383, 5985, 6630, 7320, 8056, 8840, 9673,
                                               10557, 11493, 12483, 13528, 14630, 15790, 17010, 18291,
                                               19635, 21043, 22517,
                                           };
    /// <summary>
    ///     This test uses the list values provided from http://oeis.org/A002717/list.
    /// </summary>
    [Test]
    public static void TestOeisList()
    {
        var sequence = new MatchstickTriangleSequence().Sequence.Take(_testList.Length);
        sequence.SequenceEqual(_testList).Should().BeTrue();
    }
}
