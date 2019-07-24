using System;
using System.Collections.Generic;

namespace AStar
{
    internal class Program
    {
        // Map to use
        private static readonly string[] Map = new string[]
        {
            "+------+",
            "|      |",
            "|A X   |",
            "|XXX   |",
            "|   X  |",
            "| B    |",
            "|      |",
            "+------+",
        };

        // Begin and end
        private static Location end;
        private static Location start;

        // get valid adjacent steps to the current location
        public static List<Location> AdjacentSteps(Location l)
        {
            var proposedLocations = new List<Location>
            {
                new Location(l.X - 1, l.Y, l),
                new Location(l.X, l.Y - 1, l),
                new Location(l.X + 1, l.Y, l),
                new Location(l.X, l.Y + 1, l),
            };

            var actualLocations = new List<Location>();
            foreach (var a in proposedLocations)
            {
                if (Map[a.Y][a.X] == ' ' || Map[a.Y][a.X] == 'B')
                {
                    actualLocations.Add(a);
                }
            }

            return actualLocations;
        }

        // The one and only AStar algorithm
        public static List<Location> AStar()
        {
            // Going there
            var openedList = new List<Location>();

            // Been there
            var closedList = new List<Location>();

            end = FindEnd();
            start = FindStart();

            openedList.Add(start);

            // While there are still nodes to visit
            while (openedList.Count > 0)
            {
                // Get the node that has the best chance
                var bestChoice = MinimumF(openedList);

                // Mark as visited
                _ = openedList.Remove(bestChoice);
                closedList.Add(bestChoice);

                // Did we hit the end?
                if (bestChoice.X == end.X && bestChoice.Y == end.Y)
                {
                    break;
                }

                // Find the next moves
                var adjacentChoices = AdjacentSteps(bestChoice);
                foreach (var l in adjacentChoices)
                {
                    // Been there
                    if (closedList.Contains(l))
                    {
                        continue;
                    }

                    // Haven't gone there yet!
                    if (!openedList.Contains(l))
                    {
                        openedList.Insert(0, l);
                    }

                    // We are going to go there, but did we come from a better path?
                    else
                    {
                        // Find the same location we had
                        var sameLocation = openedList.Find((Location a) => a.X == l.X && a.Y == l.Y);

                        // If our current location is better than the location we found earlier, update it with
                        // our new location
                        if (bestChoice.G + 1 + l.H < sameLocation.F)
                        {
                            _ = openedList.Remove(sameLocation);
                            openedList.Add(l);
                        }
                    }
                }
            }

            // Path to return
            return closedList.Contains(end) ? ReconstructPath(closedList) : null;
        }

        public static int ComputeHScore(int x, int y)
        {
            // If we created a new location for the end node,
            // don't worry about the Hueristic
            var result = end == null ? 0 : Math.Abs(x - end.X) + Math.Abs(y - end.Y);
            return result;
        }

        public static Location FindEnd()
        {
            Location result = null;

            for (var i = 0; i < Map.Length; i++)
            {
                var flag = Map[i].Contains("B");
                if (flag)
                {
                    result = new Location(Map[i].IndexOf('B'), i, null);
                    return result;
                }
            }

            return result;
        }

        public static Location FindStart()
        {
            Location result = null;
            for (var i = 0; i < Map.Length; i++)
            {
                var flag = Map[i].Contains("A");
                if (flag)
                {
                    result = new Location(Map[i].IndexOf('A'), i, null);
                    return result;
                }
            }

            return result;
        }

        // Get the best F score out of the list of locations
        public static Location MinimumF(List<Location> l)
        {
            var min = l[0];
            foreach (var current in l)
            {
                if (current.F < min.F)
                {
                    min = current;
                }
            }

            return min;
        }

        // Reconstructs the path from beginning to end
        public static List<Location> ReconstructPath(List<Location> closedList)
        {
            var path = new List<Location>();

            var location = closedList.Find(x => x.X == end.X && x.Y == end.Y);

            path.Add(location);

            while (location.Parent != null)
            {
                location = location.Parent;
                path.Insert(0, location);
            }

            return path;
        }

        public static void Main()
        {
            var list = AStar();

            if (list == null)
            {
                Console.WriteLine("No solution!");
            }
            else
            {
                Console.WriteLine("Solution found as follows:");
                foreach (var current in list)
                {
                    Console.WriteLine(current.X + ", " + current.Y);
                }
            }
        }
    }
}
