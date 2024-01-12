using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class PrimorialNumbersSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new PrimorialNumbersSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] 
            { 1, 2, 6, 30, 210, 2310, 30030, 510510, 9699690, 223092870 })
            .Should().BeTrue();
    }
}
