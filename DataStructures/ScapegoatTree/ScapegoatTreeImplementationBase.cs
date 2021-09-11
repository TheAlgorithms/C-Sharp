using System;
using System.Collections.Generic;

namespace DataStructures.ScapegoatTree
{
    public abstract class ScapegoatTreeImplementationBase<TKey>
        where TKey : IComparable
    {
        public abstract Node<TKey> RebuildWithRoot(Node<TKey> root);

        public abstract Node<TKey>? SearchWithRoot(Node<TKey> root, TKey key);

        public abstract bool TryDeleteWithRoot(Node<TKey> root, TKey key);

        public abstract bool TryInsertWithRoot(Node<TKey> root, Node<TKey> node, Queue<Node<TKey>> path);

        public abstract (Node<TKey>? parent, Node<TKey> subtree) RebuildFromPath(double alpha, Queue<Node<TKey>> path);
    }
}
