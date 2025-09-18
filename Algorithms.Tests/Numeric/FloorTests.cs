using Algorithms.Numeric;

namespace Algorithms.Tests.Numeric;

public static class FloorTests
{
    [TestCase(0.0, 0)]
    [TestCase(1.1, 1)]
    [TestCase(1.9, 1)]
    [TestCase(1.0, 1)]
    [TestCase(-1.1, -2)]
    [TestCase(-1.9, -2)]
    [TestCase(-1.0, -1)]
    [TestCase(1000000000.1, 1000000000)]
    [TestCase(1, 1)]
    public static void GetsFloorVal<T>(T inputNum, T expected) where T : INumber<T>
    {
        // Act
        var result = Floor.FloorVal(inputNum);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}