using System;
namespace DataStructures.Stack
{
    /// <summary>
    ///     For each element in an array, the utility finds the next greater element on the right side using a stack.
    /// @author Mohit Singh
    /// @author <a href="https://github.com/mohit-gogitter">mohit-gogitter</a>
    /// </summary>
    public class NextGreaterElement
    {
        /// <summary>
        ///     For each element in an array, this method finds the next greater element on the right side using a stack.
        /// </summary>
        /// <param name="nums">Integer Array for which NextGreaterElement needs to be computed</param>
        /// <returns>Integer array containing next greater elements</returns>
        public static int[] FindNextGreaterElement(int[] nums)
        {
            Stack<int> stack = new Stack<int>();
            int[] result = new int[nums.Length];

            for (int i = nums.Length - 1; i >= 0; i--)
            {
                while (stack.Count > 0 && stack.Peek() <= nums[i])
                {
                    stack.Pop();
                }
                result[i] = stack.Count == 0 ? -1 : stack.Peek();
                stack.Push(nums[i]);
            }
            return result;
        }
    }
}