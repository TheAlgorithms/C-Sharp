using System;
using System.Collections.Generic;
using DataStructures.Probabilistic;
using NUnit.Framework;
using FluentAssertions;

namespace DataStructures.Tests.Probabilistic;

public class CountMinSketchTests
{
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
    public void TestInsertAndCount()
    {
        var obj1 = new SimpleObject("foo", 5);
        var obj2 = new SimpleObject("bar", 6);

        var sketch = new CountMinSketch<SimpleObject>(200, 5);

        for (var i = 0; i < 5000; i++)
        {
            sketch.Insert(obj1);
            sketch.Insert(obj2);
        }

        sketch.Query(obj1).Should().BeGreaterOrEqualTo(5000);
        sketch.Query(obj2).Should().BeGreaterOrEqualTo(5000);
    }

    [Test]
    public void TestOptimalInitializer()
    {
        var obj1 = new SimpleObject("foo", 5);
        var obj2 = new SimpleObject("bar", 6);

        var sketch = new CountMinSketch<SimpleObject>(.001, .05);

        for (var i = 0; i < 5000; i++)
        {
            sketch.Insert(obj1);
            sketch.Insert(obj2);
        }

        sketch.Query(obj1).Should().BeGreaterOrEqualTo(5000);
        sketch.Query(obj2).Should().BeGreaterOrEqualTo(5000);
    }

    [Test]
    public void TestProbabilities()
    {
        var sketch = new CountMinSketch<int>(.01, .05);
        var random = new Random();
        var insertedItems = new Dictionary<int,int>();
        for (var i = 0; i < 10000; i++)
        {
            var item = random.Next(0, 1000000);
            sketch.Insert(item);
            if (insertedItems.ContainsKey(item))
            {
                insertedItems[item]++;
            }
            else
            {
                insertedItems.Add(item, 1);
            }
        }

        var numMisses = 0;
        foreach (var item in insertedItems)
        {
            if (sketch.Query(item.Key) - item.Value > .01 * 100000)
            {
                numMisses++;
            }
        }

        (numMisses / (double)insertedItems.Count).Should().BeLessOrEqualTo(.05);
    }
}
