using System;
using Algorithms.Knapsack;
using NUnit.Framework;
using FluentAssertions;

namespace Algorithms.Tests.Knapsack;

public static class BranchAndBoundKnapsackSolverTests
{
    [Test]
    public static void BranchAndBoundTest_Example1_Success()
    {
        // Arrange
        var items = new[] { 'A', 'B', 'C', 'D' };
        var values = new[] { 18, 20, 14, 18 };
        var weights = new[] { 2, 4, 6, 9 };

        var capacity = 15;

        Func<char, int> weightSelector = x => weights[Array.IndexOf(items, x)];
        Func<char, double> valueSelector = x => values[Array.IndexOf(items, x)];

        // Act
        var solver = new BranchAndBoundKnapsackSolver<char>();
        var actualResult = solver.Solve(items, capacity, weightSelector, valueSelector);

        // Assert
        actualResult.Should().BeEquivalentTo(new[] { 'A', 'B', 'D' });
    }

    [Test]
    public static void BranchAndBoundTest_Example2_Success()
    {
        // Arrange
        var items = new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
        var values = new[] { 505, 352, 458, 220, 354, 414, 498, 545, 473, 543 };
        var weights = new[] { 23, 26, 20, 18, 32, 27, 29, 26, 30, 27 };

        var capacity = 67;

        Func<char, int> weightSelector = x => weights[Array.IndexOf(items, x)];
        Func<char, double> valueSelector = x => values[Array.IndexOf(items, x)];

        // Act
        var solver = new BranchAndBoundKnapsackSolver<char>();
        var actualResult = solver.Solve(items, capacity, weightSelector, valueSelector);

        // Assert
        actualResult.Should().BeEquivalentTo(new[] { 'H', 'D', 'A' });
    }

    [Test]
    public static void BranchAndBoundTest_CapacityIsZero_NothingTaken()
    {
        // Arrange
        var items = new[] { 'A', 'B', 'C', 'D' };
        var values = new[] { 18, 20, 14, 18 };
        var weights = new[] { 2, 4, 6, 9 };

        var capacity = 0;

        Func<char, int> weightSelector = x => weights[Array.IndexOf(items, x)];
        Func<char, double> valueSelector = x => values[Array.IndexOf(items, x)];

        // Act
        var solver = new BranchAndBoundKnapsackSolver<char>();
        var actualResult = solver.Solve(items, capacity, weightSelector, valueSelector);

        // Assert
        actualResult.Should().BeEmpty();
    }

    [Test]
    public static void BranchAndBoundTest_PlentyCapacity_EverythingIsTaken()
    {
        // Arrange
        var items = new[] { 'A', 'B', 'C', 'D' };
        var values = new[] { 18, 20, 14, 18 };
        var weights = new[] { 2, 4, 6, 9 };

        var capacity = 1000;

        Func<char, int> weightSelector = x => weights[Array.IndexOf(items, x)];
        Func<char, double> valueSelector = x => values[Array.IndexOf(items, x)];

        // Act
        var solver = new BranchAndBoundKnapsackSolver<char>();
        var actualResult = solver.Solve(items, capacity, weightSelector, valueSelector);

        // Assert
        actualResult.Should().BeEquivalentTo(items);
    }

    [Test]
    public static void BranchAndBoundTest_NoItems_NothingTaken()
    {
        // Arrange
        var items = Array.Empty<char>();
        var values = Array.Empty<int>();
        var weights = Array.Empty<int>();

        var capacity = 15;

        Func<char, int> weightSelector = x => weights[Array.IndexOf(items, x)];
        Func<char, double> valueSelector = x => values[Array.IndexOf(items, x)];

        // Act
        var solver = new BranchAndBoundKnapsackSolver<char>();
        var actualResult = solver.Solve(items, capacity, weightSelector, valueSelector);

        // Assert
        actualResult.Should().BeEmpty();
    }
}
