using System;
using System.Collections.Generic;

namespace traversals
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

            var tree_path = tree.Traverse_tree(tree.root);

            Console.Write("Path: ");
            for (var i = 0; i < tree_path.Length; i++)
            {
                Console.Write("{0} ", tree_path[i]);
            }
        }
    }

    internal class Tree<T>
    {
        public int Value { get; set; }
        public Tree<T> Left { get; set; }
        public Tree<T> Right { get; set; }
    }

    internal class Random_Ordered_Tree
    {
        public Tree<int> root = null;

        public void Insert(int num)
        {
            var node = new Tree<int>
            {
                Value = num,
            };

            if (root == null)
            {
                root = node;
            }
            else
            {
                var current = root;
                var rnd = new Random();

                while (current != null)
                {
                    // if value is 0 try to place on left side
                    // if value is 1 then try to place on right side
                    var value = rnd.Next(0, 2);

                    // Case for Left Side
                    if (value == 0)
                    {
                        if (current.Left != null)
                        {
                            current = current.Left;
                        }
                        else
                        {
                            current.Left = node;
                            break;
                        }
                    }

                    // Case for right Side
                    else
                    {
                        if (current.Right != null)
                        {
                            current = current.Right;
                        }
                        else
                        {
                            current.Right = node;
                            break;
                        }
                    }
                }
            }
        }

        public int[] Traverse_tree(Tree<int> t)
        {
            var tree_values = new List<int>();
            var tree_queue = new Queue<Tree<int>>();

            if (t != null)
            {
                tree_queue.Enqueue(t);
            }

            while (tree_queue.Count != 0)
            {
                var node = tree_queue.Dequeue();
                tree_values.Add(node.Value);

                if (node.Left != null)
                {
                    tree_queue.Enqueue(node.Left);
                }

                if (node.Right != null)
                {
                    tree_queue.Enqueue(node.Right);
                }
            }

            return tree_values.ToArray();
        }
    }
}
