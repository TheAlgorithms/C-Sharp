using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.SortedList
{
    internal class IntComparer : IComparer<int>
    {
        public int Compare(int x, int y) => x.CompareTo(y);
    }
}
