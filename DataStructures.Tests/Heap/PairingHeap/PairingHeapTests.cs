using System;
using System.Linq;
using DataStructures.Heap.PairingHeap;
using NUnit.Framework;

namespace DataStructures.Tests.Heap.PairingHeap
{
    internal class PairingHeapTests
    {
        [Test]
        public void BuildMinHeap_CreateHeap_HeapIsCheked()
        {
            var nodeCount = 1000 * 10;
            var minHeap = new PairingHeap<int>();
            for (var i = 0; i <= nodeCount; i++)
            {
                minHeap.Insert(i);
            }

            for (var i = 0; i <= nodeCount; i++)
            {
                minHeap.UpdateKey(i, i - 1);
            }

            var min = 0;
            for (var i = 0; i <= nodeCount; i++)
            {
                min = minHeap.Extract();
                Assert.AreEqual(min, i - 1);
            }

            Assert.AreEqual(minHeap.Count, minHeap.Count);

            var rnd = new Random();
            var testSeries = Enumerable.Range(0, nodeCount - 1).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                minHeap.Insert(item);
            }

            for (var i = 0; i < testSeries.Count; i++)
            {
                var decremented = testSeries[i] - rnd.Next(0, 1000);
                minHeap.UpdateKey(testSeries[i], decremented);
                testSeries[i] = decremented;
            }

            testSeries.Sort();

            for (var i = 0; i < nodeCount - 2; i++)
            {
                min = minHeap.Extract();
                Assert.AreEqual(testSeries[i], min);
            }

            Assert.AreEqual(minHeap.Count, minHeap.Count);
        }

        [Test]
        public void BuildMaxHeap_CreateHeap_HeapIsCheked()
        {
            var nodeCount = 1000 * 10;
            var maxHeap = new PairingHeap<int>(Sorting.Descending);
            for (var i = 0; i <= nodeCount; i++)
            {
                maxHeap.Insert(i);
            }

            for (var i = 0; i <= nodeCount; i++)
            {
                maxHeap.UpdateKey(i, i + 1);
            }

            Assert.AreEqual(maxHeap.Count, maxHeap.Count);

            var max = 0;
            for (var i = nodeCount; i >= 0; i--)
            {
                max = maxHeap.Extract();
                Assert.AreEqual(max, i + 1);
            }

            var rnd = new Random();
            var testSeries = Enumerable.Range(0, nodeCount - 1).OrderBy(x => rnd.Next()).ToList();

            foreach (var item in testSeries)
            {
                maxHeap.Insert(item);
            }
            for (var i = 0; i < testSeries.Count; i++)
            {
                var incremented = testSeries[i] + rnd.Next(0, 1000);
                maxHeap.UpdateKey(testSeries[i], incremented);
                testSeries[i] = incremented;
            }

            testSeries = testSeries.OrderByDescending(x => x).ToList();
            for (var i = 0; i < nodeCount - 2; i++)
            {
                max = maxHeap.Extract();
                Assert.AreEqual(testSeries[i], max);
            }

            Assert.AreEqual(maxHeap.Count, maxHeap.Count);
        }
    }
}
