using System;
using DataStructures.LinkedList.SkipList;
using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;

namespace DataStructures.Tests.LinkedList;

public static class SkipListTests
{
    [Test]
    public static void TestAdd()
    {
        var list = new SkipList<int>();
        list.AddOrUpdate(1, 1);
        list[2] = 2;
        list[3] = 3;

        list.Count.Should().Be(3);
        list.GetValues().Should().ContainInOrder(1, 2, 3);
    }

    [Test]
    public static void TestUpdate()
    {
        var list = new SkipList<string>();

        // Add some elements.
        list[1] = "v1";
        list[2] = "v2";
        list[5] = "v5";

        // Update
        list.AddOrUpdate(1, "v1-updated");
        list[2] = "v2-updated";

        list.Count.Should().Be(3);
        list.GetValues().Should().ContainInOrder("v1-updated", "v2-updated", "v5");
    }

    [Test]
    public static void TestContains()
    {
        var list = new SkipList<int>();
        list.AddOrUpdate(1, 1);
        list.AddOrUpdate(3, 3);
        list.AddOrUpdate(5, 5);

        list.Contains(1).Should().BeTrue();
        list.Contains(3).Should().BeTrue();
        list.Contains(5).Should().BeTrue();
        list.Contains(0).Should().BeFalse();
        list.Contains(2).Should().BeFalse();
        list.Contains(9).Should().BeFalse();
    }

    [Test]
    public static void TestGetByKey_Success()
    {
        var list = new SkipList<string>();
        list[1] = "value1";

        list[1].Should().Be("value1");
    }

    [Test]
    public static void TestGetByKey_KeyNotFoundException()
    {
        var list = new SkipList<string>();
        list[1] = "value1";

        string value;
        Action act = () => value = list[2];
        act.Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public static void TestRemove_ItemRemoved()
    {
        var list = new SkipList<int>();
        list.AddOrUpdate(1, 1);
        list.AddOrUpdate(2, 2);
        list.AddOrUpdate(3, 3);

        list.Count.Should().Be(3);
        list.Contains(2).Should().BeTrue();

        var isRemoved = list.Remove(2);

        list.Count.Should().Be(2);
        list.Contains(2).Should().BeFalse();
        isRemoved.Should().BeTrue();
    }

     [Test]
    public static void TestRemove_ItemNotFound()
    {
        var list = new SkipList<int>();
        list.AddOrUpdate(1, 1);
        list.AddOrUpdate(2, 2);
        list.AddOrUpdate(3, 3);

        var isRemoved = list.Remove(222);

        list.Count.Should().Be(3);
        isRemoved.Should().BeFalse();
    }

    [Test]
    public static void TestGetValues()
    {
        var list = new SkipList<string>();
        list[4] = "four";
        list[2] = "two";
        list[3] = "three";
        list[1] = "one";

        var valuesSortedByKey = list.GetValues();

        valuesSortedByKey.Should().ContainInOrder("one", "two", "three", "four");
    }
}
