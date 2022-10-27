using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class CakeNumbersSequenceTests
{
    [Test]
    public void First46ElementsCorrect()
    {
        var sequence = new CakeNumbersSequence().Sequence.Take(46);
        sequence.SequenceEqual(new BigInteger[]
            {
                1, 2, 4, 8, 15, 26, 42, 64, 93, 130,
                176, 232, 299, 378, 470, 576, 697, 834, 988, 1160,
                1351, 1562, 1794, 2048, 2325, 2626, 2952, 3304, 3683, 4090,
                4526, 4992, 5489, 6018, 6580, 7176, 7807, 8474, 9178, 9920,
                10701, 11522, 12384, 13288, 14235, 15226
            })
            .Should().BeTrue();
    }
}
