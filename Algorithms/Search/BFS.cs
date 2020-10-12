using System.Collections.Generic;

namespace BFS
{
    /// <summary>
    /// Bread First Search implementation.
    /// </summary>
    public class BreadthFirst<T>
    {
        // Returns true if item exists.

        public bool Find(IGraph<T> graph, T vertex)
        {
            return bfs(graph.ReferenceVertex, new HashSet<T>(), vertex);
        }

        // BFS implementation.

        private bool bfs(IGraphVertex<T> referenceVertex,
            HashSet<T> visited, T searchVertex)
        {
            var bfsQueue = new Queue<IGraphVertex<T>>();

            bfsQueue.Enqueue(referenceVertex);
            visited.Add(referenceVertex.Key);

            while (bfsQueue.Count > 0)
            {
                var current = bfsQueue.Dequeue();

                if (current.Key.Equals(searchVertex))
                	return true;

                foreach (var edge in current.Edges)
                {
                    if (visited.Contains(edge.TargetVertexKey))
                   	continue;

                    visited.Add(edge.TargetVertexKey);
                    bfsQueue.Enqueue(edge.TargetVertex);
                }
            }

            return false;
        }
    }
}