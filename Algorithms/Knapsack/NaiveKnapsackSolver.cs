namespace Algorithms.Knapsack;

/// <summary>
///     Greedy heurictic solver.
/// </summary>
/// <typeparam name="T">Type of items in knapsack.</typeparam>
public class NaiveKnapsackSolver<T> : IHeuristicKnapsackSolver<T>
{
    /// <summary>
    ///     Solves the knapsack problem using a naive greedy approach.
    ///     Items are added in order until capacity is reached.
    /// </summary>
    /// <param name="items">Array of items to consider for the knapsack.</param>
    /// <param name="capacity">Maximum weight capacity of the knapsack.</param>
    /// <param name="weightSelector">Function to get the weight of an item.</param>
    /// <param name="valueSelector">Function to get the value of an item.</param>
    /// <returns>Array of items that fit in the knapsack.</returns>
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
