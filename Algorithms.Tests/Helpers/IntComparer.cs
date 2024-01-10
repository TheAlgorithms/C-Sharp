using System.Collections.Generic;

namespace Algorithms.Tests.Helpers;

internal class IntComparer : IComparer<int>
{
    public int Compare(int x, int y) => x.CompareTo(y);
}
