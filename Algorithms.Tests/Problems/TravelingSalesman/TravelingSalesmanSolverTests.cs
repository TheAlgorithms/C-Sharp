using Algorithms.Problems.TravelingSalesman;
using NUnit.Framework;

namespace Algorithms.Tests.Problems.TravelingSalesman;

/// <summary>
/// Unit tests for TravelingSalesmanSolver. Covers brute-force and nearest neighbor methods, including edge cases and invalid input.
/// </summary>
[TestFixture]
public class TravelingSalesmanSolverTests
{
    /// <summary>
    /// Tests brute-force TSP solver on a 4-city symmetric distance matrix with known optimal route.
    /// </summary>
    [Test]
    public void SolveBruteForce_KnownOptimalRoute_ReturnsCorrectResult()
    {
        // Distance matrix for 4 cities (symmetric, triangle inequality holds)
        double[,] matrix =
        {
            { 0, 10, 15, 20 },
            { 10, 0, 35, 25 },
            { 15, 35, 0, 30 },
            { 20, 25, 30, 0 }
        };
        var (route, distance) = TravelingSalesmanSolver.SolveBruteForce(matrix);
        // Optimal route: 0 -> 1 -> 3 -> 2 -> 0, total distance = 80
        Assert.That(distance, Is.EqualTo(80));
        Assert.That(route, Is.EquivalentTo(new[] { 0, 1, 3, 2, 0 }));
    }

    /// <summary>
    /// Tests nearest neighbor heuristic on the same matrix. May not be optimal.
    /// </summary>
    [Test]
    public void SolveNearestNeighbor_Heuristic_ReturnsFeasibleRoute()
    {
        double[,] matrix =
        {
            { 0, 10, 15, 20 },
            { 10, 0, 35, 25 },
            { 15, 35, 0, 30 },
            { 20, 25, 30, 0 }
        };
        var (route, distance) = TravelingSalesmanSolver.SolveNearestNeighbor(matrix, 0);
        // Route: 0 -> 1 -> 3 -> 2 -> 0, total distance = 80
        Assert.That(route.Length, Is.EqualTo(5));
        Assert.That(route.First(), Is.EqualTo(0));
        Assert.That(route.Last(), Is.EqualTo(0));
        Assert.That(distance, Is.GreaterThanOrEqualTo(80)); // Heuristic may be optimal or suboptimal
    }

    /// <summary>
    /// Tests nearest neighbor with invalid start index.
    /// </summary>
    [Test]
    public void SolveNearestNeighbor_InvalidStart_ThrowsException()
    {
        double[,] matrix =
        {
            { 0, 1 },
            { 1, 0 }
        };
        Assert.Throws<ArgumentOutOfRangeException>(() => TravelingSalesmanSolver.SolveNearestNeighbor(matrix, -1));
        Assert.Throws<ArgumentOutOfRangeException>(() => TravelingSalesmanSolver.SolveNearestNeighbor(matrix, 2));
    }
    
    /// <summary>
    /// Tests nearest neighbor when no unvisited cities remain (should throw InvalidOperationException).
    /// </summary>
    [Test]
    public void SolveNearestNeighbor_NoUnvisitedCities_ThrowsException()
    {
        // Construct a matrix where one city cannot be reached (simulate unreachable city)
        double[,] matrix =
        {
            { 0, double.MaxValue, 1 },
            { double.MaxValue, 0, double.MaxValue },
            { 1, double.MaxValue, 0 }
        };
        // Start at city 0, city 1 is unreachable from both 0 and 2
        Assert.Throws<InvalidOperationException>(() => TravelingSalesmanSolver.SolveNearestNeighbor(matrix, 0));
    }

    /// <summary>
    /// Tests brute-force and nearest neighbor with non-square matrix.
    /// </summary>
    [Test]
    public void NonSquareMatrix_ThrowsException()
    {
        double[,] matrix = new double[2, 3];
        Assert.Throws<ArgumentException>(() => TravelingSalesmanSolver.SolveBruteForce(matrix));
        Assert.Throws<ArgumentException>(() => TravelingSalesmanSolver.SolveNearestNeighbor(matrix, 0));
    }

    /// <summary>
    /// Tests brute-force with less than two cities (invalid case).
    /// </summary>
    [Test]
    public void SolveBruteForce_TooFewCities_ThrowsException()
    {
        double[,] matrix = { { 0 } };
        Assert.Throws<ArgumentException>(() => TravelingSalesmanSolver.SolveBruteForce(matrix));
    }

    /// <summary>
    /// Tests nearest neighbor with only two cities (trivial case).
    /// </summary>
    [Test]
    public void SolveNearestNeighbor_TwoCities_ReturnsCorrectRoute()
    {
        double[,] matrix =
        {
            { 0, 5 },
            { 5, 0 }
        };
        var (route, distance) = TravelingSalesmanSolver.SolveNearestNeighbor(matrix, 0);
        Assert.That(route, Is.EquivalentTo(new[] { 0, 1, 0 }));
        Assert.That(distance, Is.EqualTo(10));
    }
}
