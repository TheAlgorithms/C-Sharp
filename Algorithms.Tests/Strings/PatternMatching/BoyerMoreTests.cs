using Algorithms.Strings;
using Algorithms.Strings.PatternMatching;
using NUnit.Framework;

namespace Algorithms.Tests.Strings;

public class BoyerMooreTests
{
    [TestCase("HelloImATestcaseAndIWillPass", "Testcase", 8)]
    [TestCase("HelloImATestcaseAndImCaseSensitive", "TestCase", -1)]
    [TestCase("Hello Im a testcase and I work with whitespaces", "testcase", 11)]
    [TestCase("Hello Im a testcase and I work with numbers like 1 2 3 4", "testcase", 11)]
    public void FindFirstOccurrence_IndexCheck(string t, string p, int expectedIndex)
    {
        var resultIndex = BoyerMoore.FindFirstOccurrence(t, p);
        Assert.That(expectedIndex, Is.EqualTo(resultIndex));
    }
}
