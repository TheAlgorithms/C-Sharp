using Algorithms.Sorters;
using Algorithms.Tests.Helpers;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;

namespace Algorithms.Tests.Sorters
{
    public class PancakeSorterTests
    {
        [Test]
        public void ArraySorted([Random(0, 1000, 100)]int n)
        {
            // Arrange
            var sorter = new PancakeSorter<int>();
            var intComparer = new IntComparer();
            var random = new Random();
            var testArray = new int[n];
            var correctArray = new int[n];
            for (var i = 0; i < n; i++)
            {
                var t = random.Next(0, 1000);
                testArray[i] = t;
                correctArray[i] = t;
            }

            // Act
            sorter.Sort(testArray, intComparer);
            Array.Sort(correctArray, intComparer);

            // Assert
            Assert.AreEqual(testArray, correctArray);
        }
    }
}
