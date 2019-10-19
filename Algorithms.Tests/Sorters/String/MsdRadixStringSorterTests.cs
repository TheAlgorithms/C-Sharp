using System;
using Algorithms.Sorters.String;
using Algorithms.Tests.Helpers;
using NUnit.Framework;

namespace Algorithms.Tests.Sorters.String
{
    /// <summary>
    /// Class for testing MSD radix sorter algorithm.
    /// </summary>
    public static class MsdRadixStringSorterTests
    {
        [Test]
        public static void ArraySorted([Random(2, 1000, 100, Distinct = true)]int n)
        {
            // Arrange 
            var sorter = new MsdRadixStringSorter();
            var (correctArray, testArray) = RandomHelper.GetStringArrays(n, 100, false);

            // Act
            sorter.Sort(testArray);
            Array.Sort(correctArray);

            // Assert
            Assert.AreEqual(correctArray, testArray);
        }
    }
}
