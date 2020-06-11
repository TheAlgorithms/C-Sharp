using System;
namespace Algorithms.Search {

    /// <summary>
    /// The idea: This is one of the public search algorithm.
    /// It takes 2 argument array of numbers and value of number
    /// It finds index of this value in given array
    ///     Time compl is O(root(n))
    /// Note: the array has to be sorted beforehand.
    /// </summary>
    public class JumpSearch {
        /// <summary>
        /// Code is really easy we find step size first
        /// Later we find the block where this element should be.!
        /// In this block we go search for value.! If found return index
        /// Otherwise return -1!
        /// </summary>
        private int jumpSearch (int[] arr, int val) {
            double temp = Math.Sqrt (val);
            int step = (int) Math.Floor (temp);

            int old = 0;
            while (val > arr[Math.Min (step, arr.Length) - 1]) {
                old = step;
                temp = Math.Sqrt (arr.Length);
                step += (int) Math.Floor (temp);
                if (arr.Length <= old) {
                    return -1;
                }
            }

            while (val > arr[old]) {
                old++;
                if (old == Math.Min (step, arr.Length)) {
                    return -1;
                }
            }

            if (arr[old] == val) {
                return old;
            }

            return -1;
        }
    }
}