using System;
using DataStructures.LinkedList.CircularLinkedList;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using NUnit.Framework;

namespace DataStructures.Tests.LinkedList;

[TestFixture]
public static class CircularLinkedListTests
{
    [Test]
    public static void TestDisplay()
    {
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);

        Assert.That("10 20 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
    }

    [Test]
    public static void TestDisplayEmptyList()
    {
        var cll = new CircularLinkedList<int>();
        cll.Display();

        Assert.That("List is empty.", Is.EqualTo(GetDisplayOutput(cll).Trim()));
    }

    [Test]
    public static void TestInsertAtBeginning()
    {
        var cll = new CircularLinkedList<int>();
        cll.InsertAtBeginning(10);
        cll.InsertAtBeginning(20);
        cll.InsertAtBeginning(30);

        Assert.That("30 20 10", Is.EqualTo(GetDisplayOutput(cll).Trim()));
    }

    [Test]
    public static void TestInsertAtEnd()
    {
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);

        Assert.That("10 20 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
    }

    [Test]
    public static void TestInsertAfter()
    {
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);

        cll.InsertAfter(20, 25);
        Assert.That("10 20 25 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
    }

    [Test]
    public static void TestInsertAtBeginningInEmptyList()
    {
        var cll = new CircularLinkedList<int>();
        cll.InsertAtBeginning(10);
        Assert.That("10", Is.EqualTo(GetDisplayOutput(cll).Trim()));
    }

    [Test]
    public static void TestInsertAtEndInEmptyList()
    {
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);

        Assert.That("10", Is.EqualTo(GetDisplayOutput(cll).Trim()));
    }

    [Test]
    public static void TestInsertAfterInEmptyList()
    {
        var cll = new CircularLinkedList<int>();
        cll.InsertAfter(10, 20);

        Assert.That("List is empty.", Is.EqualTo(GetDisplayOutput(cll).Trim()));
    }



    [Test]
    public static void TestInsertAfterSpecificNode()
    {
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);

        cll.InsertAfter(20, 25); // Insert after node with value 20
        Assert.That("10 20 25 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
    }

    [Test]
    public static void TestInsertAfterOnNonExistingValue()
    {
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
        cll.InsertAfter(99, 25); // 99 does not exist
        Assert.That("10", Is.EqualTo(GetDisplayOutput(cll).Trim()));
    }

    [Test]
    public static void TestDeleteNode()
    {
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);

        cll.DeleteNode(20);
        Assert.That("10 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
    }

    [Test]
    public static void TestDeleteOnlyNode()
    {
        var cll = new CircularLinkedList<int>();
        cll.InsertAtBeginning(10);
        cll.DeleteNode(10);
        Assert.That(cll.IsEmpty(), Is.EqualTo(true));
    }

    [Test]
    public static void TestDeleteHeadNode()
    {
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);
        cll.DeleteNode(10);
        Assert.That("20 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
    }

    [Test]
    public static void TestDeleteTailNode()
    {
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);
        cll.DeleteNode(30);
        Assert.That("10 20", Is.EqualTo(GetDisplayOutput(cll).Trim()));
    }

    [Test]
    public static void TestDeleteFromEmptyList()
    {
        var cll = new CircularLinkedList<int>();
        cll.DeleteNode(10);
        Assert.That(cll.IsEmpty(), Is.EqualTo(true));
    }

    [Test]
    public static void TestDeleteNonExistentNode()
    {
        var cll = new CircularLinkedList<int>();
        cll.InsertAtEnd(10);
        cll.InsertAtEnd(20);
        cll.InsertAtEnd(30);

        cll.DeleteNode(40); // Attempting to delete a node that doesn't exist
        Assert.That("10 20 30", Is.EqualTo(GetDisplayOutput(cll).Trim()));
    }

    /// <summary>
    /// Helper method to capture the output of the Display method for assertions.
    /// </summary>
    /// <param name="list">The CircularLinkedList instance.</param>
    /// <returns>A string representation of the list.</returns>
    private static string GetDisplayOutput(CircularLinkedList<int> list)
    {
        // Save the original output (the default Console output stream)
        var originalConsoleOut = Console.Out;

        // Use a StringWriter to capture Console output
        using (var sw = new System.IO.StringWriter())
        {
            try
            {
                // Redirect Console output to StringWriter
                Console.SetOut(sw);

                // Call the method that outputs to the console
                list.Display();

                // Return the captured output
                return sw.ToString();
            }
            finally
            {
                // Restore the original Console output stream
                Console.SetOut(originalConsoleOut);
            }
        }
    }
}
