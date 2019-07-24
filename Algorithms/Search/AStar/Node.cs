using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace AStar
{
    /// <summary>
    /// A Node(former Location)
    /// Contains Positional and other information about a single node.
    /// </summary>
    public class Node : IComparable<Node>
    {
        // Constructors
        public Node(VecN position, Node p, bool traversable, float travMulti = 1f)
        {
            Traversable = traversable;
            Position = position;
            Parent = p;
            TraversalCostMultiplier = travMulti;
            // G = p?.G + 1 ?? 0;

            // H = Program.ComputeHScore(X, Y);
            // F = G + H;
        }

        // Properties

        /// <summary>
        /// The Total cost of the Node.
        /// The Current Costs + the estimated costs.
        /// </summary>
        public float TotalCost => EstimatedCost + CurrentCost;

        /// <summary>
        /// The Distance between this node and the target node.
        /// </summary>
        public float EstimatedCost
        {
            get;
            set;
        }

        /// <summary>
        /// Will make it more costly to move over this node.
        /// </summary>
        public float TraversalCostMultiplier
        {
            get;
        }

        /// <summary>
        /// The costs it took to go from the start node to this node.
        /// </summary>
        public float CurrentCost
        {
            get;
            set;
        }

        /// <summary>
        /// The state of the Node
        /// Can be Unconsidered(Default), Open and Closed.
        /// </summary>
        public NodeState State
        {
            get;
            set;
        }

        /// <summary>
        /// Determines if the Node is traversable at all.
        /// </summary>
        public bool Traversable
        {
            get;
        }

        /// <summary>
        /// A list of all connected nodes.
        /// </summary>
        public Node[] ConnectedNodes
        {
            get;
            set;
        }

        /// <summary>
        /// The "previous" node that was processed before this node.
        /// </summary>
        public Node Parent
        {
            get;
            set;
        }

        /// <summary>
        /// The positional information of the node.
        /// </summary>
        public VecN Position
        {
            get;
        }

        /// <summary>
        /// Compares the Nodes based on their total costs.
        /// Total Costs: A* Pathfinding
        /// Current: Djikstra Pathfinding
        /// Estimated: Greedy Pathfinding
        /// </summary>
        /// <param name="other">The other node.</param>
        /// <returns>A comparison between the costs</returns>
        public int CompareTo(Node other) => TotalCost.CompareTo(other.TotalCost);

        /// <summary>
        /// returns the distance to the other node.
        /// </summary>
        /// <param name="other">The other node</param>
        /// <returns>Distance between this and other</returns>
        public float DistanceTo(Node other)
        {
            // Since we are only using the distance in comparison with other distances, we can skip using Math.Sqrt
            return Position.SqrDistance(other.Position);
        }


    }
}
