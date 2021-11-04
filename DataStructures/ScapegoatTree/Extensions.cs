using System;
using System.Collections.Generic;

namespace DataStructures.ScapegoatTree
{
    public static class Extensions
    {
        public static void FlattenTree<TKey>(Node<TKey> root, List<Node<TKey>> list) where TKey : IComparable
        {
            if (root.Left != null)
            {
                FlattenTree(root.Left, list);
            }

            list.Add(root);

            if (root.Right != null)
            {
                FlattenTree(root.Right, list);
            }
        }

        public static Node<TKey> RebuildFromList<TKey>(IList<Node<TKey>> list, int start, int end)
            where TKey : IComparable
        {
            if (start > end)
            {
                throw new ArgumentException("The parameter's value is invalid.", nameof(start));
            }

            var pivot = Convert.ToInt32(Math.Ceiling(start + (end - start) / 2.0));

            return new Node<TKey>(list[pivot].Key)
            {
                Left = start > (pivot - 1) ? null : RebuildFromList(list, start, pivot - 1),
                Right = (pivot + 1) > end ? null : RebuildFromList(list, pivot + 1, end),
            };
        }
    }
}
