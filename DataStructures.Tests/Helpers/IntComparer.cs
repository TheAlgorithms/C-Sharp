using System.Collections.Generic;

namespace DataStructures.Tests
{
    internal class IntComparer : IComparer<int>
    {       
        public int Compare(int x, int y) => x.CompareTo(y);        
    }
}