using System;
using DataStructures.Tries;
using NUnit.Framework;

namespace DataStructures.Tests.Tries
{
    public static class TrieTests
    {
        [Test]
        public static void FindWordInTrie(){
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
            Assert.IsTrue(trie.Find("trie"), "The word 'trie' isn't in Trie structure");
            Assert.IsTrue(trie.Find("node"), "The word 'node' isn't in Trie structure");
            Assert.IsTrue(trie.Find("none"), "The word 'none' isn't in Trie structure");
            Assert.IsTrue(trie.Find("treatment"), "The word 'treatment' isn't in Trie structure");

            Assert.IsFalse(trie.Find("nodes"), "The word 'nodes' is in Trie sturcture");
            Assert.IsFalse(trie.Find(""), "The word empty is in Trie structure");
            Assert.IsFalse(trie.Find("tri"), "The word 'tri' is in Trie structure");
        }

        [Test]
        public static void InsertInTrie(){
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
            Assert.IsTrue(trie.Find("trie"), "The word 'trie' isn't in Trie structure");
            Assert.IsTrue(trie.Find("node"), "The word 'node' isn't in Trie structure");
            Assert.IsTrue(trie.Find("none"), "The word 'none' isn't in Trie structure");
            Assert.IsTrue(trie.Find("treatment"), "The word 'treatment' isn't in Trie structure");
        }

        [Test]
        public static void RemoveFromTrie(){
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
            Assert.IsFalse(trie.Find("trie"), "The word 'trie' is in Trie structure");
            Assert.IsTrue(trie.Find("treatment"), "The word 'treament' isn't in Trie structure");
            Assert.IsTrue(trie.Find("node"), "The word 'node' isn't in Trie structure");
            Assert.IsTrue(trie.Find("none"), "The word 'none' isn't in Trie structure");
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
            Assert.IsTrue(trie.Find("trie"), "The word 'trie' isn't in Trie structure");
            Assert.IsFalse(trie.Find("nodes"), "The word 'nodes' is in Trie sturcture");
        }

        [Test]
        public static void RemoveAWordThatIsNtInTrie(){
            // Arrange
            const string w = "trie";
            Trie trie = new();

            // Act
            trie.Insert(w);
            trie.Remove("tri");
            trie.Remove("none");

            // Assert
            Assert.IsTrue(trie.Find("trie"), "The word 'trie' isn't in Trie structure");
        }
    }
}
