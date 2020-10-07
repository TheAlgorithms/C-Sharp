using Algorithms.Search.Substring;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Algorithms.Tests.Search.Substring
{
    public class BoyerMooreTests
    {
        [TestCase("HelloImATestcaseAndIWillPass", "Testcase", 8)]
        [TestCase("HelloImATestcaseAndImCaseSensitive", "TestCase", -1)]
        [TestCase("Hello Im a testcase and I work with whitespaces", "testcase", 11)]
        [TestCase("Hello Im a testcase and I work with numbers like 1 2 3 4", "testcase", 11)]
        public void FindFirstOccurrence_IndexCheck(string t, string p, int expectedIndex)
        {
            int resultIndex = BoyerMoore.FindFirstOccurrence(t, p);
            Assert.AreEqual(resultIndex, expectedIndex);
        }
    }
}