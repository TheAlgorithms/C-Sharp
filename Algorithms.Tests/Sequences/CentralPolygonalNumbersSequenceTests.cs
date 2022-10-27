using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class CentralPolygonalNumbersSequenceTests
{
    [Test]
    public void First53ElementsCorrect()
    {
        var sequence = new CentralPolygonalNumbersSequence().Sequence.Take(53);
        sequence.SequenceEqual(new BigInteger[]
            {
                1, 2, 4, 7, 11, 16, 22, 29, 37, 46, 56, 67, 79, 92, 106, 121, 137, 154, 172, 191, 211, 232, 254,
                277, 301, 326, 352, 379, 407, 436, 466, 497, 529, 562, 596, 631, 667, 704, 742, 781, 821, 862, 904,
                947, 991, 1036, 1082, 1129, 1177, 1226, 1276, 1327, 1379,
            })
            .Should().BeTrue();
    }
}
