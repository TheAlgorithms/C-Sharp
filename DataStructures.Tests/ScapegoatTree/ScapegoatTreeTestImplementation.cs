using System;
using System.Collections.Generic;
using DataStructures.ScapegoatTree;

namespace DataStructures.Tests.ScapegoatTree
{
    public class ScapegoatTreeTestImplementation<TKey> : IScapegoatTreeImplementable<TKey>
        where TKey : IComparable
    {
        public (Node<TKey>? parent, Node<TKey> subtree) RebuildFromPath(double alpha, Stack<Node<TKey>> path)
        {
            throw new NotImplementedException();
        }

        public Node<TKey> RebuildWithRoot(Node<TKey> root)
        {
            throw new NotImplementedException();
        }

        public Node<TKey>? SearchWithRoot(Node<TKey> root, TKey key)
        {
            throw new NotImplementedException();
        }

        public bool TryDeleteWithRoot(Node<TKey> root, TKey key)
        {
            throw new NotImplementedException();
        }

        public bool TryInsertWithRoot(Node<TKey> root, Node<TKey> node, Stack<Node<TKey>> path)
        {
            throw new NotImplementedException();
        }
    }
}
