namespace Algorithms.Knapsack;

/// <summary>
///     Solves knapsack problem:
///     to maximize sum of values of taken items,
///     while sum of weights of taken items is less than capacity.
/// </summary>
/// <typeparam name="T">Type of items in knapsack.</typeparam>
public interface IKnapsackSolver<T> : IHeuristicKnapsackSolver<T>
{
}
