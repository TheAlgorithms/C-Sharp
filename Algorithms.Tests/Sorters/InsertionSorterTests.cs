using System;
using Algorithms.Sorters;
using Algorithms.Tests.Helpers;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Sorters
{
    public class InsertionSorterTests
    {
        [Test]
        public void ArraySorted([Random(0, 1000, 1000)]int n)
        {
            // Arrange
            var sorter = new InsertionSorter<int>();
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
