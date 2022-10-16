using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

[TestFixture]
public class TetrahedralSequenceTests
{
    private static readonly BigInteger[] TestList = {
                                                        0, 1, 4, 10, 20, 35, 56, 84, 120, 165, 220, 286, 364, 455,
                                                        560, 680, 816, 969, 1140, 1330, 1540, 1771, 2024, 2300,
                                                        2600, 2925, 3276, 3654, 4060, 4495, 4960, 5456, 5984, 6545,
                                                        7140, 7770, 8436, 9139, 9880, 10660, 11480, 12341, 13244,
                                                        14190, 15180,
                                                    };

    /// <summary>
    ///     This test uses the list values provided from http://oeis.org/A000292/list.
    /// </summary>
    [Test]
    public void TestOeisList()
    {
        var sequence = new TetrahedralSequence().Sequence.Take(TestList.Length);
        sequence.SequenceEqual(TestList).Should().BeTrue();

    }
}
