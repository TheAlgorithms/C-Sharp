using Algorithms.Strings;
using Algorithms.Strings.PatternMatching;
using NUnit.Framework;

namespace Algorithms.Tests.Strings;

public class ZblockSubstringSearchTest
{
    [TestCase("abc", "abcdef", 1)]
    [TestCase("xxx", "abxxxcdexxxf", 2)]
    [TestCase("aa", "waapaaxcdaalaabb", 4)]
    [TestCase("ABC", "ABAAABCDBBABCDDEBCABC", 3)]
    [TestCase("xxx", "abcdefghij", 0)]
    [TestCase("aab", "caabxaaaz", 1)]
    [TestCase("abc", "xababaxbabcdabx", 1)]
    [TestCase("GEEK", "GEEKS FOR GEEKS", 2)]
    [TestCase("ground", "Hello, playground!", 1)]
    public void Test(string pattern, string text, int expectedOccurences)
    {
        var occurencesFound = ZblockSubstringSearch.FindSubstring(pattern, text);
        Assert.That(occurencesFound, Is.EqualTo(expectedOccurences));
    }
}
