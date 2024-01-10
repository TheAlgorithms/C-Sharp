using Algorithms.Numeric;
using NUnit.Framework;

namespace Algorithms.Tests.Numeric;

public static class NarcissisticNumberTest
{
    [TestCase(2, ExpectedResult = true)]
    [TestCase(3, ExpectedResult = true)]
    [TestCase(28, ExpectedResult = false)]
    [TestCase(153, ExpectedResult = true)]
    [TestCase(170, ExpectedResult = false)]
    [TestCase(371, ExpectedResult = true)]
    public static bool NarcissisticNumberWork(int number)
    {
        // Arrange

        // Act
        var result = NarcissisticNumberChecker.IsNarcissistic(number);

        // Assert
        return result;
    }
}
