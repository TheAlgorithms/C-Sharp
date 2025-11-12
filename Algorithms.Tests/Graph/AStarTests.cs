using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Graph;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Graph;

/// <summary>
/// Tests for A* pathfinding algorithm.
/// </summary>
public class AStarTests
{
    #region Generic Graph Tests

    [Test]
    public void FindPath_SimpleGraph_ReturnsShortestPath()
    {
        // Arrange: Simple graph A -> B -> C
        IEnumerable<(string, double)> GetNeighbors(string node) => node switch
        {
            "A" => new[] { ("B", 1.0) },
            "B" => new[] { ("C", 1.0) },
            _ => Array.Empty<(string, double)>(),
        };

        double Heuristic(string a, string b) => 0; // Dijkstra mode

        // Act
        var path = AStar.FindPath("A", "C", GetNeighbors, Heuristic);

        // Assert
        path.Should().NotBeNull();
        path.Should().Equal("A", "B", "C");
    }

    [Test]
    public void FindPath_MultiplePathsGraph_ReturnsShortestPath()
    {
        // Arrange: Graph with multiple paths
        //   A --1--> B --1--> D
        //   |                 ^
        //   +------5----------+
        IEnumerable<(string, double)> GetNeighbors(string node) => node switch
        {
            "A" => new[] { ("B", 1.0), ("D", 5.0) },
            "B" => new[] { ("D", 1.0) },
            _ => Array.Empty<(string, double)>(),
        };

        double Heuristic(string a, string b) => 0;

        // Act
        var path = AStar.FindPath("A", "D", GetNeighbors, Heuristic);

        // Assert
        path.Should().NotBeNull();
        path.Should().Equal("A", "B", "D");
    }

    [Test]
    public void FindPath_NoPath_ReturnsNull()
    {
        // Arrange: Disconnected graph
        IEnumerable<(string, double)> GetNeighbors(string node) => node switch
        {
            "A" => new[] { ("B", 1.0) },
            "C" => new[] { ("D", 1.0) },
            _ => Array.Empty<(string, double)>(),
        };

        double Heuristic(string a, string b) => 0;

        // Act
        var path = AStar.FindPath("A", "D", GetNeighbors, Heuristic);

        // Assert
        path.Should().BeNull();
    }

    [Test]
    public void FindPath_StartEqualsGoal_ReturnsSingleNode()
    {
        // Arrange
        IEnumerable<(string, double)> GetNeighbors(string node) => Array.Empty<(string, double)>();
        double Heuristic(string a, string b) => 0;

        // Act
        var path = AStar.FindPath("A", "A", GetNeighbors, Heuristic);

        // Assert
        path.Should().NotBeNull();
        path.Should().Equal("A");
    }

    [Test]
    public void FindPath_WithHeuristic_FindsOptimalPath()
    {
        // Arrange: Graph where heuristic guides search
        IEnumerable<(string, double)> GetNeighbors(string node) => node switch
        {
            "A" => new[] { ("B", 1.0), ("C", 4.0) },
            "B" => new[] { ("D", 2.0) },
            "C" => new[] { ("D", 1.0) },
            _ => Array.Empty<(string, double)>(),
        };

        var positions = new Dictionary<string, (int x, int y)>
        {
            ["A"] = (0, 0),
            ["B"] = (1, 0),
            ["C"] = (0, 1),
            ["D"] = (2, 0),
        };

        double Heuristic(string a, string b)
        {
            var (x1, y1) = positions[a];
            var (x2, y2) = positions[b];
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        // Act
        var path = AStar.FindPath("A", "D", GetNeighbors, Heuristic);

        // Assert
        path.Should().NotBeNull();
        path.Should().Equal("A", "B", "D");
    }



    #endregion

    #region Grid Tests

    [Test]
    public void FindPathInGrid_SimpleGrid_ReturnsShortestPath()
    {
        // Arrange: 3x3 grid, all walkable
        var grid = new bool[,]
        {
            { true, true, true },
            { true, true, true },
            { true, true, true },
        };

        // Act
        var path = AStar.FindPathInGrid(grid, (0, 0), (2, 2), allowDiagonal: false);

        // Assert
        path.Should().NotBeNull();
        path!.Count.Should().Be(5); // Manhattan distance: right, right, down, down
        path.First().Should().Be((0, 0));
        path.Last().Should().Be((2, 2));
    }

    [Test]
    public void FindPathInGrid_WithObstacle_FindsAlternatePath()
    {
        // Arrange: Grid with obstacle in the middle
        var grid = new bool[,]
        {
            { true, true, true },
            { true, false, true },
            { true, true, true },
        };

        // Act
        var path = AStar.FindPathInGrid(grid, (0, 0), (2, 2), allowDiagonal: false);

        // Assert
        path.Should().NotBeNull();
        path.Should().NotContain((1, 1)); // Should avoid obstacle
        path!.First().Should().Be((0, 0));
        path!.Last().Should().Be((2, 2));
    }

    [Test]
    public void FindPathInGrid_NoPath_ReturnsNull()
    {
        // Arrange: Grid with wall blocking path
        var grid = new bool[,]
        {
            { true, false, true },
            { true, false, true },
            { true, false, true },
        };

        // Act
        var path = AStar.FindPathInGrid(grid, (0, 0), (0, 2), allowDiagonal: false);

        // Assert
        path.Should().BeNull();
    }

    [Test]
    public void FindPathInGrid_DiagonalAllowed_UsesDiagonalPath()
    {
        // Arrange: 3x3 grid, all walkable
        var grid = new bool[,]
        {
            { true, true, true },
            { true, true, true },
            { true, true, true },
        };

        // Act
        var path = AStar.FindPathInGrid(grid, (0, 0), (2, 2), allowDiagonal: true);

        // Assert
        path.Should().NotBeNull();
        path!.Count.Should().Be(3); // Diagonal path is shorter
        path.First().Should().Be((0, 0));
        path.Last().Should().Be((2, 2));
    }

    [Test]
    public void FindPathInGrid_LargeGrid_FindsPath()
    {
        // Arrange: 10x10 grid
        var grid = new bool[10, 10];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                grid[i, j] = true;
            }
        }

        // Act
        var path = AStar.FindPathInGrid(grid, (0, 0), (9, 9), allowDiagonal: false);

        // Assert
        path.Should().NotBeNull();
        path!.Count.Should().Be(19); // Manhattan distance
        path.First().Should().Be((0, 0));
        path.Last().Should().Be((9, 9));
    }

    [Test]
    public void FindPathInGrid_ComplexMaze_FindsPath()
    {
        // Arrange: Complex maze
        var grid = new bool[,]
        {
            { true, true, false, true, true },
            { false, true, false, true, false },
            { true, true, true, true, true },
            { true, false, false, false, true },
            { true, true, true, true, true },
        };

        // Act
        var path = AStar.FindPathInGrid(grid, (0, 0), (4, 4), allowDiagonal: false);

        // Assert
        path.Should().NotBeNull();
        path!.First().Should().Be((0, 0));
        path!.Last().Should().Be((4, 4));
    }

    [Test]
    public void FindPathInGrid_NullGrid_ThrowsArgumentNullException()
    {
        // Act
        Action act = () => AStar.FindPathInGrid(null!, (0, 0), (1, 1));

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("grid");
    }

    [Test]
    public void FindPathInGrid_StartOutOfBounds_ThrowsArgumentException()
    {
        // Arrange
        var grid = new bool[3, 3];

        // Act
        Action act = () => AStar.FindPathInGrid(grid, (-1, 0), (1, 1));

        // Assert
        act.Should().Throw<ArgumentException>().WithParameterName("start");
    }



    [Test]
    public void FindPathInGrid_StartNotWalkable_ThrowsArgumentException()
    {
        // Arrange
        var grid = new bool[,]
        {
            { false, true, true },
            { true, true, true },
            { true, true, true },
        };

        // Act
        Action act = () => AStar.FindPathInGrid(grid, (0, 0), (2, 2));

        // Assert
        act.Should().Throw<ArgumentException>().WithParameterName("start");
    }

    [Test]
    public void FindPathInGrid_GoalNotWalkable_ThrowsArgumentException()
    {
        // Arrange
        var grid = new bool[,]
        {
            { true, true, true },
            { true, true, true },
            { true, true, false },
        };

        // Act
        Action act = () => AStar.FindPathInGrid(grid, (0, 0), (2, 2));

        // Assert
        act.Should().Throw<ArgumentException>().WithParameterName("goal");
    }

    #endregion

    #region Path Cost Tests

    [Test]
    public void CalculatePathCost_SimplePath_ReturnsCorrectCost()
    {
        // Arrange
        var path = new List<string> { "A", "B", "C" };
        double GetCost(string a, string b) => 1.0;

        // Act
        var cost = AStar.CalculatePathCost(path, GetCost);

        // Assert
        cost.Should().Be(2.0);
    }

    [Test]
    public void CalculatePathCost_VariableCosts_ReturnsCorrectCost()
    {
        // Arrange
        var path = new List<string> { "A", "B", "C", "D" };
        var costs = new Dictionary<(string, string), double>
        {
            [("A", "B")] = 1.5,
            [("B", "C")] = 2.0,
            [("C", "D")] = 3.5,
        };

        double GetCost(string a, string b) => costs[(a, b)];

        // Act
        var cost = AStar.CalculatePathCost(path, GetCost);

        // Assert
        cost.Should().Be(7.0);
    }

    [Test]
    public void CalculatePathCost_SingleNode_ReturnsZero()
    {
        // Arrange
        var path = new List<string> { "A" };
        double GetCost(string a, string b) => 1.0;

        // Act
        var cost = AStar.CalculatePathCost(path, GetCost);

        // Assert
        cost.Should().Be(0);
    }

    [Test]
    public void CalculatePathCost_EmptyPath_ReturnsZero()
    {
        // Arrange
        var path = new List<string>();
        double GetCost(string a, string b) => 1.0;

        // Act
        var cost = AStar.CalculatePathCost(path, GetCost);

        // Assert
        cost.Should().Be(0);
    }

    #endregion

    #region Integer Node Tests

    [Test]
    public void FindPath_IntegerNodes_FindsPath()
    {
        // Arrange: Graph with integer nodes
        IEnumerable<(int, double)> GetNeighbors(int node) => node switch
        {
            1 => new[] { (2, 1.0), (3, 2.0) },
            2 => new[] { (4, 1.0) },
            3 => new[] { (4, 1.0) },
            _ => Array.Empty<(int, double)>(),
        };

        double Heuristic(int a, int b) => Math.Abs(a - b);

        // Act
        var path = AStar.FindPath(1, 4, GetNeighbors, Heuristic);

        // Assert
        path.Should().NotBeNull();
        path.Should().Equal(1, 2, 4);
    }

    #endregion
}
