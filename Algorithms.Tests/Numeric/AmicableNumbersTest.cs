using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric;

public static class AmicableNumbersTest
{
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
        Assert.That(result, Is.True);
    }
}
