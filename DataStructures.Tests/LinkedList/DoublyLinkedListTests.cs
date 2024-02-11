using System;
using System.Linq;
using DataStructures.LinkedList.DoublyLinkedList;
using NUnit.Framework;

namespace DataStructures.Tests.LinkedList;

public static class DoublyLinkedListTests
{
    [Test]
    public static void TestGetData()
    {
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });
        var arr = dll.GetData().ToArray();

        Assert.That(dll.Count, Is.EqualTo(5));
        Assert.That(new[] { 0, 1, 2, 3, 4 }, Is.EqualTo(arr));
    }

    [Test]
    public static void TestGetAt()
    {
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });

        var one = dll.GetAt(1);
        var three = dll.GetAt(3);

        Assert.That(one.Data, Is.EqualTo(1));
        Assert.That(three.Data, Is.EqualTo(3));
        Assert.Throws<ArgumentOutOfRangeException>(
            () => dll.GetAt(-1)
        );
        Assert.Throws<ArgumentOutOfRangeException>(
            () => dll.GetAt(5)
        );
    }

    [Test]
    public static void TestAddtion()
    {
        var dll = new DoublyLinkedList<int>(0);
        var one = dll.Add(1);

        dll.Add(3);
        dll.AddAfter(2, one);
        dll.Add(4);

        var arr = dll.GetData().ToArray();
        var reversedArr = dll.GetDataReversed().ToArray();

        Assert.That(dll.Count, Is.EqualTo(5));
        Assert.That(new[] { 0, 1, 2, 3, 4 }, Is.EqualTo(arr));
        Assert.That(new[] { 4, 3, 2, 1, 0 }, Is.EqualTo(reversedArr));
    }

    [Test]
    public static void TestRemove()
    {
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });

        dll.RemoveNode(dll.Find(2));
        dll.RemoveHead();
        dll.Remove();

        var arr = dll.GetData().ToArray();
        var reversedArr = dll.GetDataReversed().ToArray();

        Assert.That(dll.Count, Is.EqualTo(2)    );
        Assert.That(new[] { 1, 3 }, Is.EqualTo(arr));
        Assert.That(new[] { 3, 1 }, Is.EqualTo(reversedArr));
    }

    [Test]
    public static void TestFind()
    {
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });

        var one = dll.Find(1);
        var three = dll.Find(3);

        Assert.That(one.Data, Is.EqualTo(1));
        Assert.That(three.Data, Is.EqualTo(3));
    }

    [Test]
    public static void TestIndexOf()
    {
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });

        var one = dll.IndexOf(1);
        var three = dll.IndexOf(3);

        Assert.That(one, Is.EqualTo(1));
        Assert.That(three, Is.EqualTo(3));
    }

    [Test]
    public static void TestContains()
    {
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });

        var one = dll.Contains(1);
        var six = dll.Contains(6);

        Assert.That(one, Is.True);
        Assert.That(six, Is.False);
    }

    [Test]
    public static void TestReverse()
    {
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });
        dll.Reverse();
        var arr = dll.GetData().ToArray();

        var empty = new DoublyLinkedList<int>(new int[] { });
        empty.Reverse();
        var emptyArr = empty.GetData().ToArray();

        Assert.That(arr, Is.EqualTo(new[] { 4, 3, 2, 1, 0 }));
        Assert.That(emptyArr, Is.EqualTo(new int[] { }));
    }

    [Test]
    public static void TestGetDataReversed()
    {
        var dll = new DoublyLinkedList<int>(new[] { 0, 1, 2, 3, 4 });
        var arr = dll.GetData().ToArray();
        var reversedArr = dll.GetDataReversed().ToArray();

        Assert.That(arr, Is.EqualTo(new[] { 0, 1, 2, 3, 4 }));
        Assert.That(reversedArr, Is.EqualTo(new[] { 4, 3, 2, 1, 0 }));
    }
}
