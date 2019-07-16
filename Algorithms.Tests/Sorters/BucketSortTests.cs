using Algorithms.Sorters;
using Algorithms.Tests.Helpers;
using NUnit.Framework;

namespace Algorithms.Tests.Sorters
{
    public static class BucketSortTests
    {
        [Test]
        [TestCase(new[] { 1, 2, 3, 4 }, "1234")]
        [TestCase(new[] { 17, 2, 3, 4 }, "23417")]
        [TestCase(new[] { 177, 9, 0, 15 }, "0915177")]
        [TestCase(new[] { 1, 0, 1888, 15 }, "01151888")]
        public static void BuckerSorter(int[] array, string expected)
        {
            // Arrange
            var intComparer = new IntComparer();
            var sorter = new BucketSort();

            // Act
            sorter.Sort(array, intComparer);

            // Assert
            Assert.AreEqual(expected, BucketSort.BucketToString(array));
        }
    }
}