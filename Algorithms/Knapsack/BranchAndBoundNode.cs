using Utilities.Exceptions;

namespace Algorithms.Knapsack
{
    public class BranchAndBoundNode
    {
        // taken --> true = the item where index = level is taken, vice versa
        private readonly bool taken;

        // cumulativeWeight --> um of weight of item associated in each nodes starting from root to this node (only item that is taken)
        public int CumulativeWeight { get; set; }

        // cumulativeValue --> sum of value of item associated in each nodes starting from root to this node (only item that is taken)
        public double CumulativeValue { get; set; }

        // upperBound --> largest possible value after taking/not taking the item associated to this node (fractional)
        public double UpperBound { get; set; }

        // level --> level of the node in the tree structure
        public int Level { get; set; }

        // parent node
        public BranchAndBoundNode? Parent { get; set; }

        public BranchAndBoundNode()
        {
        }

        public BranchAndBoundNode(int level, bool taken, BranchAndBoundNode parent)
        {
            this.Level = level;
            this.taken = taken;
            this.Parent = parent;
        }

        public bool IsTaken()
        {
            return taken;
        }
    }
}
