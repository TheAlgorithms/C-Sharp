using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences
{
    class NumberOfPrimesSequenceTests
    {
        [Test]
        public void First5ElementsCorrect()
        {
            var sequence = new NumberOfPrimesSequence().Sequence.Take(5);
            sequence.SequenceEqual(new BigInteger[] { 0, 4, 25, 168, 1229 })
                .Should().BeTrue();
        }
    }
}
