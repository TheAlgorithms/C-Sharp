using Algorithms.Strings;
using NUnit.Framework;

namespace Algorithms.Tests.Strings
{
    public class ZblockSubstringSearchTest
    {
        [Test]
        [TestCase("abc", "abcdef", 1)]
        [TestCase("xxx", "abxxxcdexxxf", 2)]
        [TestCase("aa", "waapaaxcdaalaa", 4)]
        [TestCase("ABC", "ABAAABCDBBABCDDEBCABC", 3)]
        [TestCase("xxx", "abcdefghij", 0)]
        public void Test(string pattern, string text, int expectedOccurences)
        {
            var occurencesFound = ZblockSubstringSearch.FindSubstring(pattern, text);
            Assert.AreEqual(expectedOccurences, occurencesFound);
        }
    }
}
