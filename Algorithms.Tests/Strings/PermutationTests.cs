using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Numeric;
using Algorithms.Strings;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Strings
{
    public class AnagramTests
    {

        [TestCase("")]
        [TestCase("A")]
        [TestCase("abcd")]
        [TestCase("aabcd")]
        [TestCase("aabbbcd")]
        [TestCase("aabbbccccd")]
        public void Test_GetEveryUniquePermutation(string word)
        {
            var anagrams = Permutation.GetEveryUniquePermutation(word);

            // make sure we have the right number of anagrams
            var occDic = new Dictionary<char, int>();
            foreach (var c in word)
            {
                if (occDic.ContainsKey(c))
                {
                    occDic[c] += 1;
                }
                else
                {
                    occDic[c] = 1;
                }
            }

            var expectedNumberOfAnagrams = Factorial.Calculate(word.Length);
            expectedNumberOfAnagrams = occDic.Aggregate(expectedNumberOfAnagrams, (current, keyValuePair) =>
            {
                return current / Factorial.Calculate(keyValuePair.Value);
            });
            Assert.AreEqual(expectedNumberOfAnagrams, anagrams.Count);

            // make sure all strings in "anagrams" are actually a permutation of "word"
            var wordSorted = SortString(word);
            foreach (var anagram in anagrams)
            {
                Assert.AreEqual(wordSorted, SortString(anagram));
            }
        }

        private static string SortString(string word)
        {
            var asArray = word.ToArray();
            Array.Sort(asArray);
            return new string(asArray);
        }
    }
}
