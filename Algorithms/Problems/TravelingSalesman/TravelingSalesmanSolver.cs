namespace Algorithms.Problems.TravelingSalesman;

/// <summary>
/// Provides methods to solve the Traveling Salesman Problem (TSP) using brute-force and nearest neighbor heuristics.
/// The TSP is a classic optimization problem in which a salesman must visit each city exactly once and return to the starting city, minimizing the total travel distance.
/// </summary>
public static class TravelingSalesmanSolver
{
    /// <summary>
    /// Solves the TSP using brute-force search. This method checks all possible permutations of cities to find the shortest possible route.
    /// WARNING: This approach is only feasible for small numbers of cities due to factorial time complexity.
    /// </summary>
    /// <param name="distanceMatrix">A square matrix where element [i, j] represents the distance from city i to city j.</param>
    /// <returns>A tuple containing the minimal route (as an array of city indices) and the minimal total distance.</returns>
    public static (int[] Route, double Distance) SolveBruteForce(double[,] distanceMatrix)
    {
        int n = distanceMatrix.GetLength(0);
        if (n != distanceMatrix.GetLength(1))
        {
            throw new ArgumentException("Distance matrix must be square.");
        }

        if (n < 2)
        {
            throw new ArgumentException("At least two cities are required.");
        }

        var cities = Enumerable.Range(0, n).ToArray();
        double minDistance = double.MaxValue;
        int[]? bestRoute = null;

        foreach (var perm in Permute(cities.Skip(1).ToArray()))
        {
            var route = new int[n + 1];
            route[0] = 0;
            for (int i = 0; i < perm.Length; i++)
            {
                route[i + 1] = perm[i];
            }

            // Ensure route ends at city 0
            route[n] = 0;

            double dist = 0;
            for (int i = 0; i < n; i++)
            {
                dist += distanceMatrix[route[i], route[i + 1]];
            }

            if (dist < minDistance)
            {
                minDistance = dist;
                bestRoute = (int[])route.Clone();
            }
        }

        return (bestRoute ?? Array.Empty<int>(), minDistance);
    }

    /// <summary>
    /// Solves the TSP using the nearest neighbor heuristic. This method builds a route by always visiting the nearest unvisited city next.
    /// This approach is much faster but may not find the optimal solution.
    /// </summary>
    /// <param name="distanceMatrix">A square matrix where element [i, j] represents the distance from city i to city j.</param>
    /// <param name="start">The starting city index.</param>
    /// <returns>A tuple containing the route (as an array of city indices) and the total distance.</returns>
    public static (int[] Route, double Distance) SolveNearestNeighbor(double[,] distanceMatrix, int start = 0)
    {
        int n = distanceMatrix.GetLength(0);
        if (n != distanceMatrix.GetLength(1))
        {
            throw new ArgumentException("Distance matrix must be square.");
        }

        if (start < 0 || start >= n)
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }

        var visited = new bool[n];
        List<int> route = [start];
        visited[start] = true;
        double totalDistance = 0;
        int current = start;
        for (int step = 1; step < n; step++)
        {
            double minDist = double.MaxValue;
            int next = -1;
            for (int j = 0; j < n; j++)
            {
                if (!visited[j] && distanceMatrix[current, j] < minDist)
                {
                    minDist = distanceMatrix[current, j];
                    next = j;
                }
            }

            if (next == -1)
            {
                throw new InvalidOperationException("No unvisited cities remain.");
            }

            route.Add(next);
            visited[next] = true;
            totalDistance += minDist;
            current = next;
        }

        totalDistance += distanceMatrix[current, start];
        route.Add(start);
        return (route.ToArray(), totalDistance);
    }

    /// <summary>
    /// Generates all permutations of the input array.
    /// Used for brute-force TSP solution.
    /// </summary>
    private static IEnumerable<int[]> Permute(int[] arr)
    {
        if (arr.Length == 1)
        {
            yield return arr;
            yield break;
        }

        for (int i = 0; i < arr.Length; i++)
        {
            var rest = arr.Where((_, idx) => idx != i).ToArray();
            foreach (var perm in Permute(rest))
            {
                yield return [arr[i], ..perm];
            }
        }
    }
}
