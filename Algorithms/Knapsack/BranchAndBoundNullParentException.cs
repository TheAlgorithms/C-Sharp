using System;
using System.Runtime.Serialization;

namespace Algorithms.Knapsack
{
    [Serializable]
    public class BranchAndBoundNullParentException : Exception
    {
        public BranchAndBoundNullParentException()
        {
        }

        public BranchAndBoundNullParentException(string msg)
        : base(msg)
        {
        }

        public BranchAndBoundNullParentException(string message, Exception innerException)
        : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected BranchAndBoundNullParentException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
        }
    }
}
