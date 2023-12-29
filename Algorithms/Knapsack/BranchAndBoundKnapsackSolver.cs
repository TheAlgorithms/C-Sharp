using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Knapsack;

/// <summary>
///     Branch and bound Knapsack solver.
/// </summary>
/// <typeparam name="T">Type of items in knapsack.</typeparam>
public class BranchAndBoundKnapsackSolver<T>
{
    /// <summary>
    ///     Returns the knapsack containing the items that maximize value while not exceeding weight capacity.
    ///     Construct a tree structure with total number of items + 1 levels, each node have two child nodes,
    ///     starting with a dummy item root, each following levels are associated with 1 items, construct the
    ///     tree in breadth first order to identify the optimal item set.
    /// </summary>
    /// <param name="items">All items to choose from.</param>
    /// <param name="capacity">The maximum weight capacity of the knapsack to be filled.</param>
    /// <param name="weightSelector">
    ///     A function that returns the value of the specified item
    ///     from the <paramref name="items">items</paramref> list.
    /// </param>
    /// <param name="valueSelector">
    ///     A function that returns the weight of the specified item
    ///     from the <paramref name="items">items</paramref> list.
    /// </param>
    /// <returns>
    ///     The array of items that provides the maximum value of the
    ///     knapsack without exceeding the specified weight <paramref name="capacity">capacity</paramref>.
    /// </returns>
    public T[] Solve(T[] items, int capacity, Func<T, int> weightSelector, Func<T, double> valueSelector)
    {
        // This is required for greedy approach in upper bound calculation to work.
        items = items.OrderBy(i => valueSelector(i) / weightSelector(i)).ToArray();

        // nodesQueue --> used to construct tree in breadth first order
        Queue<BranchAndBoundNode> nodesQueue = new();

        // maxCumulativeValue --> maximum value while not exceeding weight capacity.
        var maxCumulativeValue = 0.0;

        // starting node, associated with a temporary created dummy item
        BranchAndBoundNode root = new(level: -1, taken: false);

        // lastNodeOfOptimalPat --> last item in the optimal item sets identified by this algorithm
        BranchAndBoundNode lastNodeOfOptimalPath = root;

        nodesQueue.Enqueue(root);

        while (nodesQueue.Count != 0)
        {
            // parent --> parent node which represents the previous item, may or may not be taken into the knapsack
            BranchAndBoundNode parent = nodesQueue.Dequeue();

            // IF it is the last level, branching cannot be performed
            if (parent.Level == items.Length - 1)
            {
                continue;
            }

            // create a child node where the associated item is taken into the knapsack
            var left = new BranchAndBoundNode(parent.Level + 1, true, parent);

            // create a child node where the associated item is not taken into the knapsack
            var right = new BranchAndBoundNode(parent.Level + 1, false, parent);

            // Since the associated item on current level is taken for the first node,
            // set the cumulative weight of first node to cumulative weight of parent node + weight of the associated item,
            // set the cumulative value of first node to cumulative value of parent node + value of current level's item.
            left.CumulativeWeight = parent.CumulativeWeight + weightSelector(items[left.Level]);
            left.CumulativeValue = parent.CumulativeValue + valueSelector(items[left.Level]);
            right.CumulativeWeight = parent.CumulativeWeight;
            right.CumulativeValue = parent.CumulativeValue;

            // IF cumulative weight is smaller than the weight capacity of the knapsack AND
            // current cumulative value is larger then the current maxCumulativeValue, update the maxCumulativeValue
            if (left.CumulativeWeight <= capacity && left.CumulativeValue > maxCumulativeValue)
            {
                maxCumulativeValue = left.CumulativeValue;
                lastNodeOfOptimalPath = left;
            }

            left.UpperBound = ComputeUpperBound(left, items, capacity, weightSelector, valueSelector);
            right.UpperBound = ComputeUpperBound(right, items, capacity, weightSelector, valueSelector);

            // IF upperBound of this node is larger than maxCumulativeValue,
            // the current path is still possible to reach or surpass the maximum value,
            // add current node to nodesQueue so that nodes below it can be further explored
            if (left.UpperBound > maxCumulativeValue && left.CumulativeWeight < capacity)
            {
                nodesQueue.Enqueue(left);
            }

            // Cumulative weight is the same as for parent node and < capacity
            if (right.UpperBound > maxCumulativeValue)
            {
                nodesQueue.Enqueue(right);
            }
        }

        return GetItemsFromPath(items, lastNodeOfOptimalPath);
    }

    // determine items taken based on the path
    private static T[] GetItemsFromPath(T[] items, BranchAndBoundNode lastNodeOfPath)
    {
        List<T> takenItems = new();

        // only bogus initial node has no parent
        for (var current = lastNodeOfPath; current.Parent is not null; current = current.Parent)
        {
            if(current.IsTaken)
            {
                takenItems.Add(items[current.Level]);
            }
        }

        return takenItems.ToArray();
    }

    /// <summary>
    ///     Returns the upper bound value of a given node.
    /// </summary>
    /// <param name="aNode">The given node.</param>
    /// <param name="items">All items to choose from.</param>
    /// <param name="capacity">The maximum weight capacity of the knapsack to be filled.</param>
    /// <param name="weightSelector">
    ///     A function that returns the value of the specified item
    ///     from the <paramref name="items">items</paramref> list.
    /// </param>
    /// <param name="valueSelector">
    ///     A function that returns the weight of the specified item
    ///     from the <paramref name="items">items</paramref> list.
    /// </param>
    /// <returns>
    ///     upper bound value of the given <paramref name="aNode">node</paramref>.
    /// </returns>
    private static double ComputeUpperBound(BranchAndBoundNode aNode, T[] items, int capacity, Func<T, int> weightSelector, Func<T, double> valueSelector)
    {
        var upperBound = aNode.CumulativeValue;
        var availableWeight = capacity - aNode.CumulativeWeight;
        var nextLevel = aNode.Level + 1;

        while (availableWeight > 0 && nextLevel < items.Length)
        {
            if (weightSelector(items[nextLevel]) <= availableWeight)
            {
                upperBound += valueSelector(items[nextLevel]);
                availableWeight -= weightSelector(items[nextLevel]);
            }
            else
            {
                upperBound += valueSelector(items[nextLevel]) / weightSelector(items[nextLevel]) * availableWeight;
                availableWeight = 0;
            }

            nextLevel++;
        }

        return upperBound;
    }
}
