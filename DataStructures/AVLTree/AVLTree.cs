using System;
using System.Collections.Generic;

namespace DataStructures.AVLTree
{
    public class AVLTree<TKey>
    {
        public uint Count { get; private set; }

        private AVLTreeNode<TKey>? root;
        private readonly Comparer<TKey> comparer;


        public AVLTree()
        {
            root = null;
            comparer = Comparer<TKey>.Default;
            Count = 0;
        }

        public AVLTree(Comparer<TKey> customComparer)
        {
            root = null;
            comparer = customComparer;
            Count = 0;
        }

        public void Add(TKey key)
        {
            if (root is null)
            {
                root = new AVLTreeNode<TKey>(key);
            }
            else
            {
                Add(root, key);
            }
        }

        public void AddRange(IEnumerable<TKey> keys)
        {

        }


        private void Add(AVLTreeNode<TKey> node, TKey key)
        {
            int compareResult = comparer.Compare(node.Key, key);
            if (compareResult < 0)
            {
                if (node.Left is null)
                {
                    var newNode = new AVLTreeNode<TKey>(key);
                    node.Left = newNode;
                }
                else
                {
                    Add(node.Left, key);
                }
            }
            else if (compareResult > 0)
            {
                if (node.Right is null)
                {
                    var newNode = new AVLTreeNode<TKey>(key);
                    node.Right = newNode;
                }
                else
                {
                    Add(node.Right, key);
                }
            }
            else
            {
                throw new ArgumentException($"Key \"{key}\" already exists in tree!");
            }

            sbyte rightBF = 0,
                  leftBF = 0;
            if (node.Right is not null)
            {

            }
        }
    }
}
