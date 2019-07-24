using System;
using System.Collections.Generic;

namespace Algorithms.Traversals
{
    internal class Random_Ordered_Tree
    {
        public Tree<int> Root { get; set; }

        public void Insert(int num)
        {
            var node = new Tree<int>
            {
                Value = num,
            };

            if (Root == null)
            {
                Root = node;
            }
            else
            {
                var current = Root;
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
