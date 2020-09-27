using System;

namespace DataStructures.SegmentTrees
{
    /// <summary>
    /// This is an extension of a segment tree, which allows to apply distributive operations to a subarray (in this case muliplication).
    /// </summary>
    public class SegmentTreeApply : SegmentTree
    {
        /// <summary>
        /// Initializes the segment tree.
        /// Runtime complexity: O(n) where n equals the array-length
        /// </summary>
        /// <param name="arr">Array on which the operations should be made.</param>        
        public SegmentTreeApply(int[] arr)
        : base(arr)
        {
            // Initilizes and fills "operation" array with neutral element (in this case 1, because x * 1 = x)
            Operation = new int[Tree.Length];
            Array.Fill<int>(Operation, 1);
        }

        /// <summary>
        /// In each node we store an operation, which must be applied to all direct and indirect child nodes of this node 
        /// (but not to the node itself).
        /// </summary>
        public int[] Operation { get; }

        /// <summary>
        /// Applies a distributiv operation to a subarray definied by <c>l</c> and <c>r</c>
        /// (in this case multiplication by <c>value</c>).
        /// Runtime complexity: O(logN) where N equals the initial array-length.
        /// </summary>
        /// <param name="l">Left border of the subarray.</param>
        /// <param name="r">Right border of the subarray.</param>
        /// <param name="value">Value with which each element of the interval is calculated.</param>
        public void Apply(int l, int r, int value)
        {
            // The Application start at node with 1
            // Node with index 1 includes the whole input subarray
            Apply(++l, ++r, value, 1, Tree.Length / 2, 1);
        }

        /// <summary>
        /// Edits a query.
        /// </summary>
        /// <param name="l">Left border of the query.</param>
        /// <param name="r">Right border of the query.</param>
        /// <param name="a">Left end of the subarray enclosed by <c>i</c>.</param>
        /// <param name="b">Right end of the subarray enclosed by <c>i</c>.</param>
        /// <param name="i">Current node.</param>
        /// <returns>Sum of a subarray between <c>l</c> and <c>r</c> (including <c>l</c> and <c>r</c>).<returns>
        protected override int Query(int l, int r, int a, int b, int i)
        {
            if (l <= a && b <= r)
            {
                return Tree[i];
            }

            if (r < a || b < l)
            {
                return 0;
            }

            int m = (a + b) / 2;
            // Application of the saved operation to the direct and indrect child nodes
            return Operation[i] * (Query(l, r, a, m, Left(i)) + Query(l, r, m + 1, b, Right(i)));
        }

        /// <summary>
        /// Applies the operation.
        /// </summary>
        /// <param name="l">Left border of the Application.</param>
        /// <param name="r">Right border of the Application.</param>
        /// <param name="a">Left end of the subarray enclosed by <c>i</c>.</param>
        /// <param name="b">Right end of the subarray enclosed by <c>i</c>.</param>
        /// <param name="i">Current node.</param>
        private void Apply(int l, int r, int x, int a, int b, int i)
        {
            // If a and b are in the (by l and r) specified subarray
            if (l <= a && b <= r)
            {
                // Applies the operation to the current node and saves it for the direct and indirect child nodes
                Operation[i] = x * Operation[i];
                Tree[i] = x * Tree[i];
                return;
            }

            // If a or b are out of the by l and r specified subarray stop application at this node
            if (r < a || b < l)
            {
                return;
            }

            // Calculates index m of the node that cuts the current subarray in half
            int m = (a + b) / 2;

            // Applies the operation to both halfes
            Apply(l, r, x, a, m, Left(i));
            Apply(l, r, x, m + 1, b, Right(i));

            // Recalculates the value of this node by its (possibly new) children.
            Tree[i] = Operation[i] * (Tree[Left(i)] + Tree[Right(i)]);
        }
    }
}
