using System;

namespace Algorithms.Knapsack
{
    /// <summary>
    /// Solves knapsack problem using some heuristics
    /// Sum of values of taken items -> max
    /// Sum of weights of taken items. &lt;= capacity.
    /// </summary>
    /// <typeparam name="T">Type of items in knapsack.</typeparam>
    public interface IHeuristicKnapsackSolver<T>
    {
        /// <summary>
        /// Solves knapsack problem using some heuristics
        /// Sum of values of taken items -> max
        /// Sum of weights of taken items. &lt;= capacity.
        /// </summary>
        /// <param name="items">All items to choose from.</param>
        /// <param name="capacity">How much weight we can take.</param>
        /// <param name="weightSelector">Maps item to its weight.</param>
        /// <param name="valueSelector">Maps item to its value.</param>
        /// <returns>Items that were chosen.</returns>
        T[] Solve(T[] items, double capacity, Func<T, double> weightSelector, Func<T, double> valueSelector);
    }
}
