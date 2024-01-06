using System;
using System.Collections.Generic;

namespace DataStructures.Tries;

/// <summary>
/// This class represents the nodes of a trie.
/// </summary>
internal class TrieNode
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TrieNode"/> class. This instance was created with a character from the alphabet, and its parent will be null.
    /// </summary>
    /// <param name="value">Character of the alphabet that represents the node.</param>
    internal TrieNode(char value)
        : this(value, null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TrieNode"/> class. This instance was created with a character from the alphabet, and its parent.
    /// </summary>
    /// <param name="value">Character of the alphabet that represents the node.</param>
    /// <param name="parent">The parent or ancestor of the node in the trie structure.</param>
    internal TrieNode(char value, TrieNode? parent)
    {
        Children = new SortedList<char, TrieNode>();
        Parent = parent;
        Value = value;
    }

    /// <summary>
    /// Gets all the descendants of the current node.
    /// </summary>
    /// <value>A sorted set with all the descendants.</value>
    internal SortedList<char, TrieNode> Children { get; private set; }

    /// <summary>
    /// Gets the parent or ancestor of the node in the trie structure.
    /// </summary>
    /// <value>A TrieNode that represent a parent.</value>
    internal TrieNode? Parent { get; private set; }

    /// <summary>
    /// Gets the character of the alphabet that represents the node.
    /// </summary>
    /// <value>A character of the alphabet.</value>
    internal char Value { get; private set; }

    /// <summary>
    /// Index the descendants of the current node given an alphabet character.
    /// </summary>
    /// <value>A TrieNode with the character c in Children.</value>
    public TrieNode? this[char c]
    {
        get => Children.ContainsKey(c) ? Children[c] : null;
        set => Children[c] = value ?? throw new NullReferenceException();
    }

    /// <summary>
    /// Method that checks if the current node is a trie leaf.
    /// </summary>
    /// <returns>Returns true if the current node has no children, false otherwise.</returns>
    public bool IsLeaf()
    {
        return Children.Count == 0;
    }
}
