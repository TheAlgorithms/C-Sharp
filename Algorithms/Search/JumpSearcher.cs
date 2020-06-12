using System;
namespace Algorithms.Search {

    /// <summary>
    /// The idea: This is one of the public search algorithm.
    /// It takes 2 argument array of numbers and value of number
    /// It finds index of this value in given array
    ///     Time compl is O(root(n))
    /// Note: the array has to be sorted beforehand.
    /// </summary>
    public class JumpSearcher {
        /// <summary>
        /// Code is really easy we find step size first
        /// Later we find the block where this element should be.!
        /// In this block we go search for value.! If found return index
        /// Otherwise return -1!
        /// </summary>
        private int JumpSearch (int[] arr, int elem) {
            int size = arr.Length;
            int step = (int) Math.Floor (Math.Sqrt (elem));

            int prev = 0;
            int new_step = step;
            while (elem > arr[Math.Min (step, size) - 1]) {
                prev = new_step;
                new_step = prev + (int) Math.Floor (Math.Sqrt(size));
                if (arr.Length <= prev) {
                    return -1;
                }
            }
            step = new_step;
            while (elem > arr[prev]) {
                prev++;
                int my_min = Math.Min(step, size);
                if (prev == my_min) {
                    return -1;
                }
            }

            if (arr[prev] == elem) {
                return prev;
            }

            return -1;
        }
    }
}