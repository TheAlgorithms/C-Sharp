using System;

namespace Algorithms.Search.AStar
{
    /// <summary>
    /// A pathfinding exception is thrown when the Pathfinder encounters a critical error and can not continue.
    /// </summary>
    public class PathfindingException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PathfindingException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public PathfindingException(string message)
            : base(message)
        {
        }
    }
}
