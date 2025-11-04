using DataStructures.BTree;
using static FluentAssertions.FluentActions;

namespace DataStructures.Tests;

internal class BTreeTests
{
    private static readonly int[] Data = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];

    [Test]
    public void Constructor_DefaultMinimumDegree_CreatesEmptyTree()
    {
        var tree = new BTree<int>();

        tree.Count.Should().Be(0);
        tree.MinimumDegree.Should().Be(2);
    }

    [Test]
    public void Constructor_CustomMinimumDegree_SetsCorrectDegree()
    {
        var tree = new BTree<int>(3);

        tree.MinimumDegree.Should().Be(3);
        tree.Count.Should().Be(0);
    }

    [Test]
    public void Constructor_MinimumDegreeLessThan2_ThrowsException()
    {
        Invoking(() => new BTree<int>(1))
            .Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("Minimum degree must be at least 2.*");
    }

    [Test]
    public void Constructor_CustomComparerMinimumDegreeLessThan2_ThrowsException()
    {
        var comparer = Comparer<int>.Create((x, y) => y.CompareTo(x));

        Invoking(() => new BTree<int>(1, comparer))
            .Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("Minimum degree must be at least 2.*");
    }

    [Test]
    public void Constructor_UseCustomComparer_FormsCorrectTree()
    {
        var tree = new BTree<int>(2, Comparer<int>.Create((x, y) => y.CompareTo(x)));

        tree.AddRange(new[] { 1, 2, 3, 4, 5 });

        tree.GetMin().Should().Be(5);
        tree.GetMax().Should().Be(1);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 5, 4, 3, 2, 1 },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Add_SingleKey_IncreasesCount()
    {
        var tree = new BTree<int>();

        tree.Add(5);

        tree.Count.Should().Be(1);
        tree.Contains(5).Should().BeTrue();
    }

    [Test]
    public void Add_MultipleKeys_FormsCorrectTree()
    {
        var tree = new BTree<int>(2);

        for (var i = 0; i < Data.Length; i++)
        {
            tree.Add(Data[i]);
            tree.Count.Should().Be(i + 1);
        }

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                Data,
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Add_DuplicateKey_ThrowsException()
    {
        var tree = new BTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5 });

        Invoking(() => tree.Add(3))
            .Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("""Key "3" already exists in B-Tree.""");
    }

    [Test]
    public void Add_DuplicateKeyInLeaf_ThrowsException()
    {
        var tree = new BTree<int>(3);
        tree.AddRange(new[] { 10, 20 });

        Invoking(() => tree.Add(10))
            .Should()
            .ThrowExactly<ArgumentException>()
            .WithMessage("""Key "10" already exists in B-Tree.""");
    }

    [Test]
    public void Add_CausesNodeSplit_TreeStillValid()
    {
        var tree = new BTree<int>(2);
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7 });

        tree.Count.Should().Be(7);
        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 4, 5, 6, 7 },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Add_LargeNumberOfKeys_TreeStillValid()
    {
        var tree = new BTree<int>(3);
        var keys = Enumerable.Range(1, 100).ToArray();

        tree.AddRange(keys);

        tree.Count.Should().Be(100);
        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                keys,
                config => config.WithStrictOrdering());
    }

    [Test]
    public void AddRange_MultipleKeys_FormsCorrectTree()
    {
        var tree = new BTree<char>(2);
        tree.AddRange(new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });

        tree.Count.Should().Be(7);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void AddRange_EmptyCollection_TreeRemainsEmpty()
    {
        var tree = new BTree<int>();
        tree.AddRange(Array.Empty<int>());

        tree.Count.Should().Be(0);
        tree.GetKeysInOrder().Should().BeEmpty();
    }

    [Test]
    public void Contains_KeyExists_ReturnsTrue()
    {
        var tree = new BTree<int>();
        tree.AddRange(Data);

        tree.Contains(5).Should().BeTrue();
        tree.Contains(1).Should().BeTrue();
        tree.Contains(15).Should().BeTrue();
    }

    [Test]
    public void Contains_KeyDoesNotExist_ReturnsFalse()
    {
        var tree = new BTree<int>();
        tree.AddRange(Data);

        tree.Contains(100).Should().BeFalse();
        tree.Contains(-5).Should().BeFalse();
        tree.Contains(0).Should().BeFalse();
    }

    [Test]
    public void Contains_EmptyTree_ReturnsFalse()
    {
        var tree = new BTree<int>();

        tree.Contains(5).Should().BeFalse();
        tree.Contains(-12).Should().BeFalse();
    }

    [Test]
    public void Remove_SingleKey_DecreasesCount()
    {
        var tree = new BTree<int>();
        tree.AddRange(new[] { 1, 2, 3, 4, 5 });

        tree.Remove(3);

        tree.Count.Should().Be(4);
        tree.Contains(3).Should().BeFalse();
    }

    [Test]
    public void Remove_FromLeafNode_TreeStillValid()
    {
        var tree = new BTree<int>(2);
        tree.AddRange(Data);

        tree.Remove(1);

        tree.Count.Should().Be(14);
        tree.Contains(1).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Remove_FromNonLeafNode_TreeStillValid()
    {
        var tree = new BTree<int>(2);
        tree.AddRange(Data);

        tree.Remove(8);

        tree.Count.Should().Be(14);
        tree.Contains(8).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 4, 5, 6, 7, 9, 10, 11, 12, 13, 14, 15 },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Remove_CausesMerge_TreeStillValid()
    {
        var tree = new BTree<int>(2);
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7 });

        tree.Remove(4);

        tree.Count.Should().Be(6);
        tree.Contains(4).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 5, 6, 7 },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Remove_MultipleKeys_TreeStillValid()
    {
        var tree = new BTree<int>(2);
        tree.AddRange(Data);

        tree.Remove(5);
        tree.Remove(10);
        tree.Remove(15);

        tree.Count.Should().Be(12);
        tree.Contains(5).Should().BeFalse();
        tree.Contains(10).Should().BeFalse();
        tree.Contains(15).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 13, 14 },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Remove_AllKeys_TreeBecomesEmpty()
    {
        var tree = new BTree<int>(2);
        tree.AddRange(new[] { 1, 2, 3, 4, 5 });

        tree.Remove(1);
        tree.Remove(2);
        tree.Remove(3);
        tree.Remove(4);
        tree.Remove(5);

        tree.Count.Should().Be(0);
        tree.GetKeysInOrder().Should().BeEmpty();
    }

    [Test]
    public void Remove_EmptyTree_ThrowsException()
    {
        var tree = new BTree<int>();

        Invoking(() => tree.Remove(1))
            .Should()
            .ThrowExactly<KeyNotFoundException>()
            .WithMessage("""Key "1" is not in the B-Tree.""");
    }

    [Test]
    public void Remove_KeyNotInTree_ThrowsException()
    {
        var tree = new BTree<int>();
        tree.AddRange(Data);

        Invoking(() => tree.Remove(100))
            .Should()
            .ThrowExactly<KeyNotFoundException>()
            .WithMessage("""Key "100" is not in the B-Tree.""");
    }

    [Test]
    public void GetMin_NonEmptyTree_ReturnsMinimum()
    {
        var tree = new BTree<int>();
        tree.AddRange(new[] { 5, 2, 8, 1, 9, 3 });

        tree.GetMin().Should().Be(1);
    }

    [Test]
    public void GetMin_SingleElement_ReturnsElement()
    {
        var tree = new BTree<int>();
        tree.Add(42);

        tree.GetMin().Should().Be(42);
    }

    [Test]
    public void GetMin_EmptyTree_ThrowsException()
    {
        var tree = new BTree<int>();

        Invoking(() => tree.GetMin())
            .Should()
            .ThrowExactly<InvalidOperationException>()
            .WithMessage("B-Tree is empty.");
    }

    [Test]
    public void GetMax_NonEmptyTree_ReturnsMaximum()
    {
        var tree = new BTree<int>();
        tree.AddRange(new[] { 5, 2, 8, 1, 9, 3 });

        tree.GetMax().Should().Be(9);
    }

    [Test]
    public void GetMax_SingleElement_ReturnsElement()
    {
        var tree = new BTree<int>();
        tree.Add(42);

        tree.GetMax().Should().Be(42);
    }

    [Test]
    public void GetMax_EmptyTree_ThrowsException()
    {
        var tree = new BTree<int>();

        Invoking(() => tree.GetMax())
            .Should()
            .ThrowExactly<InvalidOperationException>()
            .WithMessage("B-Tree is empty.");
    }

    [Test]
    public void GetKeysInOrder_NonEmptyTree_ReturnsCorrectOrder()
    {
        var tree = new BTree<int>(2);
        tree.AddRange(new[] { 5, 2, 8, 1, 9, 3, 7, 4, 6 });

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void GetKeysInOrder_EmptyTree_ReturnsEmpty()
    {
        var tree = new BTree<int>();

        tree.GetKeysInOrder().Should().BeEmpty();
    }

    [Test]
    public void GetKeysInOrder_AfterRemoval_ReturnsCorrectOrder()
    {
        var tree = new BTree<int>(2);
        tree.AddRange(Data);
        tree.Remove(5);
        tree.Remove(10);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 4, 6, 7, 8, 9, 11, 12, 13, 14, 15 },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void GetKeysPreOrder_NonEmptyTree_ReturnsCorrectOrder()
    {
        var tree = new BTree<int>(2);
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7 });

        var preOrder = tree.GetKeysPreOrder().ToArray();

        preOrder.Should().HaveCount(7);
        preOrder.Should().Contain(new[] { 1, 2, 3, 4, 5, 6, 7 });
    }

    [Test]
    public void GetKeysPreOrder_EmptyTree_ReturnsEmpty()
    {
        var tree = new BTree<int>();

        tree.GetKeysPreOrder().Should().BeEmpty();
    }

    [Test]
    public void GetKeysPostOrder_NonEmptyTree_ReturnsCorrectOrder()
    {
        var tree = new BTree<int>(2);
        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7 });

        var postOrder = tree.GetKeysPostOrder().ToArray();

        postOrder.Should().HaveCount(7);
        postOrder.Should().Contain(new[] { 1, 2, 3, 4, 5, 6, 7 });
    }

    [Test]
    public void GetKeysPostOrder_EmptyTree_ReturnsEmpty()
    {
        var tree = new BTree<int>();

        tree.GetKeysPostOrder().Should().BeEmpty();
    }

    [Test]
    public void BTree_LargeDataSet_MaintainsCorrectness()
    {
        var tree = new BTree<int>(5);
        var keys = Enumerable.Range(1, 1000).ToArray();

        tree.AddRange(keys);

        tree.Count.Should().Be(1000);
        tree.GetMin().Should().Be(1);
        tree.GetMax().Should().Be(1000);

        var inOrder = tree.GetKeysInOrder().ToArray();
        inOrder.Should().BeEquivalentTo(keys, config => config.WithStrictOrdering());

        for (var i = 1; i <= 1000; i++)
        {
            tree.Contains(i).Should().BeTrue();
        }
    }

    [Test]
    public void BTree_RandomInsertion_MaintainsCorrectness()
    {
        var tree = new BTree<int>(3);
        var random = new Random(42);
        var keys = Enumerable.Range(1, 100)
            .OrderBy(_ => random.Next())
            .ToArray();

        tree.AddRange(keys);

        tree.Count.Should().Be(100);

        var inOrder = tree.GetKeysInOrder().ToArray();
        inOrder.Should().BeEquivalentTo(
            Enumerable.Range(1, 100),
            config => config.WithStrictOrdering());
    }

    [Test]
    public void BTree_RandomDeletion_MaintainsCorrectness()
    {
        var tree = new BTree<int>(3);
        tree.AddRange(Enumerable.Range(1, 50));

        var random = new Random(42);
        var keysToRemove = Enumerable.Range(1, 50)
            .OrderBy(_ => random.Next())
            .Take(25)
            .ToArray();

        foreach (var key in keysToRemove)
        {
            tree.Remove(key);
        }

        tree.Count.Should().Be(25);

        var remainingKeys = Enumerable.Range(1, 50)
            .Except(keysToRemove)
            .OrderBy(x => x)
            .ToArray();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                remainingKeys,
                config => config.WithStrictOrdering());
    }

    [Test]
    public void BTree_StringKeys_WorksCorrectly()
    {
        var tree = new BTree<string>(2);
        var keys = new[] { "apple", "banana", "cherry", "date", "elderberry", "fig", "grape" };

        tree.AddRange(keys);

        tree.Count.Should().Be(7);
        tree.GetMin().Should().Be("apple");
        tree.GetMax().Should().Be("grape");

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                keys.OrderBy(x => x),
                config => config.WithStrictOrdering());
    }

    [Test]
    public void BTree_DifferentMinimumDegrees_AllWorkCorrectly()
    {
        for (var degree = 2; degree <= 10; degree++)
        {
            var tree = new BTree<int>(degree);
            tree.AddRange(Enumerable.Range(1, 50));

            tree.Count.Should().Be(50);
            tree.GetKeysInOrder()
                .Should()
                .BeEquivalentTo(
                    Enumerable.Range(1, 50),
                    config => config.WithStrictOrdering());
        }
    }

    [Test]
    public void BTree_InsertRemoveInsert_WorksCorrectly()
    {
        var tree = new BTree<int>(2);

        tree.AddRange(new[] { 1, 2, 3, 4, 5 });
        tree.Remove(3);
        tree.Add(3);

        tree.Count.Should().Be(5);
        tree.Contains(3).Should().BeTrue();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 4, 5 },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Remove_BorrowFromPreviousSibling_TreeStillValid()
    {
        var tree = new BTree<int>(3);

        tree.AddRange(new[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 });

        tree.Remove(100);

        tree.Count.Should().Be(9);
        tree.Contains(100).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 10, 20, 30, 40, 50, 60, 70, 80, 90 },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Remove_BorrowFromNextSibling_TreeStillValid()
    {
        var tree = new BTree<int>(3);

        tree.AddRange(new[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 });

        tree.Remove(10);

        tree.Count.Should().Be(9);
        tree.Contains(10).Should().BeFalse();

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 20, 30, 40, 50, 60, 70, 80, 90, 100 },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Remove_UsesPredecessor_TreeStillValid()
    {
        var tree = new BTree<int>(3);

        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });

        var rootKeys = tree.GetKeysPreOrder().Take(3).ToArray();
        var keyToRemove = rootKeys[0];

        tree.Remove(keyToRemove);

        tree.Contains(keyToRemove).Should().BeFalse();
        tree.Count.Should().Be(14);

        var inOrder = tree.GetKeysInOrder().ToArray();
        inOrder.Should().HaveCount(14);
        inOrder.Should().BeInAscendingOrder();
    }

    [Test]
    public void Remove_UsesSuccessor_TreeStillValid()
    {
        var tree = new BTree<int>(2);

        tree.AddRange(new[] { 10, 20, 30, 40, 50, 60, 70 });

        tree.Remove(40);

        tree.Contains(40).Should().BeFalse();
        tree.Count.Should().Be(6);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 10, 20, 30, 50, 60, 70 },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Remove_MergeWithSibling_TreeStillValid()
    {
        var tree = new BTree<int>(2);

        tree.AddRange(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

        tree.Remove(9);
        tree.Remove(8);
        tree.Remove(7);

        tree.Count.Should().Be(6);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 3, 4, 5, 6 },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Remove_ComplexRebalancing_TreeStillValid()
    {
        var tree = new BTree<int>(3);

        tree.AddRange(Enumerable.Range(1, 30));

        var keysToRemove = new[] { 5, 15, 25, 10, 20, 30, 1, 29 };

        foreach (var key in keysToRemove)
        {
            tree.Remove(key);
            tree.Contains(key).Should().BeFalse();
        }

        tree.Count.Should().Be(22);

        var expected = Enumerable.Range(1, 30).Except(keysToRemove).OrderBy(x => x);
        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                expected,
                config => config.WithStrictOrdering());
    }

    [Test]
    public void Remove_FromMinimumDegree3_AllRebalancingPaths()
    {
        var tree = new BTree<int>(3);

        tree.AddRange(Enumerable.Range(1, 50));
        var keysToRemove = new[] { 25, 12, 37, 6, 44, 18, 31, 3, 47, 15 };

        foreach (var key in keysToRemove)
        {
            var countBefore = tree.Count;
            tree.Remove(key);

            tree.Count.Should().Be(countBefore - 1);
            tree.Contains(key).Should().BeFalse();

            var inOrder = tree.GetKeysInOrder().ToArray();
            inOrder.Should().BeInAscendingOrder();
        }

        tree.Count.Should().Be(40);
    }

    [Test]
    public void Remove_SequentialFromLargeTree_MaintainsStructure()
    {
        var tree = new BTree<int>(4);

        tree.AddRange(Enumerable.Range(1, 100));
        for (var i = 3; i <= 99; i += 3)
        {
            tree.Remove(i);
        }

        tree.Count.Should().Be(67);

        var inOrder = tree.GetKeysInOrder().ToArray();
        inOrder.Should().HaveCount(67);
        inOrder.Should().BeInAscendingOrder();

        tree.Contains(3).Should().BeFalse();
        tree.Contains(6).Should().BeFalse();
        tree.Contains(99).Should().BeFalse();
        tree.Contains(1).Should().BeTrue();
        tree.Contains(2).Should().BeTrue();
        tree.Contains(100).Should().BeTrue();
    }

    [Test]
    public void BTree_DegreeThree_CompleteInsertDeleteCycle()
    {
        var tree = new BTree<int>(3);

        var keys = Enumerable.Range(1, 40).ToArray();
        tree.AddRange(keys);
        tree.Count.Should().Be(40);

        for (var i = 2; i <= 40; i += 2)
        {
            tree.Remove(i);
        }

        tree.Count.Should().Be(20);

        var remaining = tree.GetKeysInOrder().ToArray();
        remaining.Should().BeEquivalentTo(
            Enumerable.Range(1, 40).Where(x => x % 2 != 0),
            config => config.WithStrictOrdering());

        tree.Add(2);
        tree.Add(4);
        tree.Count.Should().Be(22);

        tree.Contains(2).Should().BeTrue();
        tree.Contains(4).Should().BeTrue();
    }

    [Test]
    public void Remove_RootWithMultipleChildren_HandledCorrectly()
    {
        var tree = new BTree<int>(2);

        tree.AddRange(new[] { 1, 2, 3, 4, 5 });
        tree.Remove(3);
        tree.Count.Should().Be(4);

        tree.GetKeysInOrder()
            .Should()
            .BeEquivalentTo(
                new[] { 1, 2, 4, 5 },
                config => config.WithStrictOrdering());
    }

    [Test]
    public void BTree_HighDegree_StressTest()
    {
        var tree = new BTree<int>(10);

        tree.AddRange(Enumerable.Range(1, 200));
        tree.Count.Should().Be(200);

        for (var i = 10; i <= 200; i += 10)
        {
            tree.Remove(i);
        }

        tree.Count.Should().Be(180);

        var inOrder = tree.GetKeysInOrder().ToArray();
        inOrder.Should().HaveCount(180);
        inOrder.Should().BeInAscendingOrder();

        tree.Contains(10).Should().BeFalse();
        tree.Contains(100).Should().BeFalse();
        tree.Contains(200).Should().BeFalse();
    }
}
