using System;
using System.Collections.Generic;

namespace Algorithms.Knapsack
{
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
            // parent --> parent node which represents the previous item, may or may not be taken into the knapsack
            BranchAndBoundNode parent;

            // Nodes --> store the nodes of the tree
            List<BranchAndBoundNode> nodes = new List<BranchAndBoundNode>();

            // lastNodeOfOptimalPat --> last item in the optimal item sets identified by this algorithm
            BranchAndBoundNode lastNodeOfOptimalPath = new BranchAndBoundNode();

            // nodesQueue --> used to construct tree in breadth first order
            Queue<BranchAndBoundNode> nodesQueue = new Queue<BranchAndBoundNode>();

            // maxCumulativeValue --> maximum value while not exceeding weight capacity.
            double maxCumulativeValue = 0.0;

            // starting node, associated with a temporary created dummy item
            BranchAndBoundNode root = new BranchAndBoundNode();

            root.Level = -1;
            root.CumulativeWeight = 0;
            root.CumulativeValue = 0;

            nodesQueue.Enqueue(root);

            int counter = 0;

            while (nodesQueue.Count != 0)
            {
                parent = nodesQueue.Dequeue();

                // IF it is the last level
                if (parent.Level > items.Length - 1)
                {
                    continue;
                }

                // create a child node where the associated item is taken into the knapsack
                nodes.Add(new BranchAndBoundNode(parent.Level + 1, true, parent));

                // create a child node where the associated item is not taken into the knapsack
                nodes.Add(new BranchAndBoundNode(parent.Level + 1, false, parent));

                // Since the associated item on current level is taken for the first node,
                // set the cumulative weight of first node to cumulative weight of parent node + weight of the associated item,
                // set the cumulative value of first node to cumulative value of parent node + value of current level's item.
                nodes[counter].CumulativeWeight = parent.CumulativeWeight + weightSelector(items[nodes[counter].Level]);
                nodes[counter].CumulativeValue = parent.CumulativeValue + valueSelector(items[nodes[counter].Level]);

                // IF cumulative weight is smaller than the weight capacity of the knapsack AND
                // current cumulative value is larger then the current maxCumulativeValue, update the maxCumulativeValue
                if (nodes[counter].CumulativeWeight <= capacity && nodes[counter].CumulativeValue > maxCumulativeValue)
                {
                    maxCumulativeValue = nodes[counter].CumulativeValue;
                    lastNodeOfOptimalPath = nodes[counter];
                }

                // find upperBound of this node
                nodes[counter].UpperBound = ComputeUpperBound(nodes[counter], items, capacity, weightSelector, valueSelector);

                // IF upperBound of this node is larger than maxCumulativeValue,
                // the current path is still possible to reach or surpass the maximum value,
                // add current node to nodesQueue so that nodes below it can be further explored
                if (nodes[counter].UpperBound > maxCumulativeValue && nodes[counter].CumulativeWeight < capacity)
                {
                    nodesQueue.Enqueue(nodes[counter]);
                }

                // repeat everything for the second node but the node's level's item is not taken
                counter++;
                nodes[counter].Parent = parent;

                nodes[counter].CumulativeWeight = parent.CumulativeWeight;
                nodes[counter].CumulativeValue = parent.CumulativeValue;

                nodes[counter].UpperBound = ComputeUpperBound(nodes[counter], items, capacity, weightSelector, valueSelector);

                if (nodes[counter].UpperBound > maxCumulativeValue)
                {
                    nodesQueue.Enqueue(nodes[counter]);
                }

                counter++;
            }

            return GetOptimalItems(items, lastNodeOfOptimalPath, weightSelector, valueSelector);
        }

        // determine items taken based on the path that gives maximum value
        private static T[] GetOptimalItems(T[] items, BranchAndBoundNode lastNodeOfOptimalPath, Func<T, int> weightSelector, Func<T, double> valueSelector)
        {
            List<T> takenItems = new List<T>();

            BranchAndBoundNode? currentNode = lastNodeOfOptimalPath;
            int numberOfNodes = lastNodeOfOptimalPath.Level;

            for (int i = numberOfNodes; i >= 0; i--)
            {
                // the node's level's item is taken, add to knapsack taken items list
                if(currentNode != null)
                {
                    if(currentNode.IsTaken())
                    {
                        takenItems.Add(items[i]);
                    }

                    // set currentNode to its parent to check if the parent is taken in the next iteration
                    currentNode = currentNode.Parent;
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
            double upperBound = aNode.CumulativeValue;
            int availableWeight = capacity - aNode.CumulativeWeight;
            int nextLevel = aNode.Level + 1;

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
}
