using Algorithms.Graph;
using DataStructures.BinarySearchTree;
using NUnit.Framework;

namespace Algorithms.Tests.Graph;

public static class BreadthFirstTreeTraversalTests
{
    [Test]
    public static void CorrectLevelOrderTraversal()
    {
        // Arrange
        int[] correctPath = { 7, 4, 13, 2, 5, 11, 15, 14, 16 };
        int[] insertionOrder = { 7, 13, 11, 15, 14, 4, 5, 16, 2 };
        BinarySearchTree<int> testTree = new BinarySearchTree<int>();
        foreach (int data in insertionOrder)
        {
            testTree.Add(data);
        }

        // Act
        int[] levelOrder = BreadthFirstTreeTraversal<int>.LevelOrderTraversal(testTree);

        // Assert
        Assert.That(correctPath, Is.EqualTo(levelOrder));
    }

    [Test]
    public static void EmptyArrayForNullRoot()
    {
        // Arrange
        BinarySearchTree<int> testTree = new BinarySearchTree<int>();

        // Act
        int[] levelOrder = BreadthFirstTreeTraversal<int>.LevelOrderTraversal(testTree);

        // Assert
        Assert.That(levelOrder, Is.Empty);
    }

    [TestCase(new[] { 7, 9, 5 })]
    [TestCase(new[] { 7, 13, 11, 15, 14, 4, 5, 16, 2 })]
    public static void IncorrectLevelOrderTraversal(int[] insertion)
    {
        // Arrange
        BinarySearchTree<int> testTree = new BinarySearchTree<int>();
        foreach (int data in insertion)
        {
            testTree.Add(data);
        }

        // Act
        int[] levelOrder = BreadthFirstTreeTraversal<int>.LevelOrderTraversal(testTree);

        // Assert
        Assert.That(insertion, Is.Not.EqualTo(levelOrder));
    }

    [Test]
    public static void DeepestNodeInTree()
    {
        // Arrange
        BinarySearchTree<int> testTree = new BinarySearchTree<int>();
        int[] insertion = { 7, 13, 11, 15, 4, 5, 12, 2, 9 };
        foreach (int data in insertion)
        {
            testTree.Add(data);
        }

        // Act
        int deepest = BreadthFirstTreeTraversal<int>.DeepestNode(testTree);

        // Assert
        Assert.That(deepest, Is.EqualTo(12));
    }

    [Test]
    public static void DeepestNodeOfEmptyTree()
    {
        // Arrange
        BinarySearchTree<int?> testTree = new BinarySearchTree<int?>();

        // Act
        int? deepest = BreadthFirstTreeTraversal<int?>.DeepestNode(testTree);

        // Assert
        Assert.That(deepest, Is.Null);
    }
}

