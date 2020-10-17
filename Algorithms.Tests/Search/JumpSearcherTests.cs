using System.Linq;

using Algorithms.Searches;

using NUnit.Framework;

using Utilities.Exceptions;

namespace Algorithms.Tests.Search
{
    [TestFixture]
    public static class JumpSearcherTests
    {
        [Test]
        public static void FindIndex_ItemPresent_IndexCorrect()
        {
            var searcher = new JumpSearcher();
            var arr = Helper.GetSortedArray(1000);
            var present = Helper.GetItemIn(arr);
            var index = searcher.FindIndex(arr.ToList<int>(), present, 500);
            Assert.AreEqual(present, arr[index]);
        }

        [Test]
        public static void FindIndex_ItemPresent_IndexLessThanZero()
        {
            var searcher = new JumpSearcher();
            var arr = Helper.GetSortedArray(1000);
            var present = Helper.GetItemIn(arr);
            var index = searcher.FindIndex(arr.ToList<int>(), present, -2);
            Assert.AreEqual(present, arr[index]);
        }

        [Test]
        public static void FindIndex_ItemPresent_IndexGreaterThanListLength()
        {
            var searcher = new JumpSearcher();
            var arr = Helper.GetSortedArray(1000);
            var present = Helper.GetItemIn(arr);
            var index = searcher.FindIndex(arr.ToList<int>(), present, 1001);
            Assert.AreEqual(present, arr[index]);
        }

        [Test]
        public static void FindIndex_ItemNotFound()
        {
            var searcher = new JumpSearcher();
            var arr = Helper.GetSortedArray(1000);
            var present = arr[arr.Length - 1] + 1;
            var index = searcher.FindIndex(arr.ToList<int>(), present, 100);
            Assert.AreEqual(present, arr[index]);
        }
    }
}