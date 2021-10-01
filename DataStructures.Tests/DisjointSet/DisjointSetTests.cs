using NUnit.Framework;

namespace DataStructures.DisjointSet.Tests
{
    class DisjointSetTests
    {
        [Test]
        public static void MakeTest()
        {
            DisjointSet<int> ds = new DisjointSet<int>();
            var one = ds.MakeSet(1);
            var two = ds.MakeSet(2);
            Assert.IsTrue(one.Data == 1);
            Assert.IsTrue(two.Data == 2);
        }
        [Test]
        public static void UnionTest()
        {
            DisjointSet<int> ds = new DisjointSet<int>();
            var one = ds.MakeSet(1);
            var two = ds.MakeSet(2);
            var three = ds.MakeSet(3);
            ds.UnionSet(one, two);
            Assert.IsTrue(ds.FindSet(one) == ds.FindSet(two));
            ds.UnionSet(one, three);
            Assert.IsTrue(ds.FindSet(two) == ds.FindSet(three));
            Assert.IsTrue(one.Rank + two.Rank + three.Rank == 1);
        }
    }
}
