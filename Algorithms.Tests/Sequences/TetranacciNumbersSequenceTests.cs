using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class TetranacciNumbersSequenceTests
{
    [Test]
    public void First35ElementsCorrect()
    {
        var sequence = new TetranacciNumbersSequence().Sequence.Take(35);
        sequence.SequenceEqual(new BigInteger[]
            {
                1, 1, 1, 1, 4, 7, 13, 25, 49, 94, 181, 349, 673, 1297, 2500, 4819, 9289, 17905, 34513, 66526, 128233,
                247177, 476449, 918385, 1770244, 3412255, 6577333, 12678217, 24438049, 47105854, 90799453, 175021573,
                337364929, 650291809, 1253477764,
            })
            .Should().BeTrue();
    }
}
