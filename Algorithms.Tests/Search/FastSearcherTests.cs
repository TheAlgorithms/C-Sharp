using Algorithms.Search;
using NUnit.Framework;
using Utilities.Exceptions;

namespace Algorithms.Tests.Search
{
    public static class FastSearcherTests
    {
        [Test]
        public static void FindIndex_ItemPresent_IndexCorrect()
        {
            var searcher = new FastSearcher();
            var arr = Helper.GetSortedArray(1000);
            var present = Helper.GetItemIn(arr);
            var index = searcher.FindIndex(arr, present);
            Assert.AreEqual(present, arr[index]);
        }

        [Test]
        public static void FindIndex_ItemMissing_ItemNotFoundExceptionThrown()
        {
            var searcher = new FastSearcher();
            var arr = Helper.GetSortedArray(1000);
            var missing = Helper.GetItemNotIn(arr);
            _ = Assert.Throws<ItemNotFoundException>(() => searcher.FindIndex(arr, missing));
        }

        [Test]
        public static void FindIndex_ItemSmallerThanAllMissing_ItemNotFoundExceptionThrown()
        {
            var searcher = new FastSearcher();
            var arr = Helper.GetSortedArray(1000);
            var missing = Helper.GetItemSmallerThanAllIn(arr);
            _ = Assert.Throws<ItemNotFoundException>(() => searcher.FindIndex(arr, missing));
        }

        [Test]
        public static void FindIndex_ItemBiggerThanAllMissing_ItemNotFoundExceptionThrown()
        {
            var searcher = new FastSearcher();
            var arr = Helper.GetSortedArray(1000);
            var missing = Helper.GetItemBiggerThanAllIn(arr);
            _ = Assert.Throws<ItemNotFoundException>(() => searcher.FindIndex(arr, missing));
        }
    }
}
