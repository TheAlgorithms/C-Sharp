using NUnit.Framework;

namespace Algorithms.Tests.Helpers
{
    internal static class RandomHelper
    {
        public static (int[] correctArray, int[] testArray) GetArrays(int n)
        {
            var testArray = new int[n];
            var correctArray = new int[n];
            for (var i = 0; i < n; i++)
            {
                var t = TestContext.CurrentContext.Random.Next(0, 1000);
                testArray[i] = t;
                correctArray[i] = t;
            }
            return (correctArray, testArray);
        }
    }
}
