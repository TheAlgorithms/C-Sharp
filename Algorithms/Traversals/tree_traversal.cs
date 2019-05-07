using System;
using System.Collections.Generic;

namespace traversals
{
    class Program
    {
        static void Main(string[] args)
        {
            var tree = new Random_Ordered_Tree();
            var rnd = new Random();
            for(var i = 0; i < 10; i++)
            {
                var value = rnd.Next(1,100);

                Console.WriteLine("Inserting {0}", value);
                tree.insert(value);
            }

            var tree_path = tree.traverse_tree(tree.root);

            Console.Write("Path: ");
            for(var i = 0; i < tree_path.Length; i++)
            {
                Console.Write("{0} ", tree_path[i]);
            }
        }
    }

    class Tree<T>
    {
        public int value {get; set;}
        public Tree<T> left {get; set;}
        public Tree<T> right {get; set;}
    }

    class Random_Ordered_Tree
    {
        public Tree<int> root = null;

        public void insert(int num)
        {
            var node = new Tree<int>();
            node.value = num;

            if(root == null)
            {
                root = node;
            }
            else
            {
                var current = root;
                var rnd = new Random();

                while(current != null)
                {

                     // if value is 0 try to place on left side
                     // if value is 1 then try to place on right side
                    var value = rnd.Next(0,2);

                    // Case for Left Side
                    if(value == 0)
                    {
                        if(current.left != null)
                        {
                            current = current.left;
                        }
                        else
                        {
                            current.left = node;
                            break;
                        }
                    }

                    // Case for right Side
                    else
                    {
                        if(current.right != null)
                        {
                            current = current.right;
                        }
                        else
                        {
                            current.right = node;
                            break;
                        }
                    }
                }
            }
        }

        public int[] traverse_tree(Tree<int> t)
        {
            var tree_values = new List<int>();
            var tree_queue = new Queue<Tree<int>>();

            if(t != null)
            {
                tree_queue.Enqueue(t);
            }

            while(tree_queue.Count != 0)
            {
                var node = tree_queue.Dequeue();
                tree_values.Add(node.value);

                if(node.left != null)
                {
                    tree_queue.Enqueue(node.left);
                }

                if(node.right != null)
                {
                    tree_queue.Enqueue(node.right);
                }
            }
            return tree_values.ToArray();
        }
    }
}
