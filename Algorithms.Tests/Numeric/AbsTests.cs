using Algorithms.Numeric;

namespace Algorithms.Tests.Numeric;

public static class AbsTests
{
    [TestCase(0, 0)]
    [TestCase(34, 34)]
    [TestCase(-100000000000.0d, 100000000000.0d)]
    [TestCase(-3, 3)]
    [TestCase(-3.1443123d, 3.1443123d)]
    public static void GetsAbsVal<T>(T inputNum, T expected) where T : INumber<T>
    {
        // Act
        var result = Abs.AbsVal(inputNum);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(new[] { -3, -1, 2, -11 }, -11)]
    [TestCase(new[] { 0, 5, 1, 11 }, 11)]
    [TestCase(new[] { 3.0, -10.0, -2.0 }, -10.0d)]
    public static void GetAbsMax<T>(T[] inputNums, T expected) where T : INumber<T>
    {
        // Act
        var result = Abs.AbsMax(inputNums);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public static void AbsMaxThrowsArgumentException()
    {
        // Arrange
        var inputNums = Array.Empty<int>();

        // Assert
        Assert.Throws<ArgumentException>(() => Abs.AbsMax(inputNums));
    }

    [TestCase(new[] { -3, -1, 2, -11 }, -1)]
    [TestCase(new[] { -3, -5, 1, -11 }, 1)]
    [TestCase(new[] { 0, 5, 1, 11 }, 0)]
    public static void GetAbsMin<T>(T[] inputNums, T expected) where T : INumber<T>
    {
        // Act
        var result = Abs.AbsMin(inputNums);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public static void AbsMinThrowsArgumentException()
    {
        // Arrange
        var inputNums = Array.Empty<int>();

        // Assert
        Assert.Throws<ArgumentException>(() => Abs.AbsMin(inputNums));
    }
}