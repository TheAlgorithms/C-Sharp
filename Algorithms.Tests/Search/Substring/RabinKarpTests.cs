using System;
using System.Collections.Generic;
using Algorithms.Search.Substring;
using NUnit.Framework;

namespace Algorithms.Tests.Search.Substring
{
    public class RabinKarpTest
    {

        [TestCase("HelloImATestcaseAndIWillPass", "Testcase", new int[] {8})]
        [TestCase("HelloImATestcaseAndImCaseSensitiv", "TestCase", new int[] {})]
        [TestCase("HelloImATestcaseAndIHaveTwoOccurrencesOfTestcase", "Testcase", new int[] {8, 40})]
        public void FindAllOccurrences_IndexCheck(string t, string p, int[] expectedIndices)
        {
            List<int> result = RabinKarp.FindAllOccurrences(t, p);
            Assert.AreEqual(result, new List<int>(expectedIndices));
        }
        
    }
    
}