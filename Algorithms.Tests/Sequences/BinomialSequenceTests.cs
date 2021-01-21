using System.Linq;
using System.Numerics;
using Algorithms.Sequences;
using NUnit.Framework;

namespace Algorithms.Tests.Sequences
{
    public class BinomialSequenceTests
    {
        [Test]
        public void First4RowsCorrect()
        {
            var sequence = new BinomialSequence(4).Sequence.ToArray();
            Assert.AreEqual(new BigInteger[] { 1, 1, 1, 1, 2, 1, 1, 3, 3, 1 }, sequence);
        }
    }
}