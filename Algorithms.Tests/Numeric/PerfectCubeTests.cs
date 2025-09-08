using Algorithms.Numeric;

namespace Algorithms.Tests.Numeric;

public static class PerfectCubeTests
{
    [TestCase(-27, ExpectedResult = true)]
    [TestCase(27, ExpectedResult = true)]
    [TestCase(4, ExpectedResult = false)]
    [TestCase(64, ExpectedResult = true)]
    [TestCase(0, ExpectedResult = true)]
    [TestCase(1, ExpectedResult = true)]
    [TestCase(8, ExpectedResult = true)]
    [TestCase(9, ExpectedResult = false)]
    public static bool IsPerfectCube_ResultIsCorrect(int number)
    {
        // Act
        var result = PerfectCubeChecker.IsPerfectCube(number);

        // Assert
        return result;
    }

    [TestCase(-27, ExpectedResult = true)]
    [TestCase(27, ExpectedResult = true)]
    [TestCase(4, ExpectedResult = false)]
    [TestCase(64, ExpectedResult = true)]
    [TestCase(0, ExpectedResult = true)]
    [TestCase(1, ExpectedResult = true)]
    [TestCase(8, ExpectedResult = true)]
    [TestCase(9, ExpectedResult = false)]
    public static bool IsPerfectCubeBinarySearch_ResultIsCorrect(int number)
    {
        // Act
        var result = PerfectCubeChecker.IsPerfectCubeBinarySearch(number);

        // Assert
        return result;
    }
}
