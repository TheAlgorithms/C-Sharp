using Algorithms.Numeric;

namespace Algorithms.Tests.Numeric;

public static class AdditionWithoutArithmeticTests
{
    [TestCase(3, 5, 8)]
    [TestCase(13, 5, 18)]
    [TestCase(-7, 2, -5)]
    [TestCase(0, -7, -7)]
    [TestCase(-321, 0, -321)]
    public static void CalculateAdditionWithoutArithmetic_Test(int first, int second, int expectedResult)
    {
        // Act
        var result = AdditionWithoutArithmetic.CalculateAdditionWithoutArithmetic(first, second);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
