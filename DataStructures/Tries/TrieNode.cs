using System;
using System.Collections.Generic;

namespace DataStructures.Tries
{
    /// <summary>
    /// This class represents the nodes of a trie
    /// </summary>
    internal class TrieNode
    {
        /// <summary>
        /// Initialize a node with a character from the alphabet, and its parent will be null
        /// </summary>
        /// <param name="value">Character of the alphabet that represents the node</param>
        internal TrieNode(char value)
            : this(value, null)
        {
        }

        /// <summary>
        /// Initialize a node of the trie given the character and its parent in the trie
        /// </summary>
        /// <param name="value">Character of the alphabet that represents the node</param>
        /// <param name="parent">The parent or ancestor of the node in the trie structure</param>
        internal TrieNode(char value, TrieNode? parent)
        {
            this.Children = new SortedList<char, TrieNode>();
            this.Parent = parent;
            this.Value = value;
        }

        /// <summary>
        /// Descendants of the current node
        /// </summary>
        /// <value></value>
        internal SortedList<char, TrieNode> Children { get; private set; }

        /// <summary>
        /// The parent or ancestor of the node in the trie structure
        /// </summary>
        /// <value></value>
        internal TrieNode? Parent { get; private set; }

        /// <summary>
        /// Character of the alphabet that represents the node
        /// </summary>
        /// <value></value>
        internal char Value { get; private set; }

        /// <summary>
        /// Index the descendants of the current node given an alphabet character
        /// </summary>
        /// <value></value>
        public TrieNode? this[char c]
        {
            get
            {
                return this.Children.ContainsKey(c) ? this.Children[c] : null;
            }

            set
            {
                this.Children[c] = value ?? throw new NullReferenceException();
            }
        }

        /// <summary>
        /// Method that checks if the current node is a trie leaf.
        /// </summary>
        /// <returns>Returns true if the current node has no children, false otherwise</returns>
        public bool IsLeaf()
        {
            return Children.Count == 0;
        }
    }
}
