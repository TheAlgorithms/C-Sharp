using System.Collections.Generic;

namespace DFS
{
    /// <summary>
    /// Depth First Search.
    /// </summary>

    public class DepthFirst<T>
    {
        // Returns true if item exists.

        public bool Find(IGraph<T> graph, T vertex)
        {
            return dfs(graph.ReferenceVertex, new HashSet<T>(), vertex);
        }

        // Recursive DFS.

        private bool dfs(IGraphVertex<T> current,
            HashSet<T> visited, T searchVetex)
        {
            visited.Add(current.Key);

            if (current.Key.Equals(searchVetex))
            	return true;

            foreach (var edge in current.Edges)
            {
                if (visited.Contains(edge.TargetVertexKey))
                	continue;

                if (dfs(edge.TargetVertex, visited, searchVetex))
                	return true;
            }

            return false;
        }
    }
}