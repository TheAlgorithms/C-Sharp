using System;
using System.Linq;
using Algorithms.Search;

using NUnit.Framework;
using NUnit.Framework.Internal;
using Utility.Exception;

namespace Algorithms.Tests.Search
{
    public static class FastSearcherTests
    {
        [Test]
        public static void FindIndex_ItemPresent_IndexCorrect()
        {
            // Arrange
            var searcher = new FastSearcher();
            var arr = new int[10];
            for (var i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }

            Array.Sort(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                int expectedIndex = i;
                var x = arr[expectedIndex];
                var actualIndex = searcher.FindIndex(arr, x);

                // Assert
                Assert.AreEqual(expectedIndex, actualIndex);
            }
        }

        [Test]
        public static void FindIndex_ItemPresent_IndexCorrect_ArrayIsOdd()
        {
            // Arrange
            var searcher = new FastSearcher();
            var arr = new int[11];
            for (var i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }

            Array.Sort(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                int expectedIndex = i;
                var x = arr[expectedIndex];
                var actualIndex = searcher.FindIndex(arr, x);

                // Assert
                Assert.AreEqual(expectedIndex, actualIndex);
            }
           
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
            _ = Assert.Throws<ItemNotFoundException>(() => searcher.FindIndex(arr, x));
        }
    }
}
