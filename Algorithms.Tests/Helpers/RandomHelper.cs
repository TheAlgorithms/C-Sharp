using NUnit.Framework;

namespace Algorithms.Tests.Helpers
{
    internal static class RandomHelper
    {
        public static (int[] correctArray, int[] testArray) GetArrays(int n)
        {
            var testArr = new int[n];
            var correctArray = new int[n];

            for (var i = 0; i < n; i++)
            {
                var t = TestContext.CurrentContext.Random.Next(1_000_000);
                testArr[i] = t;
                correctArray[i] = t;
            }

            return (correctArray, testArr);
        }

        public static (string[] correctArray, string[] testArray) GetStringArrays(int n, int maxLength, bool equalLength)
        {
            var testArr = new string[n];
            var correctArray = new string[n];
            var length = TestContext.CurrentContext.Random.Next(2, maxLength);

            for (var i = 0; i < n; i++)
            {
                if (!equalLength)
                {
                    length = TestContext.CurrentContext.Random.Next(2, maxLength);
                }

                var chars = new char[length];
                for (var j = 0; j < length; j++)
                {
                    chars[j] = (char)TestContext.CurrentContext.Random.Next(97, 123);
                }

                var t = new string(chars);
                testArr[i] = t;
                correctArray[i] = t;
            }

            return (correctArray, testArr);
        }
    }
}
