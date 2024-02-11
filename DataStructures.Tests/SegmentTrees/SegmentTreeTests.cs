using DataStructures.SegmentTrees;
using NUnit.Framework;

namespace DataStructures.Tests.SegmentTrees;

[TestFixture]
public class SegmentTreeTests
{
    private readonly SegmentTree testTree = new(new[] { 8, 9, 1, 4, 8, 7, 2 });

    [Test]
    public void TreeArray_Test()
    {
        int[] expectedArray = { 0, 39, 22, 17, 17, 5, 15, 2, 8, 9, 1, 4, 8, 7, 2, 0 };
        Assert.That(testTree.Tree, Is.EqualTo(expectedArray));
    }

    [TestCase(1, 4, 22)]
    [TestCase(2, 2, 1)]
    public void Query_Test(int left, int right, int expectedValue)
    {
        Assert.That(testTree.Query(left, right), Is.EqualTo(expectedValue));
    }
}
