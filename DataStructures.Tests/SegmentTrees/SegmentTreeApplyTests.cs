using DataStructures.SegmentTrees;
using NUnit.Framework;

namespace DataStructures.Tests.SegmentTrees
{
    [TestFixture]
    public class SegmentTreeApplyTests
    {
        private readonly SegmentTreeApply testTree = new(new [] {8, 9, 1, 4, 8, 7, 2});

        [Test]
        public void Apply_Query_Update_Query_Test()
        {
            Assert.AreEqual(22, testTree.Query(1, 4));
            testTree.Apply(0, 3, 2);
            Assert.AreEqual(new [] {1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}, testTree.Operand);
            Assert.AreEqual(36, testTree.Query(1, 4));            
        }

    }
}