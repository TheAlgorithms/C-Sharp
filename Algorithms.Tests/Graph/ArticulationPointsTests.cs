using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Graph;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Graph;

public class ArticulationPointsTests
{
    [Test]
    public void Find_SimpleChain_ReturnsMiddleVertex()
    {
        // Arrange: A - B - C (B is articulation point)
        var vertices = new[] { "A", "B", "C" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);

        // Assert
        result.Should().ContainSingle();
        result.Should().Contain("B");
    }

    [Test]
    public void Find_Triangle_ReturnsEmpty()
    {
        // Arrange: A - B - C - A (no articulation points)
        var vertices = new[] { "A", "B", "C" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "C" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "A", "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public void Find_StarGraph_ReturnsCenterVertex()
    {
        // Arrange: Star with center A
        var vertices = new[] { "A", "B", "C", "D" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "C", "D" },
            "B" => new[] { "A" },
            "C" => new[] { "A" },
            "D" => new[] { "A" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);

        // Assert
        result.Should().ContainSingle();
        result.Should().Contain("A");
    }

    [Test]
    public void Find_BridgeGraph_ReturnsMultiplePoints()
    {
        // Arrange: (A-B-C) - D - (E-F-G)
        var vertices = new[] { "A", "B", "C", "D", "E", "F", "G" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C", "D" },
            "C" => new[] { "B" },
            "D" => new[] { "B", "E" },
            "E" => new[] { "D", "F" },
            "F" => new[] { "E", "G" },
            "G" => new[] { "F" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);

        // Assert
        result.Should().HaveCount(4);
        result.Should().Contain(new[] { "B", "D", "E", "F" });
    }

    [Test]
    public void Find_DisconnectedGraph_FindsPointsInEachComponent()
    {
        // Arrange: (A-B-C) and (D-E-F)
        var vertices = new[] { "A", "B", "C", "D", "E", "F" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B" },
            "D" => new[] { "E" },
            "E" => new[] { "D", "F" },
            "F" => new[] { "E" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(new[] { "B", "E" });
    }

    [Test]
    public void Find_SingleVertex_ReturnsEmpty()
    {
        // Arrange
        var vertices = new[] { "A" };
        IEnumerable<string> GetNeighbors(string v) => Array.Empty<string>();

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public void Find_TwoVertices_ReturnsEmpty()
    {
        // Arrange: A - B
        var vertices = new[] { "A", "B" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public void Find_ComplexGraph_ReturnsCorrectPoints()
    {
        // Arrange: Complex graph with multiple articulation points
        var vertices = new[] { 1, 2, 3, 4, 5, 6, 7 };
        IEnumerable<int> GetNeighbors(int v) => v switch
        {
            1 => new[] { 2, 3 },
            2 => new[] { 1, 3 },
            3 => new[] { 1, 2, 4 },
            4 => new[] { 3, 5, 6 },
            5 => new[] { 4, 6 },
            6 => new[] { 4, 5, 7 },
            7 => new[] { 6 },
            _ => Array.Empty<int>(),
        };

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);

        // Assert
        result.Should().Contain(new[] { 3, 4, 6 });
    }

    [Test]
    public void Find_EmptyGraph_ReturnsEmpty()
    {
        // Arrange
        var vertices = Array.Empty<string>();
        IEnumerable<string> GetNeighbors(string v) => Array.Empty<string>();

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);

        // Assert
        result.Should().BeEmpty();
    }

    [Test]
    public void Find_NullVertices_ThrowsArgumentNullException()
    {
        // Act
        Action act = () => ArticulationPoints.Find<string>(null!, v => Array.Empty<string>());

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("vertices");
    }

    [Test]
    public void Find_NullGetNeighbors_ThrowsArgumentNullException()
    {
        // Arrange
        var vertices = new[] { "A" };

        // Act
        Action act = () => ArticulationPoints.Find(vertices, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("getNeighbors");
    }

    [Test]
    public void IsArticulationPoint_ValidPoint_ReturnsTrue()
    {
        // Arrange: A - B - C
        var vertices = new[] { "A", "B", "C" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.IsArticulationPoint("B", vertices, GetNeighbors);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void IsArticulationPoint_NotArticulationPoint_ReturnsFalse()
    {
        // Arrange: A - B - C
        var vertices = new[] { "A", "B", "C" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.IsArticulationPoint("A", vertices, GetNeighbors);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void Count_SimpleChain_ReturnsOne()
    {
        // Arrange: A - B - C
        var vertices = new[] { "A", "B", "C" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.Count(vertices, GetNeighbors);

        // Assert
        result.Should().Be(1);
    }

    [Test]
    public void Count_Triangle_ReturnsZero()
    {
        // Arrange: A - B - C - A
        var vertices = new[] { "A", "B", "C" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "C" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "A", "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = ArticulationPoints.Count(vertices, GetNeighbors);

        // Assert
        result.Should().Be(0);
    }

    [Test]
    public void Find_LargeGraph_FindsAllPoints()
    {
        // Arrange: Large chain
        var vertices = Enumerable.Range(1, 10).ToArray();
        IEnumerable<int> GetNeighbors(int v)
        {
            var neighbors = new List<int>();
            if (v > 1)
            {
                neighbors.Add(v - 1);
            }

            if (v < 10)
            {
                neighbors.Add(v + 1);
            }

            return neighbors;
        }

        // Act
        var result = ArticulationPoints.Find(vertices, GetNeighbors);

        // Assert
        result.Should().HaveCount(8); // All except endpoints
        result.Should().NotContain(new[] { 1, 10 });
    }
}
