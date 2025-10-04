namespace Algorithms.Knapsack;

public class BranchAndBoundNode(int level, bool taken, BranchAndBoundNode? parent = null)
{
    // isTaken --> true = the item where index = level is taken, vice versa
    public bool IsTaken { get; } = taken;

    // cumulativeWeight --> um of weight of item associated in each nodes starting from root to this node (only item that is taken)
    public int CumulativeWeight { get; set; }

    // cumulativeValue --> sum of value of item associated in each nodes starting from root to this node (only item that is taken)
    public double CumulativeValue { get; set; }

    // upperBound --> largest possible value after taking/not taking the item associated to this node (fractional)
    public double UpperBound { get; set; }

    // level --> level of the node in the tree structure
    public int Level { get; } = level;

    // parent node
    public BranchAndBoundNode? Parent { get; } = parent;
}
