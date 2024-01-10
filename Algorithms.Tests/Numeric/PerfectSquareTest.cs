using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric;

public static class PerfectSquareTests
{
    [TestCase(-4, ExpectedResult = false)]
    [TestCase(4, ExpectedResult = true)]
    [TestCase(9, ExpectedResult = true)]
    [TestCase(10, ExpectedResult = false)]
    [TestCase(16, ExpectedResult = true)]
    [TestCase(70, ExpectedResult = false)]
    [TestCase(81, ExpectedResult = true)]
    public static bool IsPerfectSquare_ResultIsCorrect(int number)
    {
        // Arrange

        // Act
        var result = PerfectSquareChecker.IsPerfectSquare(number);

        // Assert
        return result;
    }
}
