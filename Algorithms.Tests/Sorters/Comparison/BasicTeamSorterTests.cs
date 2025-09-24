using Algorithms.Sorters.Comparison;

namespace Algorithms.Tests.Sorters.Comparison
{
    [TestFixture]
    public class BasicTimSorterTests
    {
        private readonly BasicTimSorter<int> sorter = new(Comparer<int>.Default);

        [Test]
        public void Sort_EmptyArray_DoesNotThrow()
        {
            var array = Array.Empty<int>();
            Assert.DoesNotThrow(() => sorter.Sort(array));
            Assert.That(array, Is.Empty);
        }

        [Test]
        public void Sort_SingleElementArray_DoesNotChangeArray()
        {
            var array = new[] { 1 };
            sorter.Sort(array);
            Assert.That(array, Is.EqualTo(new[] { 1 }));
        }

        [Test]
        public void Sort_AlreadySortedArray_DoesNotChangeArray()
        {
            var array = new[] { 1, 2, 3, 4, 5 };
            sorter.Sort(array);
            Assert.That(array, Is.EqualTo(new[] { 1, 2, 3, 4, 5 }));
        }

        [Test]
        public void Sort_UnsortedArray_SortsCorrectly()
        {
            var array = new[] { 5, 3, 1, 4, 2 };
            sorter.Sort(array);
            Assert.That(array, Is.EqualTo(new[] { 1, 2, 3, 4, 5 }));
        }

        [Test]
        public void Sort_ReverseSortedArray_SortsCorrectly()
        {
            var array = new[] { 5, 4, 3, 2, 1 };
            sorter.Sort(array);
            Assert.That(array, Is.EqualTo(new[] { 1, 2, 3, 4, 5 }));
        }

        [Test]
        public void Sort_ArrayWithDuplicates_SortsCorrectly()
        {
            var array = new[] { 3, 1, 2, 3, 1, 2 };
            sorter.Sort(array);
            Assert.That(array, Is.EqualTo(new[] { 1, 1, 2, 2, 3, 3 }));
        }

        [Test]
        public void Sort_LargeArray_SortsCorrectly()
        {
            var array = new int[1000];
            for (var i = 0; i < 1000; i++)
            {
                array[i] = 1000 - i;
            }
            sorter.Sort(array);
            array.Should().BeInAscendingOrder();
        }

        [Test]
        public void Sort_LargeRandomArray_SortsCorrectly()
        {
            var array = new int[1000];
            var random = new Random();
            for (var i = 0; i < 1000; i++)
            {
                array[i] = random.Next(1, 1001);
            }
            sorter.Sort(array);
            array.Should().BeInAscendingOrder();
        }
    }
}
