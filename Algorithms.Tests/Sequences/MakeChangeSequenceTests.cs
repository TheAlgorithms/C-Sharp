using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class MakeChangeSequenceTests
{
    [Test]
    public void First100ElementsCorrect()
    {
        // Values from https://oeis.org/A000008/b000008.txt
        var test = new BigInteger[]
                   {
                       1,       1,    2,    2,    3,    4,    5,    6,    7,    8,
                       11,     12,   15,   16,   19,   22,   25,   28,   31,   34,
                       40,     43,   49,   52,   58,   64,   70,   76,   82,   88,
                       98,    104,  114,  120,  130,  140,  150,  160,  170,  180,
                       195,   205,  220,  230,  245,  260,  275,  290,  305,  320,
                       341,   356,  377,  392,  413,  434,  455,  476,  497,  518,
                       546,   567,  595,  616,  644,  672,  700,  728,  756,  784,
                       820,   848,  884,  912,  948,  984, 1020, 1056, 1092, 1128,
                       1173, 1209, 1254, 1290, 1335, 1380, 1425, 1470, 1515, 1560,
                       1615, 1660, 1715, 1760, 1815, 1870, 1925, 1980, 2035, 2090,
                   };

        var sequence = new MakeChangeSequence().Sequence.Take(test.Length);
        sequence.SequenceEqual(test).Should().BeTrue();
    }
}
