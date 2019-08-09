using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Knapsack
{
    /// <summary>
    /// Dynamic Programming Knapsack solver.
    /// </summary>
    /// <typeparam name="T">Type of items in knapsack.</typeparam>
    public class DPKnapsackSolver<T> : IHeuristicKnapsackSolver<T>
    {
        private readonly int maxTableSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="DPKnapsackSolver{T}"/> class.
        /// </summary>
        /// <param name="maxTableSize">Limits the total amount of memory used by
        /// specifying the maximum size of the memoization table. The size of this
        /// table is calculated by multiplying 1 plus the number of items that can be
        /// selected by the maximum capacity of the knapsack and the desired
        /// granularity of that weight.</param>
        /// <example>If 9 items can chosen to fit into a knapsack of
        /// capacity 50.2 and we want the weight to be calculated to 1 decimal
        /// place, then the table size would be 5000 (10 * 500).</example>
        public DPKnapsackSolver(int maxTableSize = 10000000)
        {
            this.maxTableSize = maxTableSize;
        }

        /// <summary>
        /// Returns the knapsack containing the items that
        /// maximize value while not exceeding weight capacity.
        /// </summary>
        /// <param name="items">The list of items from which we select ones to be in the knapsack.</param>
        /// <param name="capacity">The maximum weight capacity of the knapsack to be filled.</param>
        /// <param name="weightSelector">A function that returns the value of the specified item
        /// from the <paramref name="items">items</paramref> list.</param>
        /// <param name="valueSelector">A function that returns the weight of the specified item
        /// from the <paramref name="items">items</paramref> list.</param>
        /// <returns>The array of items that provides the maximum value of the
        /// knapsack without exceeding the specified weight <paramref name="capacity">capacity</paramref>.</returns>
        public T[] Solve(T[] items, double capacity, Func<T, double> weightSelector, Func<T, double> valueSelector)
        {
            if (DetermineTableSize(capacity, items.Count()) > this.maxTableSize)
            {
                throw new InvalidOperationException($"{items.Count()} items with a knapsack capacity of {capacity} would result in a memoization table size larger than the configured value of {this.maxTableSize}.");
            }

            int decimalPlacesToAdjustWeight = CalculateDecimalPlaceAccuracy(this.maxTableSize, items.Count());

            int maxCapacity = Convert.ToInt32(Math.Ceiling(capacity * Math.Pow(10, decimalPlacesToAdjustWeight)));
            Func<T, int> adjustedWeightSelector = x => Convert.ToInt32(Math.Round(weightSelector(x) * Math.Pow(10, decimalPlacesToAdjustWeight)));

            var memoTable = Memoize(items, adjustedWeightSelector, valueSelector, maxCapacity);
            return GetOptimalItems(items, adjustedWeightSelector, memoTable, maxCapacity);
        }

        private static int DetermineTableSize(double capacity, int itemCount)
        {
            return Convert.ToInt32(Math.Ceiling(capacity)) * (itemCount + 1);
        }

        private static int CalculateDecimalPlaceAccuracy(int maxTableSize, int numberOfItems)
        {
            // TODO: Implement for real
            return 0;
        }

        private static T[] GetOptimalItems(T[] items, Func<T, int> weightSelector, double[,] memoTable, int capacity)
        {
            int currentCapacity = capacity;

            var result = new List<T>();
            for (int i = items.Count() - 1; i >= 0; i--)
            {
                if (memoTable[i + 1, currentCapacity] > memoTable[i, currentCapacity])
                {
                    var item = items[i];
                    result.Add(item);
                    currentCapacity -= weightSelector(item);
                }
            }

            result.Reverse(); // we added items back to front
            return result.ToArray();
        }

        private static double[,] Memoize(T[] items, Func<T, int> weightSelector, Func<T, double> valueSelector, int maxCapacity)
        {
            // Memoize in a bottom up manner
            int n = items.Count();
            var rv = new double[n + 1, maxCapacity + 1];
            for (var i = 0; i <= n; i++)
            {
                for (var w = 0; w <= maxCapacity; w++)
                {
                    if (i == 0 || w == 0)
                    {
                        // If we have no items to take, or
                        // if we have no capacity in our knapsack
                        // we cannot possibly have any value
                        rv[i, w] = 0;
                    }
                    else if (weightSelector(items[i - 1]) <= w)
                    {
                        // Decide if it is better to take or not take this item
                        var iut = items[i - 1]; // iut = Item under test
                        var vut = valueSelector(iut); // vut = Value of item under test
                        var wut = weightSelector(iut); // wut = Weight of item under test
                        var valueIfTaken = vut + rv[i - 1, w - wut];
                        var valueIfNotTaken = rv[i - 1, w];
                        rv[i, w] = Math.Max(valueIfTaken, valueIfNotTaken);
                    }
                    else
                    {
                        // There is not enough room to take this item
                        rv[i, w] = rv[i - 1, w];
                    }
                }
            }

            return rv;
        }
    }
}
