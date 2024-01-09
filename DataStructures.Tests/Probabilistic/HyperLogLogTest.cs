using System;
using System.Collections.Generic;
using DataStructures.Probabilistic;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests.Probabilistic;

public class HyperLogLogTest
{
    [Test]
    public void TestHyperLogLog()
    {
        var hll = new HyperLogLog<int>();
        HashSet<int> actual = new ();

        var rand = new Random();
        var tolerance = .05;
        for (var i = 0; i < 10000; i++)
        {
            var k = rand.Next(20000);
            hll.Add(k);
            actual.Add(k);
        }

        hll.Cardinality().Should()
            .BeGreaterOrEqualTo((int)(actual.Count * (1 - tolerance)))
            .And
            .BeLessOrEqualTo((int)(actual.Count * (1 + tolerance)));
    }

    [Test]
    public void TestHyperLogLogMerge()
    {
        var hll1 = new HyperLogLog<int>();
        var hll2 = new HyperLogLog<int>();
        var rand = new Random();
        var tolerance = .05;
        HashSet<int> actual = new ();
        for (var i = 0; i < 5000; i++)
        {
            var k = rand.Next(20000);
            hll1.Add(k);
            actual.Add(k);
        }

        for (var i = 0; i < 5000; i++)
        {
            var k = rand.Next(20000);
            hll2.Add(k);
            actual.Add(k);
        }

        var hll = HyperLogLog<int>.Merge(hll1, hll2);
        hll.Cardinality().Should()
            .BeGreaterOrEqualTo((int)(actual.Count * (1 - tolerance)))
            .And
            .BeLessOrEqualTo((int)(actual.Count * (1 + tolerance)));
    }
}
