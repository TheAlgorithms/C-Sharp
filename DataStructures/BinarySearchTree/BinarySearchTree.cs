using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.BinarySearchTree
{
    /// <summary>
    /// Node-based binary tree data structure which has the following properties:
    /// - The left subtree of a node contains only nodes with keys lesser than the node’s key
    /// - The right subtree of a node contains only nodes with keys greater than the node’s key
    /// - The left and right subtree each must also be a binary search tree.
    /// Source: https://www.geeksforgeeks.org/binary-search-tree-set-1-search-and-insertion/.
    /// </summary>
     /// <typeparam name="T">Generic type taht have to be comparable.</typeparam>
    public class BinarySearchTree<T>
    {
        /// <summary>
        /// Current root of a subtree.
        /// </summary>
        private Node<T>? rootNode;

        /// <summary>
        /// Function to insert a new node with given key in BST.
        /// </summary>
        /// <param name="value">Inbput key value of type <c>T</c>.</param>
        public void Insert(T value)
        {
            rootNode = Insert(value, rootNode);
        }

        /// <summary>
        /// Function to search a given key in a given BST.
        /// </summary>
        /// <param name="value">Key value of type <c>T</c> to search for.</param>
        public Node<T>? Search(T value) => Search(value, rootNode);

        /// <summary>
        /// Function to delete a key.
        /// </summary>
        /// <param name="value">Key value of type <c>T</c> to remove.</param>
        public void Remove(T value)
        {
            rootNode = Remove(value, rootNode);
        }

        /// <summary>
        /// Function to get the key values in preorder.
        /// </summary>
        /// <returns>List of key values in preorder.</returns>
        public List<T> Preorder() => Preorder(rootNode);

        /// <summary>
        /// Function to get the key values in inorder.
        /// </summary>
        /// <returns>List of key values in inorder.</returns>
        public List<T> Inorder() => Inorder(rootNode);

        /// <summary>
        /// Function to get the key values in postorder.
        /// </summary>
        /// <returns>List of key values in postorder.</returns>
        public List<T> Postorder() => Postorder(rootNode);

        /// <summary>
        /// Function to insert a new node with given key in a subtree.
        /// Returns the new root of this subtree.
        /// </summary>
        /// <param name="value">Key value of type <c>T</c> you're searching for.</param>
        /// <param name="node">Current node.</param>
        /// <returns>New root of this subtree.</returns>
        private Node<T> Insert(T value, Node<T>? node)
        {
            if (node is null)
            {
                // If node is null, the subtree is empty
                // Returns a new node as the new root of this new subtree
                return new Node<T>(value);
            }

            // Recur down the next subtree under consideration of the BTS properties
            Comparer<T> defComp = Comparer<T>.Default;
            if (defComp.Compare(value, node.Value) < 0)
            {
                node.Left = Insert(value, node.Left);
            }
            else
            {
                node.Right = Insert(value, node.Right);
            }

            // Returns the (unchanged) node as root of this subtree
            return node;
        }

        /// <summary>
        /// Function to search a given key in a given BST.
        /// </summary>
        /// <param name="value">Key value of type <c>T</c> you're searching for.</param>
        /// <param name="node">Current node.</param>
        /// <returns>Found node.</returns>
        private Node<T>? Search(T value, Node<T>? node)
        {
            if (node is null)
            {
                // If node is null, the subtree is empty
                // Returns an empty note, because it couldn't find the value
                return node;
            }

            // Recur down the next subtree under consideration of the BTS properties
            Comparer<T> defComp = Comparer<T>.Default;
            if (defComp.Compare(value, node.Value) < 0)
            {
                // The value is smaller than the current node
                return Search(value, node.Left);
            }
            else if (defComp.Compare(value, node.Value) > 0)
            {
                // The value is bigger than the current node
                return Search(value, node.Right);
            }
            else
            {
                // If it isn't smaller or bigger it equals the current node
                return node;
            }
        }

        /// <summary>
        /// Function deletes the key and returns the new root of this subtree.
        /// </summary>
        /// <param name="value">Key value of type <c>T</c> you're searching for.</param>
        /// <param name="node">Current node.</param>
        /// <returns>New root of this subtree.</returns>
        private Node<T>? Remove(T value, Node<T>? node)
        {
            if (node is null)
            {
                // If node is null, the subtree is empty
                // Returns an empty note, because it couldn't find the key
                return node;
            }

            // Recur down the next subtree under consideration of the BTS properties
            Comparer<T> defComp = Comparer<T>.Default;
            if (defComp.Compare(value, node.Value) < 0)
            {
                // The value is smaller than the current node
                node.Left = Remove(value, node.Left);
            }
            else if (defComp.Compare(value, node.Value) > 0)
            {
                // The value is bigger than the current node
                node.Right = Remove(value, node.Right);
            }
            else
            {
                // If it isn't smaller or bigger it equals the current node

                // Node with only one child
                if (node.Left is null)
                {
                    return node.Right;
                }
                else if (node.Right is null)
                {
                    return node.Left;
                }
                else
                {
                    // Node with two children: Get the smallest node in the right subtree (inorder successor)
                    Node<T> temp = MinValueNode(node.Right);

                    // Copy the inorder successor's value to the current node and delete the inorder successor
                    node.Value = temp.Value;
                    node.Right = Remove(temp.Value, node.Right);
                }
            }

            // Returns the current node
            return node;
        }

        /// <summary>
        /// Found the node with the minium key value in this tree.
        /// It is similar to the leftmost node.
        /// </summary>
        /// <param name="node">Current node.</param>
        /// <returns>Leftmost node in this tree.</returns>
        private Node<T> MinValueNode(Node<T> node)
        {
            if (node.Left is null)
            {
                // Current node has no left child
                return node;
            }
            else
            {
                // Otherwise examine leftmost node in this subtree
                return MinValueNode(node.Left);
            }
        }

        /// <summary>
        /// Function to get the key values in preorder.
        /// </summary>
        /// <returns>List of key values in preorder.</returns>
        private List<T> Preorder(Node<T>? node)
        {
            List<T> temp = new List<T>();
            if (node != null)
            {
                // First add current node to the list
                temp.Add(node.Value);

                // Second add the left subtree in preorder to the list
                temp.AddRange(Preorder(node.Left));

                // Third add the right subtree in preorder to the list
                temp.AddRange(Preorder(node.Right));
            }

            return temp;
        }

        /// <summary>
        /// Function to get the key values in inorder.
        /// </summary>
        /// <returns>List of key values in inorder.</returns>
        private List<T> Inorder(Node<T>? node)
        {
            List<T> temp = new List<T>();
            if (node != null)
            {
                // First add the left subtree in preorder to the list
                temp.AddRange(Inorder(node.Left));

                // Second add current node to the list
                temp.Add(node.Value);

                // Third add the right subtree in preorder to the list
                temp.AddRange(Inorder(node.Right));
            }

            return temp;
        }

        /// <summary>
        /// Function to get the key values in postorder.
        /// </summary>
        /// <returns>List of key values in postorder.</returns>
        private List<T> Postorder(Node<T>? node)
        {
            List<T> temp = new List<T>();
            if (node != null)
            {
                // First add the left subtree in preorder to the list
                temp.AddRange(Postorder(node.Left));

                // Second add the right subtree in preorder to the list
                temp.AddRange(Postorder(node.Right));

                // Third add current node to the list
                temp.Add(node.Value);
            }

            return temp;
        }
    }
}
