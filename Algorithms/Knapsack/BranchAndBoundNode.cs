namespace Algorithms.Knapsack
{
    public class BranchAndBoundNode
    {
        public BranchAndBoundNode()
        {
        }

        public BranchAndBoundNode(int level, bool taken, BranchAndBoundNode parent)
        {
            this.level = level;
            this.taken = taken;
            this.parent = parent;
        }

        // cumulativeWeight --> um of weight of item associated in each nodes starting from root to this node (only item that is taken)
        private int cumulativeWeight;

        // cumulativeValue --> sum of value of item associated in each nodes starting from root to this node (only item that is taken)
        private double cumulativeValue;

        // upperBound --> largest possible value after taking/not taking the item associated to this node (fractional)
        private double upperBound;

        // level --> level of the node in the tree structure
        private int level;

        // taken --> true = the item where index = level is taken, vice versa
        private readonly bool taken;

        private BranchAndBoundNode? parent; // parent node

        public int GetCumulativeWeight()
        {
            return cumulativeWeight;
        }

        public void SetCumulativeWeight(int cumulativeWeight)
        {
            this.cumulativeWeight = cumulativeWeight;
        }

        public double GetCumulativeValue()
        {
            return cumulativeValue;
        }

        public void SetCumulativeValue(double cumulativeValue)
        {
            this.cumulativeValue = cumulativeValue;
        }

        public double GetUpperBound()
        {
            return upperBound;
        }

        public void SetUpperBound(double upperBound)
        {
            this.upperBound = upperBound;
        }

        public int GetLevel()
        {
            return level;
        }

        public void SetLevel(int level)
        {
            this.level = level;
        }

        public bool IsTaken()
        {
            return taken;
        }

        public BranchAndBoundNode GetParent()
        {
            if (parent != null)
            {
                return parent;
            }
            else
            {
                throw new System.NullReferenceException("Parent node is null.");
            }
        }

        public void SetParent(BranchAndBoundNode parent)
        {
            this.parent = parent;
        }
    }
}