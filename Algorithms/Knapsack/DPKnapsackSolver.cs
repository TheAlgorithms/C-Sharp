using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Knapsack
{
    /// <summary>
    /// Dynamic Programming Knapsack solver.
    /// </summary>
    /// <typeparam name="T">Type of items in knapsack.</typeparam>
    public class DPKnapsackSolver<T> : IHeuristicKnapsackSolver<T>
    {
        /// <summary>
        /// Returns the optimal weights for items in the knapsack
        /// so as to maximize value while not exceeding capacity.
        /// </summary>
        /// <param name="items">TODO. 2.</param>
        /// <param name="capacity">TODO. 3.</param>
        /// <param name="weightSelector">TODO. 4.</param>
        /// <param name="valueSelector">TODO. 5.</param>
        /// <returns>TODO. 6.</returns>
        public T[] Solve(T[] items, double capacity, Func<T, double> weightSelector, Func<T, double> valueSelector)
        {
            throw new NotImplementedException();
        }
    }
}
