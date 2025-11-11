using Algorithms.Graph;
using DataStructures.Graph;

namespace Algorithms.Tests.Graph;

public class TopologicalSortTests
{
    /// <summary>
    ///     Test topological sort on a simple linear DAG: A → B → C.
    ///     Expected order: [A, B, C].
    /// </summary>
    [Test]
    public void Sort_SimpleLinearGraph_ReturnsCorrectOrder()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<string>(3);
        var vertexA = graph.AddVertex("A");
        var vertexB = graph.AddVertex("B");
        var vertexC = graph.AddVertex("C");

        graph.AddEdge(vertexA, vertexB, 1);
        graph.AddEdge(vertexB, vertexC, 1);

        var topologicalSort = new TopologicalSort<string>();

        // Act
        var result = topologicalSort.Sort(graph);

        // Assert
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo(vertexA));
        Assert.That(result[1], Is.EqualTo(vertexB));
        Assert.That(result[2], Is.EqualTo(vertexC));
    }

    /// <summary>
    ///     Test Kahn's algorithm on a simple linear DAG: A → B → C.
    ///     Expected order: [A, B, C].
    /// </summary>
    [Test]
    public void SortKahn_SimpleLinearGraph_ReturnsCorrectOrder()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<string>(3);
        var vertexA = graph.AddVertex("A");
        var vertexB = graph.AddVertex("B");
        var vertexC = graph.AddVertex("C");

        graph.AddEdge(vertexA, vertexB, 1);
        graph.AddEdge(vertexB, vertexC, 1);

        var topologicalSort = new TopologicalSort<string>();

        // Act
        var result = topologicalSort.SortKahn(graph);

        // Assert
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo(vertexA));
        Assert.That(result[1], Is.EqualTo(vertexB));
        Assert.That(result[2], Is.EqualTo(vertexC));
    }

    /// <summary>
    ///     Test topological sort on a DAG with multiple valid orderings.
    ///     Graph: A → C
    ///            B → C
    ///     Valid orderings: [A, B, C] or [B, A, C].
    ///     We verify that C comes after both A and B.
    /// </summary>
    [Test]
    public void Sort_GraphWithMultipleValidOrderings_ReturnsValidOrder()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<string>(3);
        var vertexA = graph.AddVertex("A");
        var vertexB = graph.AddVertex("B");
        var vertexC = graph.AddVertex("C");

        graph.AddEdge(vertexA, vertexC, 1);
        graph.AddEdge(vertexB, vertexC, 1);

        var topologicalSort = new TopologicalSort<string>();

        // Act
        var result = topologicalSort.Sort(graph);

        // Assert
        Assert.That(result.Count, Is.EqualTo(3));

        // C should come after both A and B
        var indexA = result.IndexOf(vertexA);
        var indexB = result.IndexOf(vertexB);
        var indexC = result.IndexOf(vertexC);

        Assert.That(indexC, Is.GreaterThan(indexA));
        Assert.That(indexC, Is.GreaterThan(indexB));
    }

    /// <summary>
    ///     Test Kahn's algorithm on a DAG with multiple valid orderings.
    ///     Graph: A → C
    ///            B → C
    ///     Valid orderings: [A, B, C] or [B, A, C].
    ///     We verify that C comes after both A and B.
    /// </summary>
    [Test]
    public void SortKahn_GraphWithMultipleValidOrderings_ReturnsValidOrder()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<string>(3);
        var vertexA = graph.AddVertex("A");
        var vertexB = graph.AddVertex("B");
        var vertexC = graph.AddVertex("C");

        graph.AddEdge(vertexA, vertexC, 1);
        graph.AddEdge(vertexB, vertexC, 1);

        var topologicalSort = new TopologicalSort<string>();

        // Act
        var result = topologicalSort.SortKahn(graph);

        // Assert
        Assert.That(result.Count, Is.EqualTo(3));

        // C should come after both A and B
        var indexA = result.IndexOf(vertexA);
        var indexB = result.IndexOf(vertexB);
        var indexC = result.IndexOf(vertexC);

        Assert.That(indexC, Is.GreaterThan(indexA));
        Assert.That(indexC, Is.GreaterThan(indexB));
    }

    /// <summary>
    ///     Test topological sort on a more complex DAG.
    ///     Graph: A → B → D
    ///            A → C → D
    ///     Valid orderings include: [A, B, C, D], [A, C, B, D].
    ///     We verify that A comes first and D comes last.
    /// </summary>
    [Test]
    public void Sort_ComplexDAG_ReturnsValidOrder()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<string>(4);
        var vertexA = graph.AddVertex("A");
        var vertexB = graph.AddVertex("B");
        var vertexC = graph.AddVertex("C");
        var vertexD = graph.AddVertex("D");

        graph.AddEdge(vertexA, vertexB, 1);
        graph.AddEdge(vertexA, vertexC, 1);
        graph.AddEdge(vertexB, vertexD, 1);
        graph.AddEdge(vertexC, vertexD, 1);

        var topologicalSort = new TopologicalSort<string>();

        // Act
        var result = topologicalSort.Sort(graph);

        // Assert
        Assert.That(result.Count, Is.EqualTo(4));
        Assert.That(result[0], Is.EqualTo(vertexA)); // A must be first
        Assert.That(result[3], Is.EqualTo(vertexD)); // D must be last

        // B and C should come after A and before D
        var indexB = result.IndexOf(vertexB);
        var indexC = result.IndexOf(vertexC);

        Assert.That(indexB, Is.GreaterThan(0));
        Assert.That(indexB, Is.LessThan(3));
        Assert.That(indexC, Is.GreaterThan(0));
        Assert.That(indexC, Is.LessThan(3));
    }

    /// <summary>
    ///     Test Kahn's algorithm on a more complex DAG.
    ///     Graph: A → B → D
    ///            A → C → D
    ///     Valid orderings include: [A, B, C, D], [A, C, B, D].
    ///     We verify that A comes first and D comes last.
    /// </summary>
    [Test]
    public void SortKahn_ComplexDAG_ReturnsValidOrder()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<string>(4);
        var vertexA = graph.AddVertex("A");
        var vertexB = graph.AddVertex("B");
        var vertexC = graph.AddVertex("C");
        var vertexD = graph.AddVertex("D");

        graph.AddEdge(vertexA, vertexB, 1);
        graph.AddEdge(vertexA, vertexC, 1);
        graph.AddEdge(vertexB, vertexD, 1);
        graph.AddEdge(vertexC, vertexD, 1);

        var topologicalSort = new TopologicalSort<string>();

        // Act
        var result = topologicalSort.SortKahn(graph);

        // Assert
        Assert.That(result.Count, Is.EqualTo(4));
        Assert.That(result[0], Is.EqualTo(vertexA)); // A must be first
        Assert.That(result[3], Is.EqualTo(vertexD)); // D must be last

        // B and C should come after A and before D
        var indexB = result.IndexOf(vertexB);
        var indexC = result.IndexOf(vertexC);

        Assert.That(indexB, Is.GreaterThan(0));
        Assert.That(indexB, Is.LessThan(3));
        Assert.That(indexC, Is.GreaterThan(0));
        Assert.That(indexC, Is.LessThan(3));
    }

    /// <summary>
    ///     Test topological sort on a graph with a cycle.
    ///     Graph: A → B → C → A (cycle).
    ///     Should throw InvalidOperationException.
    /// </summary>
    [Test]
    public void Sort_GraphWithCycle_ThrowsInvalidOperationException()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<string>(3);
        var vertexA = graph.AddVertex("A");
        var vertexB = graph.AddVertex("B");
        var vertexC = graph.AddVertex("C");

        graph.AddEdge(vertexA, vertexB, 1);
        graph.AddEdge(vertexB, vertexC, 1);
        graph.AddEdge(vertexC, vertexA, 1); // Creates a cycle

        var topologicalSort = new TopologicalSort<string>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => topologicalSort.Sort(graph));
    }

    /// <summary>
    ///     Test Kahn's algorithm on a graph with a cycle.
    ///     Graph: A → B → C → A (cycle).
    ///     Should throw InvalidOperationException.
    /// </summary>
    [Test]
    public void SortKahn_GraphWithCycle_ThrowsInvalidOperationException()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<string>(3);
        var vertexA = graph.AddVertex("A");
        var vertexB = graph.AddVertex("B");
        var vertexC = graph.AddVertex("C");

        graph.AddEdge(vertexA, vertexB, 1);
        graph.AddEdge(vertexB, vertexC, 1);
        graph.AddEdge(vertexC, vertexA, 1); // Creates a cycle

        var topologicalSort = new TopologicalSort<string>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => topologicalSort.SortKahn(graph));
    }

    /// <summary>
    ///     Test topological sort on a single vertex graph.
    ///     Graph: A (no edges).
    ///     Expected order: [A].
    /// </summary>
    [Test]
    public void Sort_SingleVertexGraph_ReturnsSingleVertex()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<string>(1);
        var vertexA = graph.AddVertex("A");

        var topologicalSort = new TopologicalSort<string>();

        // Act
        var result = topologicalSort.Sort(graph);

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0], Is.EqualTo(vertexA));
    }

    /// <summary>
    ///     Test Kahn's algorithm on a single vertex graph.
    ///     Graph: A (no edges).
    ///     Expected order: [A].
    /// </summary>
    [Test]
    public void SortKahn_SingleVertexGraph_ReturnsSingleVertex()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<string>(1);
        var vertexA = graph.AddVertex("A");

        var topologicalSort = new TopologicalSort<string>();

        // Act
        var result = topologicalSort.SortKahn(graph);

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result[0], Is.EqualTo(vertexA));
    }

    /// <summary>
    ///     Test topological sort on a disconnected DAG.
    ///     Graph: A → B (component 1)
    ///            C → D (component 2)
    ///     Valid orderings: [A, B, C, D], [A, C, B, D], [C, A, B, D], [C, D, A, B], etc.
    ///     We verify that A comes before B and C comes before D.
    /// </summary>
    [Test]
    public void Sort_DisconnectedDAG_ReturnsValidOrder()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<string>(4);
        var vertexA = graph.AddVertex("A");
        var vertexB = graph.AddVertex("B");
        var vertexC = graph.AddVertex("C");
        var vertexD = graph.AddVertex("D");

        graph.AddEdge(vertexA, vertexB, 1);
        graph.AddEdge(vertexC, vertexD, 1);

        var topologicalSort = new TopologicalSort<string>();

        // Act
        var result = topologicalSort.Sort(graph);

        // Assert
        Assert.That(result.Count, Is.EqualTo(4));

        // A should come before B
        var indexA = result.IndexOf(vertexA);
        var indexB = result.IndexOf(vertexB);
        Assert.That(indexB, Is.GreaterThan(indexA));

        // C should come before D
        var indexC = result.IndexOf(vertexC);
        var indexD = result.IndexOf(vertexD);
        Assert.That(indexD, Is.GreaterThan(indexC));
    }

    /// <summary>
    ///     Test Kahn's algorithm on a disconnected DAG.
    ///     Graph: A → B (component 1)
    ///            C → D (component 2)
    ///     Valid orderings: [A, B, C, D], [A, C, B, D], [C, A, B, D], [C, D, A, B], etc.
    ///     We verify that A comes before B and C comes before D.
    /// </summary>
    [Test]
    public void SortKahn_DisconnectedDAG_ReturnsValidOrder()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<string>(4);
        var vertexA = graph.AddVertex("A");
        var vertexB = graph.AddVertex("B");
        var vertexC = graph.AddVertex("C");
        var vertexD = graph.AddVertex("D");

        graph.AddEdge(vertexA, vertexB, 1);
        graph.AddEdge(vertexC, vertexD, 1);

        var topologicalSort = new TopologicalSort<string>();

        // Act
        var result = topologicalSort.SortKahn(graph);

        // Assert
        Assert.That(result.Count, Is.EqualTo(4));

        // A should come before B
        var indexA = result.IndexOf(vertexA);
        var indexB = result.IndexOf(vertexB);
        Assert.That(indexB, Is.GreaterThan(indexA));

        // C should come before D
        var indexC = result.IndexOf(vertexC);
        var indexD = result.IndexOf(vertexD);
        Assert.That(indexD, Is.GreaterThan(indexC));
    }

    /// <summary>
    ///     Test topological sort on a real-world scenario: course prerequisites.
    ///     Graph represents course dependencies:
    ///     - Intro to CS (A) is a prerequisite for Data Structures (B) and Algorithms (C).
    ///     - Data Structures (B) is a prerequisite for Advanced Algorithms (D).
    ///     - Algorithms (C) is a prerequisite for Advanced Algorithms (D).
    ///     Expected: A must come first, D must come last.
    /// </summary>
    [Test]
    public void Sort_CoursePrerequisitesScenario_ReturnsValidOrder()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<string>(4);
        var introCS = graph.AddVertex("Intro to CS");
        var dataStructures = graph.AddVertex("Data Structures");
        var algorithms = graph.AddVertex("Algorithms");
        var advancedAlgorithms = graph.AddVertex("Advanced Algorithms");

        graph.AddEdge(introCS, dataStructures, 1);
        graph.AddEdge(introCS, algorithms, 1);
        graph.AddEdge(dataStructures, advancedAlgorithms, 1);
        graph.AddEdge(algorithms, advancedAlgorithms, 1);

        var topologicalSort = new TopologicalSort<string>();

        // Act
        var result = topologicalSort.Sort(graph);

        // Assert
        Assert.That(result.Count, Is.EqualTo(4));
        Assert.That(result[0], Is.EqualTo(introCS)); // Must take Intro to CS first
        Assert.That(result[3], Is.EqualTo(advancedAlgorithms)); // Advanced Algorithms must be last

        // Data Structures and Algorithms should be in the middle
        var indexDS = result.IndexOf(dataStructures);
        var indexAlgo = result.IndexOf(algorithms);

        Assert.That(indexDS, Is.GreaterThan(0));
        Assert.That(indexDS, Is.LessThan(3));
        Assert.That(indexAlgo, Is.GreaterThan(0));
        Assert.That(indexAlgo, Is.LessThan(3));
    }

    /// <summary>
    ///     Test Kahn's algorithm on a real-world scenario: course prerequisites.
    ///     Graph represents course dependencies:
    ///     - Intro to CS (A) is a prerequisite for Data Structures (B) and Algorithms (C).
    ///     - Data Structures (B) is a prerequisite for Advanced Algorithms (D).
    ///     - Algorithms (C) is a prerequisite for Advanced Algorithms (D).
    ///     Expected: A must come first, D must come last.
    /// </summary>
    [Test]
    public void SortKahn_CoursePrerequisitesScenario_ReturnsValidOrder()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<string>(4);
        var introCS = graph.AddVertex("Intro to CS");
        var dataStructures = graph.AddVertex("Data Structures");
        var algorithms = graph.AddVertex("Algorithms");
        var advancedAlgorithms = graph.AddVertex("Advanced Algorithms");

        graph.AddEdge(introCS, dataStructures, 1);
        graph.AddEdge(introCS, algorithms, 1);
        graph.AddEdge(dataStructures, advancedAlgorithms, 1);
        graph.AddEdge(algorithms, advancedAlgorithms, 1);

        var topologicalSort = new TopologicalSort<string>();

        // Act
        var result = topologicalSort.SortKahn(graph);

        // Assert
        Assert.That(result.Count, Is.EqualTo(4));
        Assert.That(result[0], Is.EqualTo(introCS)); // Must take Intro to CS first
        Assert.That(result[3], Is.EqualTo(advancedAlgorithms)); // Advanced Algorithms must be last

        // Data Structures and Algorithms should be in the middle
        var indexDS = result.IndexOf(dataStructures);
        var indexAlgo = result.IndexOf(algorithms);

        Assert.That(indexDS, Is.GreaterThan(0));
        Assert.That(indexDS, Is.LessThan(3));
        Assert.That(indexAlgo, Is.GreaterThan(0));
        Assert.That(indexAlgo, Is.LessThan(3));
    }

    /// <summary>
    ///     Test topological sort with integer vertices.
    ///     Graph: 1 → 2 → 3.
    ///     Expected order: [1, 2, 3].
    /// </summary>
    [Test]
    public void Sort_IntegerVertices_ReturnsCorrectOrder()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<int>(3);
        var vertex1 = graph.AddVertex(1);
        var vertex2 = graph.AddVertex(2);
        var vertex3 = graph.AddVertex(3);

        graph.AddEdge(vertex1, vertex2, 1);
        graph.AddEdge(vertex2, vertex3, 1);

        var topologicalSort = new TopologicalSort<int>();

        // Act
        var result = topologicalSort.Sort(graph);

        // Assert
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo(vertex1));
        Assert.That(result[1], Is.EqualTo(vertex2));
        Assert.That(result[2], Is.EqualTo(vertex3));
    }

    /// <summary>
    ///     Test Kahn's algorithm with integer vertices.
    ///     Graph: 1 → 2 → 3.
    ///     Expected order: [1, 2, 3].
    /// </summary>
    [Test]
    public void SortKahn_IntegerVertices_ReturnsCorrectOrder()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<int>(3);
        var vertex1 = graph.AddVertex(1);
        var vertex2 = graph.AddVertex(2);
        var vertex3 = graph.AddVertex(3);

        graph.AddEdge(vertex1, vertex2, 1);
        graph.AddEdge(vertex2, vertex3, 1);

        var topologicalSort = new TopologicalSort<int>();

        // Act
        var result = topologicalSort.SortKahn(graph);

        // Assert
        Assert.That(result.Count, Is.EqualTo(3));
        Assert.That(result[0], Is.EqualTo(vertex1));
        Assert.That(result[1], Is.EqualTo(vertex2));
        Assert.That(result[2], Is.EqualTo(vertex3));
    }

    /// <summary>
    ///     Test topological sort on a graph with self-loop (cycle).
    ///     Graph: A → A (self-loop).
    ///     Should throw InvalidOperationException.
    /// </summary>
    [Test]
    public void Sort_GraphWithSelfLoop_ThrowsInvalidOperationException()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<string>(1);
        var vertexA = graph.AddVertex("A");

        graph.AddEdge(vertexA, vertexA, 1); // Self-loop

        var topologicalSort = new TopologicalSort<string>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => topologicalSort.Sort(graph));
    }

    /// <summary>
    ///     Test Kahn's algorithm on a graph with self-loop (cycle).
    ///     Graph: A → A (self-loop).
    ///     Should throw InvalidOperationException.
    /// </summary>
    [Test]
    public void SortKahn_GraphWithSelfLoop_ThrowsInvalidOperationException()
    {
        // Arrange
        var graph = new DirectedWeightedGraph<string>(1);
        var vertexA = graph.AddVertex("A");

        graph.AddEdge(vertexA, vertexA, 1); // Self-loop

        var topologicalSort = new TopologicalSort<string>();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => topologicalSort.SortKahn(graph));
    }
}
