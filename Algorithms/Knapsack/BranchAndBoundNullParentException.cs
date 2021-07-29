using System;
using System.Runtime.Serialization;

namespace Algorithms.Knapsack
{
    public class BranchAndBoundNullParentException : Exception, ISerializable
    {
        public BranchAndBoundNullParentException(string msg)
        : base(msg)
        {
        }
    }
}
