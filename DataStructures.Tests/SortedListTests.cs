using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DataStructures.Tests
{
    [TestFixture]
    public class SortedListTests
    {
        [Test]
        public void Add_AddMultipleValues_SortingCorrectly(
            [Random(1, 1000, 100, Distinct = true)]
            int count)
        {
            var values = GetValues(count);
            var list = new SortedList<int>();

            foreach (var value in values)
            {
                list.Add(value);
            }

            CollectionAssert.AreEqual(values.OrderBy(i => i), list);
        }

        [Test]
        public void Contains_PositiveArrayAdded_NegativeNumberAsked_FalseReturned(
            [Random(1, 200, 10, Distinct = true)] int count)
        {
            var values = GetValues(count);
            const int value = -1;

            var list = new SortedList<int>();

            foreach (var i in values)
            {
                list.Add(i);
            }

            Assert.IsFalse(list.Contains(value));
        }

        [Test]
        public void Contains_PositiveArrayAdded_ContainingValueAsked_TrueReturned(
            [Random(1, 200, 10, Distinct = true)] int count)
        {
            var values = GetValues(count);
            var value = values[TestContext.CurrentContext.Random.Next(count - 1)];

            var list = new SortedList<int>();

            foreach (var i in values)
            {
                list.Add(i);
            }

            Assert.IsTrue(list.Contains(value));
        }


        [Test]
        public void Remove_PositiveArrayAdded_NegativeNumberAsked_FalseReturned(
            [Random(1, 200, 10, Distinct = true)] int count)
        {
            var values = GetValues(count);
            const int value = -1;

            var list = new SortedList<int>();

            foreach (var i in values)
            {
                list.Add(i);
            }

            Assert.IsFalse(list.TryRemove(value));
        }

        [Test]
        public void Remove_PositiveArrayAdded_ContainingValueAsked_TrueReturned(
            [Random(1, 200, 10, Distinct = true)] int count)
        {
            var values = GetValues(count);
            var value = values[TestContext.CurrentContext.Random.Next(count - 1)];

            var list = new SortedList<int>();

            foreach (var i in values)
            {
                list.Add(i);
            }

            var expectingValues = values
                .OrderBy(i => i)
                .ToList();

            expectingValues.Remove(value);

            Assert.IsTrue(list.TryRemove(value));
            CollectionAssert.AreEqual(expectingValues, list);
        }

        [Test]
        public void Clear_ArrayAdded_ListCleaned_ListIsEmpty(
            [Random(1, 20, 1, Distinct = true)] int count)
        {
            var values = GetValues(count);

            var list = new SortedList<int>();

            foreach (var i in values)
            {
                list.Add(i);
            }

            list.Clear();

            CollectionAssert.IsEmpty(list);
        }

        private static List<int> GetValues(int count)
            => Enumerable
                .Range(0, count)
                .Select(_ => TestContext.CurrentContext.Random.Next(1_000_000))
                .ToList();
    }
}
