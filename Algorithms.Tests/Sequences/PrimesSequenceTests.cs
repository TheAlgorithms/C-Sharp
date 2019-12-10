using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences
{
    public class PrimesSequenceTests
    {
        [Test]
        public void First10ElementsCorrect()
        {
            var sequence = new PrimesSequence().Sequence;

            Assert.AreEqual(new BigInteger[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 }, sequence.Take(10));
        }
    }
}
