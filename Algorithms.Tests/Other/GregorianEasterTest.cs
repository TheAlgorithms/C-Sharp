using System;
using Algorithms.Other;
using NUnit.Framework;

namespace Algorithms.Tests.Other
{
    public static class GregorianEasterTest
    {
        [Test]
        [TestCase(1818, "22/03/1818")]
        [TestCase(1991, "31/03/1991")]
        [TestCase(1954, "18/04/1954")]
        [TestCase(2000, "23/04/2000")]
        public static void CalculateTest(int year, string expected)
        {
            DateTime result = GregorianEaster.Calculate(year);

            Assert.AreEqual(result, Convert.ToDateTime(expected));
        }

        [Test]
        [TestCase(100)]
        public static void CalculateInvalidYearTest(int year)
        {
            Assert.Throws(Is.TypeOf<ArgumentException>().And.Message.EqualTo("Invalid year"), delegate { GregorianEaster.Calculate(year); });
        }
    }
}
