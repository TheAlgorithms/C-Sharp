using DataStructures.LinkedList.CircularLinkedList;

namespace DataStructures.Tests.LinkedList;

[TestFixture]
public static class CircularLinkedListTests
{
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
        var ex = Assert.Throws<InvalidOperationException>(() => cll.InsertAfter(10, 20));

        Assert.That(ex!.Message, Is.EqualTo("List is empty."));
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
        var ex = Assert.Throws<InvalidOperationException>(() => cll.DeleteNode(10));

        Assert.That(ex!.Message, Is.EqualTo("List is empty."));
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

    private static string GetDisplayOutput<T>(CircularLinkedList<T> list)
    {
        var head = list.GetHead();
        if (head == null)
        {
            return string.Empty;
        }

        var current = head;
        var result = new System.Text.StringBuilder();

        do
        {
            result.Append(current!.Data + " ");
            current = current.Next;
        }
        while (current != head);

        return result.ToString().Trim();
    }
}
[Test]
public static void InsertAfter_InsertingAfterTail_UpdatesTailAndPreservesHead()
{
    var list = new CircularLinkedList<int>();
    list.InsertAtEnd(10);

    list.InsertAfter(10, 20);

    var head = list.GetHead();
    Assert.That(head, Is.Not.Null);
    Assert.That(head!.Data, Is.EqualTo(10));
    Assert.That(head.Next, Is.Not.Null);
    Assert.That(head.Next!.Data, Is.EqualTo(20));
    Assert.That(head.Next.Next, Is.SameAs(head));
}
