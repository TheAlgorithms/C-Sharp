using DataStructures.DisjointSet;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests.DisjointSet;

[TestFixture]
public class DisjointSetTests
{
    [Test]
    public static void MakeSetDataInitializationTest()
    {
        DisjointSet<int> ds = new();
        var one = ds.MakeSet(1);
        var two = ds.MakeSet(2);
        one.Data.Should().Be(1);
        two.Data.Should().Be(2);
    }
    [Test]
    public static void UnionTest()
    {
        DisjointSet<int> ds = new();
        var one = ds.MakeSet(1);
        var two = ds.MakeSet(2);
        var three = ds.MakeSet(3);
        ds.UnionSet(one, two);
        ds.FindSet(one).Should().Be(ds.FindSet(two));
        ds.UnionSet(one, three);
        ds.FindSet(two).Should().Be(ds.FindSet(three));
        (one.Rank + two.Rank + three.Rank).Should().Be(1);
    }
}
