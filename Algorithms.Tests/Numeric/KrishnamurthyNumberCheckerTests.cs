using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric
{
    public class KrishnamurthyNumberCheckerTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(145)]
        [TestCase(40585)]
        public void KrishnamurthyNumberCheckerKnownNumbers(int number)
        {
            var result = KrishnamurthyNumberChecker.IsKMurthyNumber(number);
            Assert.IsTrue(result);
        }

        [TestCase(3)]
        [TestCase(4)]
        [TestCase(239847)]
        [TestCase(12374)]
        public void KrishnamurthyNumberCheckerNotKMNumber(int number)
        {
            var result = KrishnamurthyNumberChecker.IsKMurthyNumber(number);
            Assert.IsFalse(result);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void KrishnamurthyNumberCheckerNotPositiveNumber(int number)
        {
            var result = KrishnamurthyNumberChecker.IsKMurthyNumber(number);
            Assert.IsFalse(result);
        }
    }
}
