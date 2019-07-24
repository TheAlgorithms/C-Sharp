using System;
using System.Collections.Generic;

namespace Algorithms.Traversals
{
    internal class Tree<T>
    {
        public int Value { get; set; }

        public Tree<T> Left { get; set; }

        public Tree<T> Right { get; set; }
    }
}
