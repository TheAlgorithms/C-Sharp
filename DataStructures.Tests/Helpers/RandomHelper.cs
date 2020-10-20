using System.Collections.Generic;
using NUnit.Framework;

namespace DataStructures.Helpers
{
    internal static class RandomHelper
    {
        public static (List<int> correctList, List<int> testList) GetLists(int n)
        {
            var testList = new List<int>(n);
            var correctList = new List<int>(n);

            for (var i = 0; i < n; i++)
            {
                var t = TestContext.CurrentContext.Random.Next(1_000_000);
                testList.Add(t);
                correctList.Add(t);
            }

            return (correctList, testList);
        }
    }
}
