using System;
using Algorithms.Sorters.External;
using Algorithms.Sorters.External.Storages;
using Algorithms.Tests.Helpers;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Algorithms.Tests.Sorters.External;

public static class ExternalMergeSorterTests
{
    [Test]
    public static void ArraySorted(
        [Random(0, 1000, 100, Distinct = true)]
        int n)
    {
        // Arrange
        var sorter = new ExternalMergeSorter<int>();
        var intComparer = new IntComparer();
        var (correctArray, testArray) = RandomHelper.GetArrays(n);
        var main = new IntInMemoryStorage(testArray);
        var temp = new IntInMemoryStorage(new int[testArray.Length]);

        // Act
        sorter.Sort(main, temp, intComparer);
        Array.Sort(correctArray, intComparer);

        // Assert
        Assert.That(correctArray, Is.EqualTo(testArray));
    }

    [Test]
    public static void ArraySorted_OnDisk(
        [Random(0, 1000, 100, Distinct = true)]
        int n)
    {
        // Arrange
        var sorter = new ExternalMergeSorter<int>();
        var intComparer = new IntComparer();
        var (correctArray, testArray) = RandomHelper.GetArrays(n);
        var randomizer = Randomizer.CreateRandomizer();
        var main = new IntFileStorage($"sorted_{randomizer.GetString(100)}", n);
        var temp = new IntFileStorage($"temp_{randomizer.GetString(100)}", n);

        var writer = main.GetWriter();
        for (var i = 0; i < n; i++)
        {
            writer.Write(correctArray[i]);
        }

        writer.Dispose();

        // Act
        sorter.Sort(main, temp, intComparer);
        Array.Sort(correctArray, intComparer);

        // Assert
        var reader = main.GetReader();
        for (var i = 0; i < n; i++)
        {
            testArray[i] = reader.Read();
        }

        Assert.That(correctArray, Is.EqualTo(testArray));
    }
}
