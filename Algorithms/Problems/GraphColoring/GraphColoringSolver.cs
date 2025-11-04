namespace Algorithms.Problems.GraphColoring;

/// <summary>
/// Solves the Graph Coloring Problem using backtracking to assign colors to graph vertices
/// such that no two adjacent vertices share the same color.
/// </summary>
/// <remarks>
/// <para>
/// The Graph Coloring Problem is an NP-complete problem that aims to color the vertices
/// of a graph with the minimum number of colors such that no two adjacent vertices have
/// the same color. This implementation uses a backtracking algorithm to find a valid coloring
/// given a specific number of colors.
/// </para>
/// <para>
/// The algorithm attempts to assign colors to vertices one by one. If a color assignment
/// leads to a conflict, it backtracks and tries a different color. If no valid coloring
/// exists with the given number of colors, an exception is thrown.
/// </para>
/// <para>
/// <b>Complexity:</b> The worst-case time complexity is O(k^n) where n is the number of
/// vertices and k is the number of colors. This is an exponential algorithm suitable for
/// small to medium-sized graphs or graphs with special structures.
/// </para>
/// <para>
/// <b>Applications:</b> Graph coloring has numerous practical applications including:
/// register allocation in compilers, scheduling problems, frequency assignment in mobile networks,
/// and solving Sudoku puzzles.
/// </para>
/// <para>
/// For more information, see:
/// <see href="https://en.wikipedia.org/wiki/Graph_coloring">Graph Coloring on Wikipedia</see>.
/// </para>
/// </remarks>
public sealed class GraphColoringSolver
{
    /// <summary>
    /// Attempts to color a graph with the specified number of colors.
    /// </summary>
    /// <param name="adjacencyMatrix">
    /// A square boolean matrix representing the graph where <c>adjacencyMatrix[i, j]</c>
    /// is <c>true</c> if there is an edge between vertex <c>i</c> and vertex <c>j</c>.
    /// The matrix must be symmetric for undirected graphs.
    /// </param>
    /// <param name="numColors">The number of colors to use for coloring. Must be positive.</param>
    /// <returns>
    /// An array where each element represents the color assigned to the corresponding vertex
    /// (0-indexed colors from 0 to <paramref name="numColors"/> - 1).
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="adjacencyMatrix"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the adjacency matrix is not square, when <paramref name="numColors"/> is non-positive,
    /// or when no valid coloring exists with the given number of colors.
    /// </exception>
    /// <remarks>
    /// <para>
    /// This method finds the first valid coloring it encounters. Multiple valid colorings
    /// may exist for a given graph, but only one is returned.
    /// </para>
    /// <para>
    /// <b>Example:</b> For a triangle graph (3 vertices, all connected), at least 3 colors
    /// are required. Calling this method with <c>numColors = 2</c> will throw an exception,
    /// while <c>numColors = 3</c> will return a valid coloring such as <c>[0, 1, 2]</c>.
    /// </para>
    /// </remarks>
    public int[] ColorGraph(bool[,] adjacencyMatrix, int numColors)
    {
        if (adjacencyMatrix is null)
        {
            throw new ArgumentNullException(nameof(adjacencyMatrix));
        }

        var numVertices = adjacencyMatrix.GetLength(0);

        if (numVertices != adjacencyMatrix.GetLength(1))
        {
            throw new ArgumentException("Adjacency matrix must be square.", nameof(adjacencyMatrix));
        }

        if (numColors <= 0)
        {
            throw new ArgumentException("Number of colors must be positive.", nameof(numColors));
        }

        // Handle empty graph
        if (numVertices == 0)
        {
            return Array.Empty<int>();
        }

        var colors = new int[numVertices];

        // Initialize all vertices as uncolored (-1)
        Array.Fill(colors, -1);

        if (!ColorVertex(adjacencyMatrix, colors, 0, numColors))
        {
            throw new ArgumentException(
                $"Graph cannot be colored with {numColors} color(s). " +
                $"A larger number of colors may be required.");
        }

        return colors;
    }

    /// <summary>
    /// Recursively attempts to color vertices using backtracking.
    /// </summary>
    /// <param name="adjacencyMatrix">The graph adjacency matrix.</param>
    /// <param name="colors">Current color assignment for each vertex.</param>
    /// <param name="vertex">The current vertex to color.</param>
    /// <param name="numColors">The number of available colors.</param>
    /// <returns><c>true</c> if a valid coloring is found; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// This method tries each available color for the current vertex. If a color is valid
    /// (doesn't conflict with adjacent vertices), it proceeds to color the next vertex.
    /// If no valid color is found, it backtracks.
    /// </remarks>
    private bool ColorVertex(bool[,] adjacencyMatrix, int[] colors, int vertex, int numColors)
    {
        var numVertices = adjacencyMatrix.GetLength(0);

        // Base case: all vertices are colored
        if (vertex == numVertices)
        {
            return true;
        }

        // Try each color for the current vertex
        for (var color = 0; color < numColors; color++)
        {
            if (IsSafeToColor(adjacencyMatrix, colors, vertex, color))
            {
                colors[vertex] = color;

                // Recursively color the next vertex
                if (ColorVertex(adjacencyMatrix, colors, vertex + 1, numColors))
                {
                    return true;
                }

                // Backtrack if the current color assignment doesn't lead to a solution
                colors[vertex] = -1;
            }
        }

        return false;
    }

    /// <summary>
    /// Checks whether it is safe to assign a color to a vertex.
    /// </summary>
    /// <param name="adjacencyMatrix">The graph adjacency matrix.</param>
    /// <param name="colors">Current color assignment for each vertex.</param>
    /// <param name="vertex">The vertex to check.</param>
    /// <param name="color">The color to assign.</param>
    /// <returns>
    /// <c>true</c> if the color can be safely assigned (no adjacent vertex has this color);
    /// otherwise, <c>false</c>.
    /// </returns>
    /// <remarks>
    /// A color is safe to assign if none of the already-colored adjacent vertices
    /// have the same color.
    /// </remarks>
    private bool IsSafeToColor(bool[,] adjacencyMatrix, int[] colors, int vertex, int color)
    {
        var numVertices = adjacencyMatrix.GetLength(0);

        for (var i = 0; i < numVertices; i++)
        {
            // Check if vertex i is adjacent to the current vertex and has the same color
            if (adjacencyMatrix[vertex, i] && colors[i] == color)
            {
                return false;
            }
        }

        return true;
    }
}
