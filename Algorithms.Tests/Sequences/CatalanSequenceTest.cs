using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class CatalanSequenceTest
{
    [Test]
    public void First30ItemsCorrect()
    {
        var sequence = new CatalanSequence().Sequence.Take(30);
        sequence.SequenceEqual(new BigInteger[]     { 1, 1, 2, 5, 14, 42, 132, 429, 1430, 4862, 16796, 58786, 208012, 742900, 2674440,
                                                    9694845, 35357670, 129644790, 477638700, 1767263190, 6564120420, 24466267020,
                                                    91482563640, 343059613650, 1289904147324, 4861946401452, 18367353072152,
                                                    69533550916004, 263747951750360, 1002242216651368})
            .Should().BeTrue();
    }
}
