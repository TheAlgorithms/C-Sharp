using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

[TestFixture]
public class OnesCountingSequenceTest
{
    [Test]
    public void Count105()
    {
        var values = new BigInteger[] {
                                          0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4, 1, 2, 2, 3, 2, 3, 3, 4,
                                          2, 3, 3, 4, 3, 4, 4, 5, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 2,
                                          3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3,
                                          3, 4, 3, 4, 4, 5, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 2, 3, 3,
                                          4, 3, 4, 4, 5, 3,
                                      };
        var sequence = new OnesCountingSequence().Sequence.Take(values.Length);
    }
}
