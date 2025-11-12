using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Graph;

/// <summary>
/// A* (A-Star) pathfinding algorithm implementation.
/// Finds the shortest path between two nodes using a heuristic function.
/// </summary>
public static class AStar
{
    /// <summary>
    /// Finds the shortest path from start to goal using A* algorithm.
    /// </summary>
    /// <typeparam name="T">Type of node identifier.</typeparam>
    /// <param name="start">Starting node.</param>
    /// <param name="goal">Goal node.</param>
    /// <param name="getNeighbors">Function to get neighbors of a node with their costs.</param>
    /// <param name="heuristic">Heuristic function estimating cost from node to goal.</param>
    /// <returns>List of nodes representing the path from start to goal, or null if no path exists.</returns>
    public static List<T>? FindPath<T>(
        T start,
        T goal,
        Func<T, IEnumerable<(T node, double cost)>> getNeighbors,
        Func<T, T, double> heuristic) where T : notnull
    {
        if (start == null)
        {
            throw new ArgumentNullException(nameof(start));
        }

        if (goal == null)
        {
            throw new ArgumentNullException(nameof(goal));
        }

        if (getNeighbors == null)
        {
            throw new ArgumentNullException(nameof(getNeighbors));
        }

        if (heuristic == null)
        {
            throw new ArgumentNullException(nameof(heuristic));
        }

        var openSet = new PriorityQueue<T, double>();
        var cameFrom = new Dictionary<T, T>();
        var gScore = new Dictionary<T, double> { [start] = 0 };
        var fScore = new Dictionary<T, double> { [start] = heuristic(start, goal) };

        openSet.Enqueue(start, fScore[start]);

        while (openSet.Count > 0)
        {
            var current = openSet.Dequeue();

            if (EqualityComparer<T>.Default.Equals(current, goal))
            {
                return ReconstructPath(cameFrom, current);
            }

            foreach (var (neighbor, cost) in getNeighbors(current))
            {
                var tentativeGScore = gScore[current] + cost;

                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = tentativeGScore + heuristic(neighbor, goal);

                    openSet.Enqueue(neighbor, fScore[neighbor]);
                }
            }
        }

        return null; // No path found
    }

    /// <summary>
    /// Finds the shortest path in a 2D grid from start to goal.
    /// </summary>
    /// <param name="grid">2D grid where true represents walkable cells and false represents obstacles.</param>
    /// <param name="start">Starting position (row, col).</param>
    /// <param name="goal">Goal position (row, col).</param>
    /// <param name="allowDiagonal">Whether diagonal movement is allowed.</param>
    /// <returns>List of positions representing the path, or null if no path exists.</returns>
    public static List<(int row, int col)>? FindPathInGrid(
        bool[,] grid,
        (int row, int col) start,
        (int row, int col) goal,
        bool allowDiagonal = false)
    {
        if (grid == null)
        {
            throw new ArgumentNullException(nameof(grid));
        }

        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        if (start.row < 0 || start.row >= rows || start.col < 0 || start.col >= cols)
        {
            throw new ArgumentException("Start position is out of bounds.", nameof(start));
        }

        if (goal.row < 0 || goal.row >= rows || goal.col < 0 || goal.col >= cols)
        {
            throw new ArgumentException("Goal position is out of bounds.", nameof(goal));
        }

        if (!grid[start.row, start.col])
        {
            throw new ArgumentException("Start position is not walkable.", nameof(start));
        }

        if (!grid[goal.row, goal.col])
        {
            throw new ArgumentException("Goal position is not walkable.", nameof(goal));
        }

        IEnumerable<((int row, int col) node, double cost)> GetNeighbors((int row, int col) pos)
        {
            var neighbors = new List<((int row, int col), double)>();

            // Cardinal directions (up, down, left, right)
            var directions = new[]
            {
                (-1, 0), (1, 0), (0, -1), (0, 1),
            };

            // Add diagonal directions if allowed
            if (allowDiagonal)
            {
                directions = directions.Concat(new[]
                {
                    (-1, -1), (-1, 1), (1, -1), (1, 1),
                }).ToArray();
            }

            foreach (var (dr, dc) in directions)
            {
                int newRow = pos.row + dr;
                int newCol = pos.col + dc;

                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols && grid[newRow, newCol])
                {
                    // Cost is sqrt(2) for diagonal, 1 for cardinal
                    double cost = (dr != 0 && dc != 0) ? Math.Sqrt(2) : 1.0;
                    neighbors.Add(((newRow, newCol), cost));
                }
            }

            return neighbors;
        }

        double ManhattanDistance((int row, int col) a, (int row, int col) b)
        {
            return Math.Abs(a.row - b.row) + Math.Abs(a.col - b.col);
        }

        double EuclideanDistance((int row, int col) a, (int row, int col) b)
        {
            int dr = a.row - b.row;
            int dc = a.col - b.col;
            return Math.Sqrt((dr * dr) + (dc * dc));
        }

        // Use Euclidean distance for diagonal movement, Manhattan for cardinal only
        var heuristic = allowDiagonal ? EuclideanDistance : ManhattanDistance;

        return FindPath(start, goal, GetNeighbors, heuristic);
    }

    /// <summary>
    /// Calculates the total cost of a path.
    /// </summary>
    /// <typeparam name="T">Type of node identifier.</typeparam>
    /// <param name="path">The path to calculate cost for.</param>
    /// <param name="getCost">Function to get the cost between two adjacent nodes.</param>
    /// <returns>Total cost of the path.</returns>
    public static double CalculatePathCost<T>(List<T> path, Func<T, T, double> getCost) where T : notnull
    {
        if (path == null || path.Count < 2)
        {
            return 0;
        }

        double totalCost = 0;
        for (int i = 0; i < path.Count - 1; i++)
        {
            totalCost += getCost(path[i], path[i + 1]);
        }

        return totalCost;
    }

    private static List<T> ReconstructPath<T>(Dictionary<T, T> cameFrom, T current) where T : notnull
    {
        var path = new List<T> { current };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Insert(0, current);
        }

        return path;
    }
}
