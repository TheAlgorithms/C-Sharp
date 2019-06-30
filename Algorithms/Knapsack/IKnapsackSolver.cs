namespace Algorithms.Knapsack
{
    /// <summary>
    /// Solves knapsack problem
    /// Sum of values of taken items -> max
    /// Sum of weights of taken items <= capacity
    /// </summary>
    /// <typeparam name="T">Type of items in knapsack</typeparam>
    public interface IKnapsackSolver<T> : IHeuristicKnapsackSolver<T>
    {
    }
}
