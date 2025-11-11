using DataStructures.Graph;

namespace Algorithms.Graph;

/// <summary>
///     Topological Sort is a linear ordering of vertices in a Directed Acyclic Graph (DAG)
///     such that for every directed edge (u, v), vertex u comes before vertex v in the ordering.
///
///     KEY CONCEPTS:
///     1. Only applicable to Directed Acyclic Graphs (DAGs) - graphs with no cycles.
///     2. A DAG can have multiple valid topological orderings.
///     3. Used in dependency resolution, task scheduling, build systems, and course prerequisites.
///
///     ALGORITHM APPROACHES:
///     1. DFS-based (Depth-First Search): Uses post-order traversal and reverses the result.
///     2. Kahn's Algorithm: Uses in-degree counting and processes vertices with zero in-degree.
///
///     TIME COMPLEXITY: O(V + E) where V is vertices and E is edges.
///     SPACE COMPLEXITY: O(V) for the visited set and result stack.
///
///     Reference: "Introduction to Algorithms" (CLRS) by Cormen, Leiserson, Rivest, and Stein.
///     Also covered in "Algorithm Design Manual" by Steven Skiena.
/// </summary>
/// <typeparam name="T">Vertex data type.</typeparam>
public class TopologicalSort<T> where T : IComparable<T>
{
    /// <summary>
    ///     Performs topological sort on a directed acyclic graph using DFS-based approach.
    ///
    ///     ALGORITHM STEPS (DFS-based approach):
    ///     1. Initialize a visited set to track processed vertices.
    ///     2. Initialize a stack to store the topological ordering.
    ///     3. For each unvisited vertex in the graph:
    ///        a) Perform DFS from that vertex.
    ///        b) After visiting all descendants, push the vertex to the stack.
    ///     4. The stack now contains vertices in reverse topological order.
    ///     5. Pop all vertices from the stack to get the topological ordering.
    ///
    ///     WHY IT WORKS:
    ///     - In DFS, we push a vertex to the stack only after visiting all its descendants.
    ///     - This ensures that all vertices that depend on the current vertex are processed first.
    ///     - Reversing this order gives us the topological sort.
    ///
    ///     EXAMPLE:
    ///     Graph: A → B → C
    ///            A → D
    ///            D → C
    ///     Valid topological orderings: [A, B, D, C] or [A, D, B, C].
    ///
    ///     USE CASES:
    ///     - Build systems (compile dependencies).
    ///     - Task scheduling with dependencies.
    ///     - Course prerequisite ordering.
    ///     - Package dependency resolution.
    /// </summary>
    /// <param name="graph">The directed acyclic graph to sort.</param>
    /// <returns>A list of vertices in topological order.</returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the graph contains a cycle (not a DAG).
    /// </exception>
    public List<Vertex<T>> Sort(IDirectedWeightedGraph<T> graph)
    {
        // Stack to store vertices in reverse topological order.
        // We use a stack because DFS naturally gives us reverse topological order.
        var stack = new Stack<Vertex<T>>();

        // Track visited vertices to avoid reprocessing and detect cycles.
        var visited = new HashSet<Vertex<T>>();

        // Track vertices currently in the recursion stack to detect cycles.
        // If we encounter a vertex that's in the recursion stack, we have a cycle.
        var recursionStack = new HashSet<Vertex<T>>();

        // Process all vertices in the graph.
        // We need to iterate through all vertices because the graph might be disconnected.
        for (int i = 0; i < graph.Count; i++)
        {
            var vertex = graph.Vertices[i];

            // Skip null vertices (shouldn't happen in a well-formed graph).
            if (vertex == null)
            {
                continue;
            }

            // If vertex hasn't been visited, perform DFS from it.
            if (!visited.Contains(vertex))
            {
                DfsTopologicalSort(graph, vertex, visited, recursionStack, stack);
            }
        }

        // Convert stack to list. The stack contains vertices in reverse topological order,
        // so we need to reverse it to get the correct topological ordering.
        var result = new List<Vertex<T>>(stack.Count);
        while (stack.Count > 0)
        {
            result.Add(stack.Pop());
        }

        return result;
    }

    /// <summary>
    ///     Performs topological sort using Kahn's Algorithm (BFS-based approach).
    ///
    ///     ALGORITHM STEPS (Kahn's Algorithm):
    ///     1. Calculate in-degree (number of incoming edges) for each vertex.
    ///     2. Add all vertices with in-degree 0 to a queue.
    ///     3. While the queue is not empty:
    ///        a) Remove a vertex from the queue and add it to the result.
    ///        b) For each neighbor of this vertex:
    ///           - Decrease its in-degree by 1.
    ///           - If in-degree becomes 0, add it to the queue.
    ///     4. If all vertices are processed, return the result.
    ///     5. If not all vertices are processed, the graph has a cycle.
    ///
    ///     WHY IT WORKS:
    ///     - Vertices with in-degree 0 have no dependencies and can be processed first.
    ///     - After processing a vertex, we "remove" its outgoing edges by decreasing
    ///       the in-degree of its neighbors.
    ///     - This gradually reveals more vertices with in-degree 0.
    ///
    ///     ADVANTAGES OVER DFS:
    ///     - More intuitive for understanding dependencies.
    ///     - Easier to detect cycles (if not all vertices are processed).
    ///     - Better for parallel processing scenarios.
    /// </summary>
    /// <param name="graph">The directed acyclic graph to sort.</param>
    /// <returns>A list of vertices in topological order.</returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the graph contains a cycle (not a DAG).
    /// </exception>
    public List<Vertex<T>> SortKahn(IDirectedWeightedGraph<T> graph)
    {
        // Calculate in-degree for each vertex.
        var inDegree = CalculateInDegrees(graph);

        // Queue to process vertices with in-degree 0.
        var queue = InitializeQueueWithZeroInDegreeVertices(inDegree);

        // Process vertices in topological order.
        var result = ProcessVerticesInTopologicalOrder(graph, inDegree, queue);

        // Verify all vertices were processed (no cycles).
        ValidateNoCycles(graph, result);

        return result;
    }

    /// <summary>
    ///     Calculates the in-degree for each vertex in the graph.
    ///     In-degree is the number of incoming edges to a vertex.
    /// </summary>
    /// <param name="graph">The graph to analyze.</param>
    /// <returns>Dictionary mapping each vertex to its in-degree.</returns>
    private Dictionary<Vertex<T>, int> CalculateInDegrees(IDirectedWeightedGraph<T> graph)
    {
        var inDegree = new Dictionary<Vertex<T>, int>();

        // Initialize in-degree for all vertices to 0.
        for (int i = 0; i < graph.Count; i++)
        {
            var vertex = graph.Vertices[i];
            if (vertex != null)
            {
                inDegree[vertex] = 0;
            }
        }

        // Calculate actual in-degrees by examining all edges.
        for (int i = 0; i < graph.Count; i++)
        {
            var vertex = graph.Vertices[i];
            if (vertex != null)
            {
                IncrementNeighborInDegrees(graph, vertex, inDegree);
            }
        }

        return inDegree;
    }

    /// <summary>
    ///     Increments the in-degree for all neighbors of a given vertex.
    /// </summary>
    /// <param name="graph">The graph containing the vertices.</param>
    /// <param name="vertex">The vertex whose neighbors' in-degrees should be incremented.</param>
    /// <param name="inDegree">Dictionary tracking in-degrees.</param>
    private void IncrementNeighborInDegrees(
        IDirectedWeightedGraph<T> graph,
        Vertex<T> vertex,
        Dictionary<Vertex<T>, int> inDegree)
    {
        foreach (var neighbor in graph.GetNeighbors(vertex))
        {
            if (neighbor != null)
            {
                inDegree[neighbor]++;
            }
        }
    }

    /// <summary>
    ///     Initializes a queue with all vertices that have in-degree 0.
    ///     These vertices have no dependencies and can be processed first.
    /// </summary>
    /// <param name="inDegree">Dictionary mapping vertices to their in-degrees.</param>
    /// <returns>Queue containing all vertices with in-degree 0.</returns>
    private Queue<Vertex<T>> InitializeQueueWithZeroInDegreeVertices(Dictionary<Vertex<T>, int> inDegree)
    {
        var queue = new Queue<Vertex<T>>();

        foreach (var kvp in inDegree)
        {
            if (kvp.Value == 0)
            {
                queue.Enqueue(kvp.Key);
            }
        }

        return queue;
    }

    /// <summary>
    ///     Processes vertices in topological order using Kahn's algorithm.
    ///     Dequeues vertices with in-degree 0 and decreases in-degrees of their neighbors.
    /// </summary>
    /// <param name="graph">The graph being sorted.</param>
    /// <param name="inDegree">Dictionary tracking in-degrees.</param>
    /// <param name="queue">Queue of vertices with in-degree 0.</param>
    /// <returns>List of vertices in topological order.</returns>
    private List<Vertex<T>> ProcessVerticesInTopologicalOrder(
        IDirectedWeightedGraph<T> graph,
        Dictionary<Vertex<T>, int> inDegree,
        Queue<Vertex<T>> queue)
    {
        var result = new List<Vertex<T>>();

        while (queue.Count > 0)
        {
            var vertex = queue.Dequeue();
            result.Add(vertex);

            ProcessNeighbors(graph, vertex, inDegree, queue);
        }

        return result;
    }

    /// <summary>
    ///     Processes neighbors of a vertex by decreasing their in-degrees.
    ///     If a neighbor's in-degree becomes 0, it's added to the queue.
    /// </summary>
    /// <param name="graph">The graph being sorted.</param>
    /// <param name="vertex">The vertex whose neighbors are being processed.</param>
    /// <param name="inDegree">Dictionary tracking in-degrees.</param>
    /// <param name="queue">Queue of vertices with in-degree 0.</param>
    private void ProcessNeighbors(
        IDirectedWeightedGraph<T> graph,
        Vertex<T> vertex,
        Dictionary<Vertex<T>, int> inDegree,
        Queue<Vertex<T>> queue)
    {
        foreach (var neighbor in graph.GetNeighbors(vertex))
        {
            if (neighbor != null)
            {
                inDegree[neighbor]--;

                if (inDegree[neighbor] == 0)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }
    }

    /// <summary>
    ///     Validates that all vertices were processed, ensuring no cycles exist.
    /// </summary>
    /// <param name="graph">The graph being sorted.</param>
    /// <param name="result">The list of processed vertices.</param>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when not all vertices were processed (cycle detected).
    /// </exception>
    private void ValidateNoCycles(IDirectedWeightedGraph<T> graph, List<Vertex<T>> result)
    {
        if (result.Count != graph.Count)
        {
            throw new InvalidOperationException(
                "Graph contains a cycle. Topological sort is only possible for Directed Acyclic Graphs (DAGs).");
        }
    }

    /// <summary>
    ///     Helper method for DFS-based topological sort.
    ///     Recursively visits vertices and adds them to the stack in post-order.
    ///
    ///     POST-ORDER TRAVERSAL:
    ///     - Visit all descendants first.
    ///     - Then process the current vertex.
    ///     - This ensures dependencies are processed before dependents.
    ///
    ///     CYCLE DETECTION:
    ///     - We maintain a recursion stack to track the current DFS path.
    ///     - If we encounter a vertex that's already in the recursion stack,
    ///       we've found a back edge, indicating a cycle.
    /// </summary>
    /// <param name="graph">The graph being sorted.</param>
    /// <param name="vertex">The current vertex being processed.</param>
    /// <param name="visited">Set of all visited vertices.</param>
    /// <param name="recursionStack">Set of vertices in the current DFS path.</param>
    /// <param name="stack">Stack to store vertices in reverse topological order.</param>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when a cycle is detected.
    /// </exception>
    private void DfsTopologicalSort(
        IDirectedWeightedGraph<T> graph,
        Vertex<T> vertex,
        HashSet<Vertex<T>> visited,
        HashSet<Vertex<T>> recursionStack,
        Stack<Vertex<T>> stack)
    {
        // CYCLE DETECTION:
        // If the vertex is in the recursion stack, we've encountered it again
        // in the current DFS path, which means there's a cycle.
        if (recursionStack.Contains(vertex))
        {
            throw new InvalidOperationException(
                $"Graph contains a cycle involving vertex: {vertex}. " +
                "Topological sort is only possible for Directed Acyclic Graphs (DAGs).");
        }

        // If already visited, no need to process again.
        if (visited.Contains(vertex))
        {
            return;
        }

        // Mark vertex as visited and add to recursion stack.
        visited.Add(vertex);
        recursionStack.Add(vertex);

        // Recursively visit all neighbors (descendants).
        // This ensures all dependencies are processed first.
        foreach (var neighbor in graph.GetNeighbors(vertex))
        {
            if (neighbor != null)
            {
                DfsTopologicalSort(graph, neighbor, visited, recursionStack, stack);
            }
        }

        // Remove from recursion stack as we're done with this DFS path.
        recursionStack.Remove(vertex);

        // POST-ORDER: Add vertex to stack after visiting all descendants.
        // This ensures that all vertices that depend on the current vertex
        // are already in the stack (deeper in the stack).
        stack.Push(vertex);
    }
}
