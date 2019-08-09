using System.Linq;
using Algorithms.Knapsack;
using NUnit.Framework;

namespace Algorithms.Tests.Knapsack
{
    public static class DPKnapsackSolverTests
    {

        [Test]
        public static void SmallSample()
        {
            //Arrange
            var items = new int[] { 0, 1, 2 };
            var val = new int[] { 50, 100, 130 };
            var wt = new int[] { 10, 20, 40 };
            int capacity = 50;
            var expected = new int[] { 0, 2 };

            //Act
            var solver = new DPKnapsackSolver<int>();
            var actual = solver.Solve(items, capacity, x => wt[x], y => val[y]);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void TakesHalf([Random(0, 1000, 100, Distinct = true)]int length)
        {
            //Arrange
            var solver = new DPKnapsackSolver<int>();
            var items = Enumerable.Repeat(42, 2 * length).ToArray();
            var expectedResult = Enumerable.Repeat(42, length);

            //Act
            var result = solver.Solve(items, length, x => 1, y => 1);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
