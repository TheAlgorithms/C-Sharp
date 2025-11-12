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
        Func<T, IEnumerable<(T Node, double Cost)>> getNeighbors,
        Func<T, T, double> heuristic) where T : notnull
    {
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
    public static List<(int Row, int Col)>? FindPathInGrid(
        bool[,] grid,
        (int Row, int Col) start,
        (int Row, int Col) goal,
        bool allowDiagonal = false)
    {
        if (grid == null)
        {
            throw new ArgumentNullException(nameof(grid));
        }

        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        ValidateGridPosition(grid, start, rows, cols, nameof(start));
        ValidateGridPosition(grid, goal, rows, cols, nameof(goal));

        IEnumerable<((int Row, int Col) Node, double Cost)> GetNeighbors((int Row, int Col) pos)
        {
            return GetGridNeighbors(pos, rows, cols, grid, allowDiagonal);
        }

        double ManhattanDistance((int Row, int Col) a, (int Row, int Col) b)
        {
            return Math.Abs(a.Row - b.Row) + Math.Abs(a.Col - b.Col);
        }

        double EuclideanDistance((int Row, int Col) a, (int Row, int Col) b)
        {
            int dr = a.Row - b.Row;
            int dc = a.Col - b.Col;
            return Math.Sqrt((dr * dr) + (dc * dc));
        }

        Func<(int Row, int Col), (int Row, int Col), double> heuristic = allowDiagonal
            ? EuclideanDistance
            : ManhattanDistance;

        return FindPath(start, goal, GetNeighbors, heuristic);
    }

    private static void ValidateGridPosition(
        bool[,] grid,
        (int Row, int Col) position,
        int rows,
        int cols,
        string paramName)
    {
        bool isOutOfBounds = position.Row < 0 || position.Row >= rows;
        isOutOfBounds = isOutOfBounds || position.Col < 0 || position.Col >= cols;

        if (isOutOfBounds)
        {
            throw new ArgumentException("Position is out of bounds.", paramName);
        }

        if (!grid[position.Row, position.Col])
        {
            throw new ArgumentException("Position is not walkable.", paramName);
        }
    }

    private static IEnumerable<((int Row, int Col), double)> GetGridNeighbors(
        (int Row, int Col) pos,
        int rows,
        int cols,
        bool[,] grid,
        bool allowDiagonal)
    {
        var neighbors = new List<((int Row, int Col), double)>();

        var directions = new[]
            {
                (-1, 0), (1, 0), (0, -1), (0, 1),
        };

        if (allowDiagonal)
        {
            directions = directions.Concat(new[]
                {
                    (-1, -1), (-1, 1), (1, -1), (1, 1),
            }).ToArray();
        }

        foreach (var (dr, dc) in directions)
            {
                int newRow = pos.Row + dr;
                int newCol = pos.Col + dc;

                bool isInBounds = newRow >= 0 && newRow < rows;
                isInBounds = isInBounds && newCol >= 0 && newCol < cols;

                if (isInBounds && grid[newRow, newCol])
                {
                    // Cost is sqrt(2) for diagonal, 1 for cardinal
                    bool isDiagonal = dr != 0 && dc != 0;
                    double cost = isDiagonal ? Math.Sqrt(2) : 1.0;
                    neighbors.Add(((newRow, newCol), cost));
            }
        }

        return neighbors;
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
