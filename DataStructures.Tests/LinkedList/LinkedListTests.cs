using System;
using System.Linq;
using DataStructures.LinkedList.SinglyLinkedList;
using NUnit.Framework;

namespace DataStructures.Tests.LinkedList;

public static class LinkedListTests
{
    [Test]
    public static void LengthWorksCorrectly([Random(0, 1000, 100)] int quantity)
    {
        // Arrange
        var a = new SinglyLinkedList<int>();

        // Act
        var r = TestContext.CurrentContext.Random;
        for (var i = 0; i < quantity; i++)
        {
            _ = a.AddFirst(r.Next());
        }

        // Assert
        Assert.That(quantity, Is.EqualTo(a.Length()));
    }

    [Test]
    public static void LengthOnEmptyListIsZero()
    {
        // Arrange
        var a = new SinglyLinkedList<int>();

        // Act

        // Assert
        Assert.That(0, Is.EqualTo(a.Length()));
    }

    [Test]
    public static void GetItemsFromLinkedList()
    {
        // Arrange
        var testObj = new SinglyLinkedList<string>();
        _ = testObj.AddLast("H");
        _ = testObj.AddLast("E");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("O");

        // Act
        var items = testObj.GetListData();

        // Assert
        Assert.That(5, Is.EqualTo(items.Count()));
        Assert.That("O", Is.EqualTo(testObj.GetElementByIndex(4)));
    }

    [Test]
    public static void GetElementByIndex_IndexOutOfRange_ArgumentOutOfRangeExceptionThrown()
    {
        // Arrange
        var list = new SinglyLinkedList<int>();

        // Act
        _ = list.AddFirst(1);
        _ = list.AddFirst(2);
        _ = list.AddFirst(3);

        // Assert
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => list.GetElementByIndex(-1));
        _ = Assert.Throws<ArgumentOutOfRangeException>(() => list.GetElementByIndex(3));
    }


    [Test]
    public static void RemoveItemsFromList()
    {
        // Arrange
        var testObj = new SinglyLinkedList<string>();
        _ = testObj.AddLast("X");
        _ = testObj.AddLast("H");
        _ = testObj.AddLast("E");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("I");
        _ = testObj.AddLast("O");

        // Act
        var xRemoveSucess = testObj.DeleteElement("X");
        var oRemoveSucess = testObj.DeleteElement("O");
        var eRemoveSucess = testObj.DeleteElement("E");
        var lRemoveSucess = testObj.DeleteElement("L");
        var l2RemoveSucess = testObj.DeleteElement("L");
        var l3RemoveSucess = testObj.DeleteElement("L");
        var nonExistantRemoveSucess = testObj.DeleteElement("F");

        var resultString = testObj.GetElementByIndex(0) + testObj.GetElementByIndex(1);

        // Assert
        Assert.That("HI", Is.EqualTo(resultString));
        Assert.That(xRemoveSucess, Is.True);
        Assert.That(oRemoveSucess, Is.True);
        Assert.That(eRemoveSucess, Is.True);
        Assert.That(lRemoveSucess, Is.True);
        Assert.That(l2RemoveSucess, Is.True);
        Assert.That(l3RemoveSucess, Is.False);
        Assert.That(nonExistantRemoveSucess, Is.False);
    }

    [Test]
    public static void DeleteFirstFromList()
    {
        // Arrange
        var testObj = new SinglyLinkedList<string>();
        _ = testObj.AddLast("H");
        _ = testObj.AddLast("E");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("O");

        // Act
        var deleteSuccess = testObj.DeleteFirst();

        // Assert
        Assert.That(deleteSuccess, Is.True);
        Assert.That(4, Is.EqualTo(testObj.Length()));
        Assert.That("E", Is.EqualTo(testObj.GetElementByIndex(0)));
    }

    [Test]
    public static void DeleteFirstFromEmptyList()
    {
        // Arrange
        var testObj = new SinglyLinkedList<string>();

        // Act
        var deleteSuccess = testObj.DeleteFirst();

        // Assert
        Assert.That(deleteSuccess, Is.False);
    }

    [Test]
    public static void DeleteLastFromList()
    {
        // Arrange
        var testObj = new SinglyLinkedList<string>();
        _ = testObj.AddLast("H");
        _ = testObj.AddLast("E");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("L");
        _ = testObj.AddLast("O");

        // Act
        var deleteSuccess = testObj.DeleteLast();

        // Assert
        Assert.That(deleteSuccess, Is.True);
        Assert.That(4, Is.EqualTo(testObj.Length()));
        Assert.That("L", Is.EqualTo(testObj.GetElementByIndex(testObj.Length() - 1)));
    }

    [Test]
    public static void DeleteLastFromEmptyList()
    {
        // Arrange
        var testObj = new SinglyLinkedList<string>();

        // Act
        var deleteSuccess = testObj.DeleteLast();

        // Assert
        Assert.That(deleteSuccess, Is.False);
    }

}
