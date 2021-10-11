using System;
using System.Collections.Generic;
using DataStructures.Probabilistic;
using NUnit.Framework;

namespace DataStructures.Tests.Probabilistic
{
    public class BloomFilterTests
    {
        static string[] TestNames = new[] { "kal;jsnfka", "alkjsdfn;lakm", "aljfopiawjf", "afowjeaofeij", "oajwsefoaiwje", "aoiwjfaoiejmf", "aoijfoawiejf" };
        public class SimpleObject
        {
            public string Name { get; set; }
            public int Number { get; set; }

            public SimpleObject(string name, int number)
            {
                Name = name;
                Number = number;
            }
        }

        [Test]
        public void TestBloomFilterInsertOptimalSize()
        {
            var filter = new BloomFilter<int>(1000);
            var set = new HashSet<int>();
            var rand = new Random();
            var falsePositives = 0;
            for (var i = 0; i < 1000; i++)
            {
                var k = rand.Next(0, 1000);
                if (!set.Contains(k) && filter.Search(k))
                {
                    falsePositives++;
                }
                filter.Insert(k);
                set.Add(k);
                Assert.IsTrue(filter.Search(k));
            }

            Assert.True(.03 > falsePositives / 1000.0);
        }

        [Test]
        public void TestBloomFilterInsert()
        {
            var filter = new BloomFilter<SimpleObject>(100000, 3);
            var rand = new Random();
            for (var i = 0; i < 1000; i++)
            {
                var simpleObject = new SimpleObject(TestNames[rand.Next(TestNames.Length)], rand.Next(15));
                filter.Insert(simpleObject);
                Assert.IsTrue(filter.Search(simpleObject));
            }
        }

        [Test]
        public void TestBloomFilterSearch()
        {
            var filter = new BloomFilter<SimpleObject>(100000, 3);
            var simpleObjectInserted = new SimpleObject("foo", 1);
            var simpleObjectInserted2 = new SimpleObject("foo", 1);
            var simpleObjectNotInserted = new SimpleObject("bar", 2);
            filter.Insert(simpleObjectInserted);
            Assert.IsTrue(filter.Search(simpleObjectInserted));
            Assert.IsTrue(filter.Search(simpleObjectInserted2));
            Assert.IsFalse(filter.Search(simpleObjectNotInserted));
        }
    }
}
