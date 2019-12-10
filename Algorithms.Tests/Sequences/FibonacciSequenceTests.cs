using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences
{
    public class FibonacciSequenceTests
    {
        [Test]
        public void First10ElementsCorrect()
        {
            var sequence = new FibonacciSequence().Sequence;

            Assert.AreEqual(new BigInteger[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34 }, sequence.Take(10));
        }
    }
}
