using DataStructures.UnrolledList;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests.UnrolledList;

public class UnrolledLinkedListTests
{
    [Test]
    public void Insert_LinkArrayToLinkedList_ReturnArrayHaveSameItems()
    {
        var linkedList = new UnrolledLinkedList(6);
        var contest = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        foreach (var number in contest)
        {
            linkedList.Insert(number);
        }

        var result = linkedList.GetRolledItems();

        result.Should().BeEquivalentTo(contest);
    }
}
