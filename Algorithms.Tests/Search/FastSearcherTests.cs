using System;
using System.Linq;
using Algorithms.Search;

using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Search
{
    public static class FastSearcherTests
    {

        [Test]
        public static void FindIndex_ItemPresent_IndexCorrect()
        {
            // Arrange
            var searcher = new FastSearcher();
            var arr = new int[100000];
            for (var i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }

            Array.Sort(arr);

            const int expectedIndex = 3;
            var x = arr[expectedIndex];
            var actualIndex = searcher.FindIndex(arr, x);

            // Assert
            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [Test]
        public static void FindIndex_ItemPresent_IndexCorrect_ArrayIsOdd()
        {
            // Arrange
            var searcher = new FastSearcher();
            var arr = new int[100001];
            for (var i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }

            Array.Sort(arr);

            const int expectedIndex = 3;
            var x = arr[expectedIndex];
            var actualIndex = searcher.FindIndex(arr, x);

            // Assert
            Assert.AreEqual(expectedIndex, actualIndex);
        }

       


        [Test]
        public static void FindIndex_ItemMissing_ItemNotFoundExceptionThrown()
        {
            // Arrange
            var searcher = new FastSearcher();
            var arr = new int[10];
            for (var i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }

            Array.Sort(arr);
            // Act
            const int x = 12;
            // Assert
            _ = Assert.Throws(typeof(ItemNotFoundException), () => searcher.FindIndex(arr, x));
        }
    }
}