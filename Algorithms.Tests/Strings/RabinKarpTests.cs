using System.Collections.Generic;
using Algorithms.Strings;
using NUnit.Framework;

namespace Algorithms.Tests.Strings
{
    public class RabinKarpTest
    {
        [TestCase("HelloImATestcaseAndIWillPass", "Testcase", new [] {8})]
        [TestCase("HelloImATestcaseAndImCaseSensitiv", "TestCase", new int[] {})]
        [TestCase("Hello Im a testcase and you can use whitespaces", "testcase", new [] {11})]
        [TestCase("Hello Im a testcase and you can use numbers like 1, 2, 3, etcetera", "etcetera", new [] {58})]
        [TestCase("HelloImATestcaseAndIHaveTwoOccurrencesOfTestcase", "Testcase", new [] {8, 40})]
        public void FindAllOccurrences_IndexCheck(string t, string p, int[] expectedIndices)
        {
            List<int> result = RabinKarp.FindAllOccurrences(t, p);
            Assert.AreEqual(result, new List<int>(expectedIndices));
        }
    }
}