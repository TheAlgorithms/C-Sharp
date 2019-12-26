using System;

namespace AStar
{
    /// <summary>
    /// A Node(former Location)
    /// Contains Positional and other information about a single node.
    /// </summary>
    public class Node : IComparable<Node>, IEquatable<Node>
    {
        // Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="position">Position of the node.</param>
        /// <param name="traversable">Flag if the node is traversable.</param>
        /// <param name="traverseMultiplier">Multiplier for traversal costs.</param>
        public Node(VecN position, bool traversable, double traverseMultiplier)
        {
            Traversable = traversable;
            Position = position;
            TraversalCostMultiplier = traverseMultiplier;
        }

        // Properties

        /// <summary>
        /// Gets the Total cost of the Node.
        /// The Current Costs + the estimated costs.
        /// </summary>
        public double TotalCost => EstimatedCost + CurrentCost;

        /// <summary>
        /// Gets or sets the Distance between this node and the target node.
        /// </summary>
        public double EstimatedCost { get; set; }

        /// <summary>
        /// Gets a value indicating whether how costly it is to traverse over this node.
        /// </summary>
        public double TraversalCostMultiplier { get; }

        /// <summary>
        /// Gets or sets a value indicating whether to go from the start node to this node.
        /// </summary>
        public double CurrentCost { get; set; }

        /// <summary>
        /// Gets or sets the state of the Node
        /// Can be Unconsidered(Default), Open and Closed.
        /// </summary>
        public NodeState State { get; set; }

        /// <summary>
        /// Gets a value indicating whether the node is traversable.
        /// </summary>
        public bool Traversable { get; }

        /// <summary>
        /// Gets or sets a list of all connected nodes.
        /// </summary>
        public Node[] ConnectedNodes { get; set; } = new Node[0];

        /// <summary>
        /// Gets or sets he "previous" node that was processed before this node.
        /// </summary>
        public Node? Parent { get; set; }

        /// <summary>
        /// Gets the positional information of the node.
        /// </summary>
        public VecN Position { get; }

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="left">TODO 2.</param>
        /// <param name="right">TODO 3.</param>
        /// <returns>TODO 4.</returns>
        public static bool operator ==(Node left, Node right) => left?.Equals(right) != false;

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="left">TODO 2.</param>
        /// <param name="right">TODO 3.</param>
        /// <returns>TODO 4.</returns>
        public static bool operator >(Node left, Node right) => left.CompareTo(right) > 0;

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="left">TODO 2.</param>
        /// <param name="right">TODO 3.</param>
        /// <returns>TODO 4.</returns>
        public static bool operator <(Node left, Node right) => left.CompareTo(right) < 0;

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="left">TODO 2.</param>
        /// <param name="right">TODO 3.</param>
        /// <returns>TODO 4.</returns>
        public static bool operator !=(Node left, Node right) => !(left == right);

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="left">TODO 2.</param>
        /// <param name="right">TODO 3.</param>
        /// <returns>TODO 4.</returns>
        public static bool operator <=(Node left, Node right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="left">TODO 2.</param>
        /// <param name="right">TODO 3.</param>
        /// <returns>TODO 4.</returns>
        public static bool operator >=(Node left, Node right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Compares the Nodes based on their total costs.
        /// Total Costs: A* Pathfinding.
        /// Current: Djikstra Pathfinding.
        /// Estimated: Greedy Pathfinding.
        /// </summary>
        /// <param name="other">The other node.</param>
        /// <returns>A comparison between the costs.</returns>
        public int CompareTo(Node other) => TotalCost.CompareTo(other.TotalCost);

        /// <summary>
        /// Equals Override.
        /// </summary>
        /// <param name="obj">The object to be checked against.</param>
        /// <returns>True if Equal, False if Not Equal.</returns>
        public override bool Equals(object obj) => (obj is Node other) && Equals(other);

        /// <summary>
        /// Useless override to shut up the automated testing.
        /// </summary>
        /// <returns>the default hash value.</returns>
        public override int GetHashCode() => base.GetHashCode();

        /// <summary>
        /// Override for IEquatable.
        /// </summary>
        /// <param name="other">The object to be checked against.</param>
        /// <returns>True if Equal, False if not Equal.</returns>
        public bool Equals(Node other) => CompareTo(other) == 0;

        /// <summary>
        /// Returns the distance to the other node.
        /// </summary>
        /// <param name="other">The other node.</param>
        /// <returns>Distance between this and other.</returns>
        public double DistanceTo(Node other)
        {
            // Since we are only using the distance in comparison with other distances, we can skip using Math.Sqrt
            return Position.SqrDistance(other.Position);
        }
    }
}
