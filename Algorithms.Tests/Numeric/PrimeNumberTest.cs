using Algorithms.Numeric;

namespace Algorithms.Tests.Numeric;

public static class PrimeNumberTests
{
    /// <summary>
    ///     Tests the PrimeChecker.IsPrime method with various inputs (primes, composites, edge cases)
    ///     to ensure the result is correct.
    /// </summary>
    [TestCase(-5, ExpectedResult = false)] // Negative number
    [TestCase(0, ExpectedResult = false)] // Zero
    [TestCase(1, ExpectedResult = false)] // One
    [TestCase(2, ExpectedResult = true)] // Smallest prime
    [TestCase(3, ExpectedResult = true)] // Prime
    [TestCase(4, ExpectedResult = false)] // Composite (2*2)
    [TestCase(7, ExpectedResult = true)] // Prime
    [TestCase(9, ExpectedResult = false)] // Composite (3*3)
    [TestCase(13, ExpectedResult = true)] // Prime
    [TestCase(15, ExpectedResult = false)] // Composite (3*5)
    [TestCase(25, ExpectedResult = false)] // Composite (5*5)
    [TestCase(29, ExpectedResult = true)] // Prime
    [TestCase(35, ExpectedResult = false)] // Composite (5*7)
    [TestCase(49, ExpectedResult = false)] // Composite (7*7)
    [TestCase(97, ExpectedResult = true)] // Larger prime
    [TestCase(100, ExpectedResult = false)] // Larger composite
    public static bool IsPrime_ResultIsCorrect(int number)
    {
        // Arrange is implicit here

        // Act
        var result = PrimeChecker.IsPrime(number);

        // Assert
        return result;
    }
}