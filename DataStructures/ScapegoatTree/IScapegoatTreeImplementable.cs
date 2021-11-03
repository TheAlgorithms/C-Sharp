using System;
using System.Collections.Generic;

namespace DataStructures.ScapegoatTree
{
    public interface IScapegoatTreeImplementable<TKey>
        where TKey : IComparable
    {
        Node<TKey> RebuildWithRoot(Node<TKey> root);

        Node<TKey>? SearchWithRoot(Node<TKey> root, TKey key);

        bool TryDeleteWithRoot(Node<TKey> root, TKey key);

        bool TryInsertWithRoot(Node<TKey> root, Node<TKey> node, Stack<Node<TKey>> path);

        (Node<TKey>? parent, Node<TKey> subtree) RebuildFromPath(double alpha, Stack<Node<TKey>> path);
    }
}
