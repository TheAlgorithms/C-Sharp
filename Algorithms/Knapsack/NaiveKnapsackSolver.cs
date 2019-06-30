using System;
using System.Collections.Generic;

namespace Algorithms.Knapsack
{
    /// <summary>
    /// Greedy heurictic solver
    /// </summary>
    /// <typeparam name="T">Type of items in knapsack</typeparam>
    public class NaiveKnapsackSolver<T> : IHeuristicKnapsackSolver<T>
    {
        public T[] Solve(T[] items, double capacity, Func<T, double> weightSelector, Func<T, double> valueSelector)
        {
            var weight = 0d;
            var left = new List<T>();

            foreach (var item in items)
            {
                var weightDelta = weightSelector(item);
                if (weight + weightDelta <= capacity)
                {
                    weight += weightDelta;
                    left.Add(item);
                }
            }

            return left.ToArray();
        }
    }
}
