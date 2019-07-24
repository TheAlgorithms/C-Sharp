using System;
using System.Collections.Generic;

namespace Algorithms.Traversals
{
    internal class Program
    {
        private static void Main()
        {
            var tree = new Random_Ordered_Tree();
            var rnd = new Random();
            for (var i = 0; i < 10; i++)
            {
                var value = rnd.Next(1, 100);

                Console.WriteLine("Inserting {0}", value);
                tree.Insert(value);
            }

            var tree_path = tree.Traverse_tree(tree.Root);

            Console.Write("Path: ");
            for (var i = 0; i < tree_path.Length; i++)
            {
                Console.Write("{0} ", tree_path[i]);
            }
        }
    }
}
