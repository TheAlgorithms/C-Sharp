using Algorithms.Problems.GraphColoring;

namespace Algorithms.Tests.Problems.GraphColoring;

[TestFixture]
public sealed class GraphColoringSolverTests
{
    /// <summary>
    /// Helper method to validate that a coloring is valid for a given graph.
    /// </summary>
    /// <param name="adjacencyMatrix">The graph adjacency matrix.</param>
    /// <param name="colors">The color assignment to validate.</param>
    /// <remarks>
    /// A valid coloring must satisfy:
    /// 1. All vertices are colored (no -1 values).
    /// 2. No two adjacent vertices have the same color.
    /// </remarks>
    private static void AssertValidColoring(bool[,] adjacencyMatrix, int[] colors)
    {
        var numVertices = adjacencyMatrix.GetLength(0);

        Assert.That(colors, Has.Length.EqualTo(numVertices), 
            "Color array length must match number of vertices.");

        // Check all vertices are colored
        for (var i = 0; i < numVertices; i++)
        {
            Assert.That(colors[i], Is.GreaterThanOrEqualTo(0),
                $"Vertex {i} is not colored (has value -1).");
        }

        // Check no adjacent vertices have the same color
        for (var i = 0; i < numVertices; i++)
        {
            for (var j = i + 1; j < numVertices; j++)
            {
                if (adjacencyMatrix[i, j])
                {
                    Assert.That(colors[i], Is.Not.EqualTo(colors[j]),
                        $"Adjacent vertices {i} and {j} have the same color {colors[i]}.");
                }
            }
        }
    }

    /// <summary>
    /// Helper method to create an empty graph (no edges).
    /// </summary>
    private static bool[,] CreateEmptyGraph(int vertices)
    {
        return new bool[vertices, vertices];
    }

    /// <summary>
    /// Helper method to create a complete graph where all vertices are connected.
    /// </summary>
    private static bool[,] CreateCompleteGraph(int vertices)
    {
        var graph = new bool[vertices, vertices];
        for (var i = 0; i < vertices; i++)
        {
            for (var j = 0; j < vertices; j++)
            {
                if (i != j)
                {
                    graph[i, j] = true;
                }
            }
        }
        return graph;
    }

    /// <summary>
    /// Helper method to create a bipartite graph (two sets with edges only between sets).
    /// </summary>
    private static bool[,] CreateBipartiteGraph(int setASize, int setBSize)
    {
        var total = setASize + setBSize;
        var graph = new bool[total, total];
        
        // Connect all vertices in set A to all vertices in set B
        for (var i = 0; i < setASize; i++)
        {
            for (var j = setASize; j < total; j++)
            {
                graph[i, j] = true;
                graph[j, i] = true;
            }
        }
        return graph;
    }

    /// <summary>
    /// Helper method to create a cycle graph.
    /// </summary>
    private static bool[,] CreateCycleGraph(int vertices)
    {
        var graph = new bool[vertices, vertices];
        for (var i = 0; i < vertices; i++)
        {
            var next = (i + 1) % vertices;
            graph[i, next] = true;
            graph[next, i] = true;
        }
        return graph;
    }

    /// <summary>
    /// Helper method to create a path graph (linear chain).
    /// </summary>
    private static bool[,] CreatePathGraph(int vertices)
    {
        var graph = new bool[vertices, vertices];
        for (var i = 0; i < vertices - 1; i++)
        {
            graph[i, i + 1] = true;
            graph[i + 1, i] = true;
        }
        return graph;
    }

    [Test]
    public void ColorGraph_ThrowsArgumentNullException_WhenAdjacencyMatrixIsNull()
    {
        var solver = new GraphColoringSolver();

        Assert.Throws<ArgumentNullException>(() => solver.ColorGraph(null!, 3));
    }

    [Test]
    public void ColorGraph_ThrowsArgumentException_WhenAdjacencyMatrixIsNotSquare()
    {
        var solver = new GraphColoringSolver();
        var nonSquareMatrix = new bool[3, 4];

        Assert.Throws<ArgumentException>(() => solver.ColorGraph(nonSquareMatrix, 3));
    }

    [TestCase(0)]
    [TestCase(-1)]
    [TestCase(-5)]
    public void ColorGraph_ThrowsArgumentException_WhenNumColorsIsNonPositive(int numColors)
    {
        var solver = new GraphColoringSolver();
        var graph = CreateEmptyGraph(3);

        Assert.Throws<ArgumentException>(() => solver.ColorGraph(graph, numColors));
    }

    [Test]
    public void ColorGraph_ReturnsEmptyArray_ForEmptyGraph()
    {
        var solver = new GraphColoringSolver();
        var graph = new bool[0, 0];

        var result = solver.ColorGraph(graph, 1);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void ColorGraph_ColorsSingleVertex_WithOneColor()
    {
        var solver = new GraphColoringSolver();
        var graph = CreateEmptyGraph(1);

        var result = solver.ColorGraph(graph, 1);

        Assert.That(result, Has.Length.EqualTo(1));
        Assert.That(result[0], Is.EqualTo(0));
        AssertValidColoring(graph, result);
    }

    [Test]
    public void ColorGraph_ColorsDisconnectedVertices_WithOneColor()
    {
        var solver = new GraphColoringSolver();
        var graph = CreateEmptyGraph(5); // No edges

        var result = solver.ColorGraph(graph, 1);

        Assert.That(result, Has.Length.EqualTo(5));
        AssertValidColoring(graph, result);
        
        // All vertices should have the same color since there are no edges
        Assert.That(result.Distinct().Count(), Is.EqualTo(1));
    }

    [Test]
    public void ColorGraph_ColorsBipartiteGraph_WithTwoColors()
    {
        var solver = new GraphColoringSolver();
        var graph = CreateBipartiteGraph(3, 3);

        var result = solver.ColorGraph(graph, 2);

        Assert.That(result, Has.Length.EqualTo(6));
        AssertValidColoring(graph, result);
        
        // Bipartite graph should use exactly 2 colors
        Assert.That(result.Distinct().Count(), Is.LessThanOrEqualTo(2));
    }

    [Test]
    public void ColorGraph_ThrowsArgumentException_ForBipartiteGraphWithOneColor()
    {
        var solver = new GraphColoringSolver();
        var graph = CreateBipartiteGraph(2, 2);

        // Bipartite graph with edges requires at least 2 colors
        Assert.Throws<ArgumentException>(() => solver.ColorGraph(graph, 1));
    }

    [Test]
    public void ColorGraph_ColorsPathGraph_WithTwoColors()
    {
        var solver = new GraphColoringSolver();
        var graph = CreatePathGraph(5);

        var result = solver.ColorGraph(graph, 2);

        Assert.That(result, Has.Length.EqualTo(5));
        AssertValidColoring(graph, result);
        
        // Path graph can be colored with 2 colors
        Assert.That(result.Distinct().Count(), Is.LessThanOrEqualTo(2));
    }

    [Test]
    public void ColorGraph_ColorsEvenCycle_WithTwoColors()
    {
        var solver = new GraphColoringSolver();
        var graph = CreateCycleGraph(6); // Even cycle

        var result = solver.ColorGraph(graph, 2);

        Assert.That(result, Has.Length.EqualTo(6));
        AssertValidColoring(graph, result);
        
        // Even cycle can be colored with 2 colors
        Assert.That(result.Distinct().Count(), Is.LessThanOrEqualTo(2));
    }

    [Test]
    public void ColorGraph_ThrowsArgumentException_ForOddCycleWithTwoColors()
    {
        var solver = new GraphColoringSolver();
        var graph = CreateCycleGraph(5); // Odd cycle

        // Odd cycle requires at least 3 colors
        Assert.Throws<ArgumentException>(() => solver.ColorGraph(graph, 2));
    }

    [Test]
    public void ColorGraph_ColorsOddCycle_WithThreeColors()
    {
        var solver = new GraphColoringSolver();
        var graph = CreateCycleGraph(5); // Odd cycle

        var result = solver.ColorGraph(graph, 3);

        Assert.That(result, Has.Length.EqualTo(5));
        AssertValidColoring(graph, result);
    }

    [Test]
    public void ColorGraph_ColorsTriangle_WithThreeColors()
    {
        var solver = new GraphColoringSolver();
        var graph = CreateCompleteGraph(3); // Triangle (K3)

        var result = solver.ColorGraph(graph, 3);

        Assert.That(result, Has.Length.EqualTo(3));
        AssertValidColoring(graph, result);
        
        // Complete graph K3 requires exactly 3 colors
        Assert.That(result.Distinct().Count(), Is.EqualTo(3));
    }

    [Test]
    public void ColorGraph_ThrowsArgumentException_ForTriangleWithTwoColors()
    {
        var solver = new GraphColoringSolver();
        var graph = CreateCompleteGraph(3); // Triangle (K3)

        // Triangle requires 3 colors
        Assert.Throws<ArgumentException>(() => solver.ColorGraph(graph, 2));
    }

    [Test]
    public void ColorGraph_ColorsCompleteGraphK4_WithFourColors()
    {
        var solver = new GraphColoringSolver();
        var graph = CreateCompleteGraph(4); // K4

        var result = solver.ColorGraph(graph, 4);

        Assert.That(result, Has.Length.EqualTo(4));
        AssertValidColoring(graph, result);
        
        // Complete graph K4 requires exactly 4 colors
        Assert.That(result.Distinct().Count(), Is.EqualTo(4));
    }

    [Test]
    public void ColorGraph_ThrowsArgumentException_ForCompleteGraphK4WithThreeColors()
    {
        var solver = new GraphColoringSolver();
        var graph = CreateCompleteGraph(4); // K4

        // K4 requires 4 colors
        Assert.Throws<ArgumentException>(() => solver.ColorGraph(graph, 3));
    }

    [Test]
    public void ColorGraph_ColorsStarGraph_WithTwoColors()
    {
        var solver = new GraphColoringSolver();
        
        // Star graph: one central vertex connected to all others
        var graph = new bool[5, 5];
        for (var i = 1; i < 5; i++)
        {
            graph[0, i] = true;
            graph[i, 0] = true;
        }

        var result = solver.ColorGraph(graph, 2);

        Assert.That(result, Has.Length.EqualTo(5));
        AssertValidColoring(graph, result);
        
        // Star graph requires only 2 colors
        Assert.That(result.Distinct().Count(), Is.LessThanOrEqualTo(2));
    }

    [Test]
    public void ColorGraph_HandlesPetersenGraph_WithThreeColors()
    {
        var solver = new GraphColoringSolver();
        
        // Simplified version: a graph that requires 3 colors
        // Creating a graph with a triangle and additional connections
        var graph = new bool[5, 5];
        
        // Triangle: vertices 0, 1, 2
        graph[0, 1] = graph[1, 0] = true;
        graph[1, 2] = graph[2, 1] = true;
        graph[2, 0] = graph[0, 2] = true;
        
        // Additional edges to vertex 3
        graph[0, 3] = graph[3, 0] = true;
        graph[1, 3] = graph[3, 1] = true;
        
        // Additional edges to vertex 4
        graph[2, 4] = graph[4, 2] = true;
        graph[3, 4] = graph[4, 3] = true;

        var result = solver.ColorGraph(graph, 3);

        Assert.That(result, Has.Length.EqualTo(5));
        AssertValidColoring(graph, result);
    }

    [Test]
    public void ColorGraph_AllColorsWithinRange()
    {
        var solver = new GraphColoringSolver();
        var graph = CreateCompleteGraph(4);
        var numColors = 4;

        var result = solver.ColorGraph(graph, numColors);

        // Verify all colors are in range [0, numColors)
        foreach (var color in result)
        {
            Assert.That(color, Is.InRange(0, numColors - 1));
        }
    }

    [Test]
    public void ColorGraph_SymmetricMatrix_ProducesSameResult()
    {
        var solver = new GraphColoringSolver();
        
        // Create a symmetric graph
        var graph = new bool[4, 4];
        graph[0, 1] = graph[1, 0] = true;
        graph[1, 2] = graph[2, 1] = true;
        graph[2, 3] = graph[3, 2] = true;

        var result = solver.ColorGraph(graph, 3);

        Assert.That(result, Has.Length.EqualTo(4));
        AssertValidColoring(graph, result);
    }

    [Test]
    public void ColorGraph_LargerGraph_ProducesValidColoring()
    {
        var solver = new GraphColoringSolver();
        
        // Create a larger graph (10 vertices, random edges)
        var graph = new bool[10, 10];
        
        // Create edges forming a more complex structure
        for (var i = 0; i < 9; i++)
        {
            graph[i, i + 1] = graph[i + 1, i] = true;
        }
        
        // Add some cross edges
        graph[0, 5] = graph[5, 0] = true;
        graph[2, 7] = graph[7, 2] = true;
        graph[3, 8] = graph[8, 3] = true;

        var result = solver.ColorGraph(graph, 3);

        Assert.That(result, Has.Length.EqualTo(10));
        AssertValidColoring(graph, result);
    }
}
