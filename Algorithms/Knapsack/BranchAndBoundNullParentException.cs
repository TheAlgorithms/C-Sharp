using System;

namespace Algorithms.Knapsack
{
    [Serializable]
    public class BranchAndBoundNullParentException : Exception
    {
        public BranchAndBoundNullParentException(string msg)
        : base(msg)
        {
        }
    }
}
