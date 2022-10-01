using Algorithms.Numeric;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Tests.Numeric
{
    public static class AmicableNumbersTest
    {
        [Test]
        [TestCase(220, 284)]
        [TestCase(1184, 1210)]
        [TestCase(2620, 2924)]
        [TestCase(5020, 5564)]
        public static void AmicableNumbersChecker_Test(int x, int y)
        {
            // Arrange

            // Act
            var result = AmicableNumbersChecker.AreAmicableNumbers(x, y);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
