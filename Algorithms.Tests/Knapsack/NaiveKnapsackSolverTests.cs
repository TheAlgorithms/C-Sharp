using System.Linq;
using Algorithms.Knapsack;
using NUnit.Framework;

namespace Algorithms.Tests.Knapsack;

public static class NaiveKnapsackSolverTests
{
    [Test]
    public static void TakesHalf(
        [Random(0, 1000, 100, Distinct = true)]
        int length)
    {
        //Arrange
        var solver = new NaiveKnapsackSolver<int>();
        var items = Enumerable.Repeat(42, 2 * length).ToArray();
        var expectedResult = Enumerable.Repeat(42, length);

        //Act
        var result = solver.Solve(items, length, _ => 1, _ => 1);

        //Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}
