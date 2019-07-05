﻿using Algorithms.Sorters;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;

namespace Algorithms.Tests.Sorters
{
    public class InsertionSorterTests
    {
        [Test]
        public void ArraySorted([Random(0, 1000, 1000)]int n)
        {
            var sorter = new InsertionSorter<int>();
            var random = new Random();
            var testArray = new int[n];
            var correctArray = new int[n];
            for (var i = 0; i < n; i++)
            {
                var t = random.Next(0, 1000);
                testArray[i] = t;
                correctArray[i] = t;
            }

            var intComparer = new IntComparer();
            sorter.Sort(testArray, intComparer);
            Array.Sort(correctArray, intComparer);

            Assert.AreEqual(testArray, correctArray);
        }

        private class IntComparer : IComparer<int>
        {
            public int Compare(int x, int y) => x.CompareTo(y);
        }
    }
}
