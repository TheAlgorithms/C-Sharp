using System;
using DataStructures.UnrolledList;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests.UnrolledList;

public class UnrolledLinkedListNodeTests
{
    [Test]
    public void GetAndSet_SetItemNodeAndGetIt_ReturnExpectedItem()
    {
        var node = new UnrolledLinkedListNode(6);
        node.Set(0, 1);

        var result = node.Get(0);

        result.Should().Be(1);
    }

    [Test]
    public void Get_GetLowIndex_ThrowArgumentException()
    {
        var node = new UnrolledLinkedListNode(6);

        Action action = () => node.Get(-1);

        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Get_GetHighIndex_ThrowArgumentException()
    {
        var node = new UnrolledLinkedListNode(6);

        Action action = () => node.Get(7);

        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Set_SetLowIndex_ThrowArgumentException()
    {
        var node = new UnrolledLinkedListNode(6);

        Action action = () => node.Set(-1, 0);

        action.Should().Throw<ArgumentException>();
    }

    [Test]
    public void Set_SetHighIndex_ThrowArgumentException()
    {
        var node = new UnrolledLinkedListNode(6);

        Action action = () => node.Set(7, 0);

        action.Should().Throw<ArgumentException>();
    }
}
