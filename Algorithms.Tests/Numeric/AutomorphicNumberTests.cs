using Algorithms.Numeric;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Tests.Numeric
{
    public class AutomorphicNumberTests
    {
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(25)]
        [TestCase(76)]
        [TestCase(376)]
        [TestCase(625)]
        [TestCase(9376)]
        [TestCase(90625)]
        [TestCase(109376)]
        [TestCase(890625)]
        [TestCase(2890625)]
        [TestCase(7109376)]
        [TestCase(12890625)]
        [TestCase(87109376)]
        public void TestAutomorphicNumbers(int number)
        {
            Assert.That(AutomorphicNumber.IsAutomorphic(number), Is.True);
        }

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(7)]
        [TestCase(18)]
        [TestCase(79)]
        [TestCase(356)]
        [TestCase(623)]
        [TestCase(9876)]
        [TestCase(90635)]
        [TestCase(119376)]
        [TestCase(891625)]
        [TestCase(2990625)]
        [TestCase(7209376)]
        [TestCase(12891625)]
        [TestCase(87129396)]
        public void TestNonAutomorphicNumbers(int number)
        {
            Assert.That(AutomorphicNumber.IsAutomorphic(number), Is.False);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void TestInvalidAutomorphicNumbers(int number)
        {
            Assert.Throws(Is.TypeOf<ArgumentException>()
                .And.Message.EqualTo($"An automorphic number must always be positive: Actual value {number}"),
                delegate
                {
                    AutomorphicNumber.IsAutomorphic(number);
                });
        }

        [TestCase(1, 100)]
        public void TestAutomorphicNumberSequence(int lower, int upper)
        {
            List<long> automorphicList = new() { 1, 5, 6, 25, 76 };
            Assert.That(AutomorphicNumber.GetAutomorphicNumbers(lower, upper), Is.EqualTo(automorphicList));
        }

        [TestCase(25,25)]
        public void TestAutomorphicNumberSequenceSameBounds(int lower, int upper)
        {
            List<long> automorphicList = new() { 25 };
            Assert.That(AutomorphicNumber.GetAutomorphicNumbers(lower, upper), Is.EqualTo(automorphicList));
        }

        [TestCase(-1,1)]
        [TestCase(0, 1)]
        public void TestAutomorphicNumberSequenceInvalidLowerBound(int lower, int upper)
        {
            Assert.Throws(Is.TypeOf<ArgumentException>()
                .And.Message.EqualTo($"Lower Bound must be greater than 0: Actual value {lower}"),
                delegate
                {
                    AutomorphicNumber.GetAutomorphicNumbers(lower, upper);
                });
        }

        [TestCase(1, -1)]
        [TestCase(10, -1)]
        public void TestAutomorphicNumberSequenceInvalidUpperBound(int lower, int upper)
        {
            Assert.Throws(Is.TypeOf<ArgumentException>()
                .And.Message.EqualTo($"Upper Bound must be greater than 0: Actual value {upper}"),
                delegate
                {
                    AutomorphicNumber.GetAutomorphicNumbers(lower, upper);
                });
        }

        [TestCase(25, 2)]
        public void TestAutomorphicNumberSequenceFlipLowerAndUpperBounds(int lower, int upper)
        {
            List<long> automorphicList = new() { 5, 6, 25 };
            Assert.That(AutomorphicNumber.GetAutomorphicNumbers(lower, upper), Is.EqualTo(automorphicList));
        }
    }
}
