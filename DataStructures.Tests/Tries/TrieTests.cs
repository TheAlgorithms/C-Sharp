using DataStructures.Tries;
using NUnit.Framework;

namespace DataStructures.Tests.Tries;

public static class TrieTests
{
    [Test]
    public static void FindWordInTrie()
    {
        // Arrange
        string[] words = {
            "trie",
            "node",
            "none",
            "treatment",
        };

        // Act
        Trie trie = new(words);

        // Assert
        Assert.That(trie.Find("trie"), Is.True, "The word 'trie' isn't in Trie structure");
        Assert.That(trie.Find("node"), Is.True, "The word 'node' isn't in Trie structure");
        Assert.That(trie.Find("none"), Is.True, "The word 'none' isn't in Trie structure");
        Assert.That(trie.Find("treatment"), Is.True, "The word 'treatment' isn't in Trie structure");

        Assert.That(trie.Find("nodes"), Is.False, "The word 'nodes' is in Trie sturcture");
        Assert.That(trie.Find(""), Is.False, "The word empty is in Trie structure");
        Assert.That(trie.Find("tri"), Is.False, "The word 'tri' is in Trie structure");
    }

    [Test]
    public static void InsertInTrie()
    {
        // Arrange
        string[] words = {
            "trie",
            "node",
            "none",
            "treatment",
        };

        Trie trie = new();

        // Act
        foreach (var t in words)
        {
            trie.Insert(t);
        }

        // Assert
        Assert.That(trie.Find("trie"), Is.True, "The word 'trie' isn't in Trie structure");
        Assert.That(trie.Find("node"), Is.True, "The word 'node' isn't in Trie structure");
        Assert.That(trie.Find("none"), Is.True, "The word 'none' isn't in Trie structure");
        Assert.That(trie.Find("treatment"), Is.True, "The word 'treatment' isn't in Trie structure");
    }

    [Test]
    public static void RemoveFromTrie()
    {
        // Arrange
        string[] words = {
            "trie",
            "node",
            "none",
            "treatment",
        };

        Trie trie = new();

        // Act
        foreach (var t in words)
        {
            trie.Insert(t);
        }
        trie.Remove("trie");

        // Assert
        Assert.That(trie.Find("trie"), Is.False, "The word 'trie' is in Trie structure");
        Assert.That(trie.Find("treatment"), Is.True, "The word 'treament' isn't in Trie structure");
        Assert.That(trie.Find("node"), Is.True, "The word 'node' isn't in Trie structure");
        Assert.That(trie.Find("none"), Is.True, "The word 'none' isn't in Trie structure");
    }

    [Test]
    public static void MultipleInsert()
    {
        // Arrange
        string w = "trie";
        Trie trie = new();

        // Act
        trie.Insert(w);
        trie.Insert(w);

        // Assert
        Assert.That(trie.Find("trie"), Is.True, "The word 'trie' isn't in Trie structure");
        Assert.That(trie.Find("nodes"), Is.False, "The word 'nodes' is in Trie sturcture");
    }

    [Test]
    public static void RemoveAWordThatIsNtInTrie()
    {
        // Arrange
        const string w = "trie";
        Trie trie = new();

        // Act
        trie.Insert(w);
        trie.Remove("tri");
        trie.Remove("none");

        // Assert
        Assert.That(trie.Find("trie"), Is.True, "The word 'trie' isn't in Trie structure");
    }
}
