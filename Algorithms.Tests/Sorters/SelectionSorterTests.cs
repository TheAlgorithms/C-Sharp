using System;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Algorithms.Sorters;
using System.Collections.Generic;

namespace Algorithms.Tests.Sorters
{
    public class SelectionSorterTests
    {
        readonly SelectionSorter<int> sorter = new SelectionSorter<int>();
        readonly Random random = new Random();

        [Test]
        [Parallelizable]
        public void ArraySorted([Random(0, 100, 10)]int n)
        {
            var testArray = new int[n];
            var correctArray = new int[n];
            for (var i = 0; i < n; i++)
            {
                var t = random.Next(0, 1000);
                testArray[i] = t;
                correctArray[i] = t;
            }

            sorter.Sort(testArray, new IntComparer());
            Array.Sort(correctArray);

            Assert.AreEqual(testArray, correctArray);
        }

        private class IntComparer : IComparer<int>
        {
            public int Compare(int x, int y) => x.CompareTo(y);
        }
    }
}
