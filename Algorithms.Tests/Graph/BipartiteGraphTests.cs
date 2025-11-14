using System;
using System.Collections.Generic;
using System.Linq;
using Algorithms.Graph;
using FluentAssertions;
using NUnit.Framework;

namespace Algorithms.Tests.Graph;

public class BipartiteGraphTests
{
    [Test]
    public void IsBipartite_EmptyGraph_ReturnsTrue()
    {
        // Arrange
        var vertices = Array.Empty<string>();
        IEnumerable<string> GetNeighbors(string v) => Array.Empty<string>();

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void IsBipartite_SingleVertex_ReturnsTrue()
    {
        // Arrange
        var vertices = new[] { "A" };
        IEnumerable<string> GetNeighbors(string v) => Array.Empty<string>();

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void IsBipartite_TwoVerticesConnected_ReturnsTrue()
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
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void IsBipartite_Triangle_ReturnsFalse()
    {
        // Arrange: A - B - C - A (odd cycle)
        var vertices = new[] { "A", "B", "C" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "C" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "A", "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void IsBipartite_Square_ReturnsTrue()
    {
        // Arrange: A - B - C - D - A (even cycle)
        var vertices = new[] { "A", "B", "C", "D" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "D" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B", "D" },
            "D" => new[] { "A", "C" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void IsBipartite_CompleteBipartiteK23_ReturnsTrue()
    {
        // Arrange: Complete bipartite K(2,3)
        var vertices = new[] { "A", "B", "X", "Y", "Z" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "X", "Y", "Z" },
            "B" => new[] { "X", "Y", "Z" },
            "X" => new[] { "A", "B" },
            "Y" => new[] { "A", "B" },
            "Z" => new[] { "A", "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void IsBipartite_DisconnectedBipartiteComponents_ReturnsTrue()
    {
        // Arrange: (A-B) and (C-D)
        var vertices = new[] { "A", "B", "C", "D" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A" },
            "C" => new[] { "D" },
            "D" => new[] { "C" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void IsBipartite_DisconnectedWithOddCycle_ReturnsFalse()
    {
        // Arrange: (A-B) and (C-D-E-C triangle)
        var vertices = new[] { "A", "B", "C", "D", "E" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A" },
            "C" => new[] { "D", "E" },
            "D" => new[] { "C", "E" },
            "E" => new[] { "C", "D" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void IsBipartite_StarGraph_ReturnsTrue()
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
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void IsBipartite_Pentagon_ReturnsFalse()
    {
        // Arrange: Pentagon (5-cycle)
        var vertices = new[] { 1, 2, 3, 4, 5 };
        IEnumerable<int> GetNeighbors(int v) => v switch
        {
            1 => new[] { 2, 5 },
            2 => new[] { 1, 3 },
            3 => new[] { 2, 4 },
            4 => new[] { 3, 5 },
            5 => new[] { 1, 4 },
            _ => Array.Empty<int>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void IsBipartite_NullVertices_ThrowsArgumentNullException()
    {
        // Act
        Action act = () => BipartiteGraph.IsBipartite<string>(null!, v => Array.Empty<string>());

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("vertices");
    }

    [Test]
    public void IsBipartite_NullGetNeighbors_ThrowsArgumentNullException()
    {
        // Arrange
        var vertices = new[] { "A" };

        // Act
        Action act = () => BipartiteGraph.IsBipartite(vertices, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("getNeighbors");
    }

    [Test]
    public void GetPartitions_BipartiteGraph_ReturnsCorrectSets()
    {
        // Arrange: A - B - C - D (chain)
        var vertices = new[] { "A", "B", "C", "D" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B", "D" },
            "D" => new[] { "C" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.GetPartitions(vertices, GetNeighbors);

        // Assert
        result.Should().NotBeNull();
        result!.Value.SetA.Should().Contain(new[] { "A", "C" });
        result.Value.SetB.Should().Contain(new[] { "B", "D" });
    }

    [Test]
    public void GetPartitions_NonBipartiteGraph_ReturnsNull()
    {
        // Arrange: Triangle
        var vertices = new[] { "A", "B", "C" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "C" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "A", "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.GetPartitions(vertices, GetNeighbors);

        // Assert
        result.Should().BeNull();
    }

    [Test]
    public void GetPartitions_EmptyGraph_ReturnsEmptySets()
    {
        // Arrange
        var vertices = Array.Empty<string>();
        IEnumerable<string> GetNeighbors(string v) => Array.Empty<string>();

        // Act
        var result = BipartiteGraph.GetPartitions(vertices, GetNeighbors);

        // Assert
        result.Should().NotBeNull();
        result!.Value.SetA.Should().BeEmpty();
        result.Value.SetB.Should().BeEmpty();
    }

    [Test]
    public void GetPartitions_NullVertices_ThrowsArgumentNullException()
    {
        // Act
        Action act = () => BipartiteGraph.GetPartitions<string>(null!, v => Array.Empty<string>());

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("vertices");
    }

    [Test]
    public void GetPartitions_NullGetNeighbors_ThrowsArgumentNullException()
    {
        // Arrange
        var vertices = new[] { "A" };

        // Act
        Action act = () => BipartiteGraph.GetPartitions(vertices, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("getNeighbors");
    }

    [Test]
    public void IsBipartiteDfs_BipartiteGraph_ReturnsTrue()
    {
        // Arrange: Square
        var vertices = new[] { "A", "B", "C", "D" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "D" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "B", "D" },
            "D" => new[] { "A", "C" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartiteDfs(vertices, GetNeighbors);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void IsBipartiteDfs_NonBipartiteGraph_ReturnsFalse()
    {
        // Arrange: Triangle
        var vertices = new[] { "A", "B", "C" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "B", "C" },
            "B" => new[] { "A", "C" },
            "C" => new[] { "A", "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.IsBipartiteDfs(vertices, GetNeighbors);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void IsBipartiteDfs_NullVertices_ThrowsArgumentNullException()
    {
        // Act
        Action act = () => BipartiteGraph.IsBipartiteDfs<string>(null!, v => Array.Empty<string>());

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("vertices");
    }

    [Test]
    public void IsBipartiteDfs_NullGetNeighbors_ThrowsArgumentNullException()
    {
        // Arrange
        var vertices = new[] { "A" };

        // Act
        Action act = () => BipartiteGraph.IsBipartiteDfs(vertices, null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("getNeighbors");
    }

    [Test]
    public void IsBipartite_LargeEvenCycle_ReturnsTrue()
    {
        // Arrange: Large even cycle (100 vertices)
        var vertices = Enumerable.Range(0, 100).ToArray();
        IEnumerable<int> GetNeighbors(int v)
        {
            var neighbors = new List<int>
            {
                v > 0 ? v - 1 : 99, // Previous or close cycle
                v < 99 ? v + 1 : 0, // Next or close cycle
            };

            return neighbors;
        }

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);

        // Assert
        result.Should().BeTrue();
    }

    [Test]
    public void IsBipartite_LargeOddCycle_ReturnsFalse()
    {
        // Arrange: Large odd cycle (101 vertices)
        var vertices = Enumerable.Range(0, 101).ToArray();
        IEnumerable<int> GetNeighbors(int v)
        {
            var neighbors = new List<int>
            {
                v > 0 ? v - 1 : 100, // Previous or close cycle
                v < 100 ? v + 1 : 0, // Next or close cycle
            };

            return neighbors;
        }

        // Act
        var result = BipartiteGraph.IsBipartite(vertices, GetNeighbors);

        // Assert
        result.Should().BeFalse();
    }

    [Test]
    public void GetPartitions_CompleteBipartite_ReturnsCorrectSets()
    {
        // Arrange: K(2,2)
        var vertices = new[] { "A", "B", "X", "Y" };
        IEnumerable<string> GetNeighbors(string v) => v switch
        {
            "A" => new[] { "X", "Y" },
            "B" => new[] { "X", "Y" },
            "X" => new[] { "A", "B" },
            "Y" => new[] { "A", "B" },
            _ => Array.Empty<string>(),
        };

        // Act
        var result = BipartiteGraph.GetPartitions(vertices, GetNeighbors);

        // Assert
        result.Should().NotBeNull();
        result!.Value.SetA.Should().HaveCount(2);
        result.Value.SetB.Should().HaveCount(2);
    }
}
