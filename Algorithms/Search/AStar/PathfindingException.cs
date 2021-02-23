using System;

namespace Algorithms.Search.AStar
{
    /// <summary>
    /// A pathfinding exception is thrown when the Pathfinder encounters a critical error and can not continue.
    /// </summary>
    public class PathfindingException : ApplicationException
    {
        public PathfindingException(string message)
            : base(message)
        {
        }
    }
}
