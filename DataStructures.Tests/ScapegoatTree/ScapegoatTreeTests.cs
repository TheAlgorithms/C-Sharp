using System;
using DataStructures.ScapegoatTree;
using NUnit.Framework;

namespace DataStructures.Tests.ScapegoatTree
{
    public class ScapegoatTreeTests
    {
        [Test]
        public void Search_RootIsNull_ReturnsNull()
        {
            var tree = new ScapegoatTree<int>();

            var result = tree.Search(1);

            Assert.IsNull(result);
        }

        [Test]
        public void Search_KeyIsPresent_ReturnsKey()
        {
            var tree = new ScapegoatTree<int>(key: 1);

            var result = tree.Search(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result!.Key);
        }

        [Test]
        public void Search_KeyIsNotPresent_ReturnsNull()
        {
            var tree = new ScapegoatTree<int>(key: 1);

            var result = tree.Search(2);

            Assert.IsNull(result);
        }

        [Test]
        public void Insert_RootIsNull_InsertsRoot()
        {
            var tree = new ScapegoatTree<int>();

            var inserted = tree.Insert(1);

            Assert.IsTrue(inserted);
            Assert.IsNotNull(tree.Root);
            Assert.AreEqual(1, tree.Root!.Key);
            Assert.AreEqual(1, tree.Size);
            Assert.AreEqual(1, tree.MaxSize);
        }

        [Test]
        public void Delete_RootIsNull_ReturnsFalse()
        {
            var tree = new ScapegoatTree<int>();

            var deleted = tree.Delete(1);

            Assert.IsFalse(deleted);
        }

        [Test]
        public void Delete_KeyIsNotPresent_ReturnsFalse()
        {
            var tree = new ScapegoatTree<int>(1);

            var deleted = tree.Delete(2);

            Assert.IsFalse(deleted);
            Assert.AreEqual(1, tree.Size);
        }

        [Test]

        public void Insert_KeyIsPresent_DuplicateIsNotInserted()
        {
            var tree = new ScapegoatTree<int>(1);

            var inserted = tree.Insert(1);

            Assert.IsFalse(inserted);
            Assert.AreEqual(1, tree.Size);
            Assert.AreEqual(1, tree.MaxSize);
        }

        [Test]
        public void Remove_KeyIsPresent_RemovesKey()
        {
            var tree = new ScapegoatTree<int>(1);

            var inserted = tree.Insert(2);

            Assert.IsTrue(inserted);

            var deleted = tree.Delete(2);

            Assert.IsTrue(deleted);
            Assert.AreEqual(1, tree.Size);
        }

        [Test]

        public void Insert_KeyIsNotPresent_KeyIsInserted()
        {
            var tree = new ScapegoatTree<int>(1);

            var inserted = tree.Insert(2);

            Assert.IsTrue(inserted);
            Assert.AreEqual(2, tree.Size);
            Assert.AreEqual(2, tree.MaxSize);
        }

        [Test]
        [TestCase(3, new[]{2,5,1,6}, -1, 0.5)]
        public void Insert_TreeIsUnbalanced_RebuildsTree(int root, int[] keys, int candidate, double alpha)
        {
            var tree = new ScapegoatTree<int>(root, alpha);

            tree.TreeIsUnbalanced += TreeIsUnbalanced;

            foreach (var item in keys)
            {
                Assert.DoesNotThrow(() => tree.Insert(item));
            }

            Assert.Throws<InvalidOperationException>(() => tree.Insert(candidate));
        }

        [Test]
        [TestCase(20, new[]{10,30,5,11,29,40,50, 1, 12}, new[]{50,40,30,29}, 0.7)]
        public void Delete_TreeIsUnbalanced_BalancesTree(int root, int[] keys, int[] candidates, double alpha)
        {
            var tree = new ScapegoatTree<int>(root, alpha);

            tree.TreeIsUnbalanced += TreeIsUnbalanced;

            foreach (var item in keys)
            {
                Assert.DoesNotThrow(() => tree.Insert(item));
            }

            Assert.Throws<InvalidOperationException>(() => 
            {
                foreach (var item in candidates)
                {
                    tree.Delete(item);
                }
            });
        }

        [Test]
        [TestCase(3, new[]{2,5,1,6}, -1, 0.5)]
        public void Insert_TreeIsUnbalanced_BalancesTree(int root, int[] keys, int candidate, double alpha)
        {
            var tree = new ScapegoatTree<int>(root, alpha);

            tree.TreeIsUnbalanced += TreeIsUnbalanced;

            foreach (var item in keys)
            {
                Assert.DoesNotThrow(() => tree.Insert(item));
            }

            tree.TreeIsUnbalanced -= TreeIsUnbalanced;

            var inserted = tree.Insert(candidate);

            Assert.True(inserted);
            Assert.True(tree.Size == 6);
            Assert.True(tree.IsAlphaWeightBalanced());
        }


        public static void TreeIsUnbalanced(object? sender, EventArgs? e)
        {
            throw new InvalidOperationException();
        }
    }
}
