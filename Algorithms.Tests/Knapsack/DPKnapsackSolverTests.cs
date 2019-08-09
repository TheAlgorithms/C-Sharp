using System;
using System.Linq;
using Algorithms.Knapsack;
using NUnit.Framework;

namespace Algorithms.Tests.Knapsack
{
    public static class DPKnapsackSolverTests
    {

        [Test]
        public static void SmallSampleOfChar()
        {
            //Arrange
            var items = new char[] { 'A', 'B', 'C' };
            var val = new int[] { 50, 100, 130 };
            var wt = new int[] { 10, 20, 40 };

            int capacity = 50;

            Func<char, double> weightSelector = x => wt[Array.IndexOf(items, x)];
            Func<char, double> valueSelector = x => val[Array.IndexOf(items, x)];

            var expected = new char[] { 'A', 'C' };


            //Act
            var solver = new DPKnapsackSolver<char>();
            var actual = solver.Solve(items, capacity, weightSelector, valueSelector);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void FSU_P01()
        {
            // Data from https://people.sc.fsu.edu/~jburkardt/datasets/knapsack_01/knapsack_01.html

            //Arrange
            var items = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
            var val = new int[] { 92, 57, 49, 68, 60, 43, 67, 84, 87, 72 };
            var wt = new int[] { 23, 31, 29, 44, 53, 38, 63, 85, 89, 82 };

            int capacity = 165;

            Func<char, double> weightSelector = x => wt[Array.IndexOf(items, x)];
            Func<char, double> valueSelector = x => val[Array.IndexOf(items, x)];

            var expected = new char[] { 'A', 'B', 'C', 'D', 'F' };


            //Act
            var solver = new DPKnapsackSolver<char>();
            var actual = solver.Solve(items, capacity, weightSelector, valueSelector);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public static void FSU_P07()
        {
            // Data from https://people.sc.fsu.edu/~jburkardt/datasets/knapsack_01/knapsack_01.html

            //Arrange
            var val = new int[] { 135, 139, 149, 150, 156, 163, 173, 184, 192, 201, 210, 214, 221, 229, 240 };
            var wt = new int[] { 70, 73, 77, 80, 82, 87, 90, 94, 98, 106, 110, 113, 115, 118, 120 };
            var items = Enumerable.Range(1, val.Count()).ToArray();

            int capacity = 750;

            Func<int, double> weightSelector = x => wt[Array.IndexOf(items, x)];
            Func<int, double> valueSelector = x => val[Array.IndexOf(items, x)];

            var expected = new int[] { 1, 3, 5, 7, 8, 9, 14, 15 };


            //Act
            var solver = new DPKnapsackSolver<int>();
            var actual = solver.Solve(items, capacity, weightSelector, valueSelector);

            //Assert
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public static void FSU_P07_AsDouble()
        {
            // Data from https://people.sc.fsu.edu/~jburkardt/datasets/knapsack_01/knapsack_01.html

            //Arrange
            var val = new double[] { 13.5, 13.9, 14.9, 15.0, 15.6, 16.3, 17.3, 18.4, 19.2, 20.1, 21.0, 21.4, 22.1, 22.9, 24.0 };
            var wt = new double[] { 7.0, 7.3, 7.7, 8.0, 8.2, 8.7, 9.0, 9.4, 9.8, 10.6, 11.0, 11.3, 11.5, 11.8, 12.0 };
            var items = Enumerable.Range(1, val.Count()).ToArray();

            double capacity = 75.0;

            Func<int, double> weightSelector = x => wt[Array.IndexOf(items, x)];
            Func<int, double> valueSelector = x => val[Array.IndexOf(items, x)];

            var expected = new int[] { 1, 3, 5, 7, 8, 9, 14, 15 };


            //Act
            var solver = new DPKnapsackSolver<int>();
            var actual = solver.Solve(items, capacity, weightSelector, valueSelector);

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
