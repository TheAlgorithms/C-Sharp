using Algorithms.Graph;
using NUnit.Framework;
using DataStructures.BinarySearchTree;
using System;

namespace Algorithms.Tests.Graph
{
    public static class BreadthFirstTreeTraversalTests
    {
        [Test]
        [TestCase(new int[] { 5, 4, 6 })]
        public static void levelOrderTraversal_TrueExpected(int[] path)
        {
            // Set up binary search tree
            BinarySearchTree<int> testTree = new BinarySearchTree<int>();
            foreach (int data in path)
            {
                testTree.Add(data);
            }
            int[] levelOrder = BreadthFirstTreeTraversal<int>.LevelOrderTraversal(testTree);

            // Assert
            Assert.AreEqual(levelOrder, path);
        }
    }
}

