using System;
using System.Collections.Generic;

namespace traversals
{
    class Program
    {
        static void Main(string[] args)
        {
            Random_Ordered_Tree tree = new Random_Ordered_Tree();
            Random rnd = new Random();
            for(int i = 0; i < 10; i++)
            {
                int value = rnd.Next(1,100);

                Console.WriteLine("Inserting {0}", value);
                tree.insert(value);
            }

            int[] tree_path = tree.traverse_tree(tree.root);

            Console.Write("Path: ");
            for(int i = 0; i < tree_path.Length; i++)
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
            Tree<int> node = new Tree<int>();
            node.value = num;

            if(root == null)
            {
                root = node;
            }
            else
            {
                Tree<int> current = root;
                Random rnd = new Random();

                while(current != null)
                {

                     // if value is 0 try to place on left side
                     // if value is 1 then try to place on right side
                    int value = rnd.Next(0,2);

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
            List<int> tree_values = new List<int>();
            Queue<Tree<int>> tree_queue = new Queue<Tree<int>>();

            if(t != null)
            {
                tree_queue.Enqueue(t);
            }

            while(tree_queue.Count != 0)
            {
                Tree<int> node = tree_queue.Dequeue();
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
