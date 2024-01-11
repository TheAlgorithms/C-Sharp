using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences;

public class KummerNumbersSequenceTests
{
    [Test]
    public void First10ElementsCorrect()
    {
        var sequence = new KummerNumbersSequence().Sequence.Take(10);
        sequence.SequenceEqual(new BigInteger[] 
            { 1, 5, 29, 209, 2309, 30029, 510509, 9699689, 223092869, 6469693229 })
            .Should().BeTrue();
    }
}
