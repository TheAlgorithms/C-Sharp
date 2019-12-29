using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences
{
    public class NaturalSequenceTests
    {
        [Test]
        public void First10ElementsCorrect()
        {
            var sequence = new NaturalSequence().Sequence;

            Assert.AreEqual(new BigInteger[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, sequence.Take(10));
        }
    }
}
