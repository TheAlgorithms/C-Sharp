using DataStructures.Cache;

namespace DataStructures.Tests.Cache;

public static class LfuCacheTests
{
    [Test]
    public static void TestPutGet()
    {
        var cache = new LfuCache<int, string>();
        cache.Put(1, "one");

        cache.Contains(1).Should().BeTrue();
        cache.Get(1).Should().Be("one");
    }

    [Test]
    public static void TestCacheMiss()
    {
        var cache = new LfuCache<int, string>();
        cache.Put(1, "one");

        cache.Contains(5).Should().BeFalse();
        cache.Get(5).Should().BeNull();
    }

    [Test]
    public static void Evict_ItemWasNotUsed()
    {
        var cache = new LfuCache<int, string>(capacity: 1);
        cache.Put(1, "one");

        // Add to the full cache, 1 will be removed
        cache.Put(2, "two");

        cache.Get(1).Should().BeNull();
        cache.Get(2).Should().Be("two");
    }

    [Test]
    public static void Evict_OneItemWasUsed()
    {
        var cache = new LfuCache<int, string>(capacity: 2);
        cache.Put(1, "one");
        cache.Put(2, "two");

        cache.Put(1, "ONE");

        // Add to the full cache, 2 will be removed
        cache.Put(3, "three");

        cache.Get(1).Should().Be("ONE");
        cache.Get(2).Should().BeNull();
        cache.Get(3).Should().Be("three");
    }

    [Test]
    public static void Evict_LruOrder()
    {
        var cache = new LfuCache<int, string>(capacity: 2);
        cache.Put(1, "one");
        cache.Put(2, "two");

        cache.Put(1, "ONE");
        cache.Put(2, "TWO");

        // Add to the full cache, 1 will be removed
        cache.Put(3, "three");

        cache.Get(1).Should().BeNull();
        cache.Get(2).Should().Be("TWO");
        cache.Get(3).Should().Be("three");
    }
}
