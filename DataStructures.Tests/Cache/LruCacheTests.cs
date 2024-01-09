using DataStructures.Cache;
using FluentAssertions;
using NUnit.Framework;

namespace DataStructures.Tests.Cache;

public static class LruCacheTests
{
    [Test]
    public static void TestPutGet()
    {
        var cache = new LruCache<int, string>();
        cache.Put(1, "one");

        cache.Contains(1).Should().BeTrue();
        cache.Get(1).Should().Be("one");
    }

    [Test]
    public static void TestCacheMiss()
    {
        var cache = new LruCache<int, string>();
        cache.Put(1, "one");

        cache.Contains(5).Should().BeFalse();
        cache.Get(5).Should().BeNull();
    }

    [Test]
    public static void TestCacheUpdate()
    {
        var cache = new LruCache<int, string>();
        cache.Put(1, "one");
        cache.Put(1, "ONE");

        cache.Get(1).Should().Be("ONE");
    }

    [Test]
    public static void RemoveOldestItem_ItemWasNotUsed()
    {
        var cache = new LruCache<int, string>(capacity: 2);
        cache.Put(1, "one");
        cache.Put(2, "two");

        // Add to the full cache, 1 will be removed
        cache.Put(3, "three");

        cache.Get(1).Should().BeNull();
        cache.Get(2).Should().Be("two");
        cache.Get(3).Should().Be("three");
    }

    [Test]
    public static void RemoveOldestItem_ItemWasRecentlyUsed()
    {
        var cache = new LruCache<int, string>(capacity: 2);
        cache.Put(1, "one");
        cache.Put(2, "two");
        cache.Get(1);

        // Add to the full cache, 1 was used, 2 should be removed
        cache.Put(3, "three");

        cache.Get(1).Should().Be("one");
        cache.Get(2).Should().BeNull();
        cache.Get(3).Should().Be("three");
    }
}
