using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AStar
{
    /// <summary>
    /// Test Program that will be used to solve a pathfinding problem with A*.
    /// </summary>
    internal class Program
    {
        // Map to use

        /// <summary>
        /// The string representation of the map
        /// A = Start
        /// B = End
        /// X = Wall
        /// Q = Quicksand(Traversal Multiplier: 5).
        /// </summary>
        private static readonly string[] map = new[]
        {
            "+------+",
            "|      |",
            "|A X   |",
            "|XXX   |",
            "|  QX  |",
            "| B    |",
            "|      |",
            "+------+",
        };

        // Begin and end
        private static Node _end;
        private static Node _start;

        private static Node[] _map;

        /// <summary>
        /// Gets the Nodes and connecting them.
        /// Returns the cached version once it was generated.
        /// </summary>
        public static Node[] Map
        {
            get
            {
                if (_map != null)
                {
                    return _map; // Return the cached result
                }

                _map = CreateNodeGraph().ToArray();
                return _map;
            }
        }

        /// <summary>
        /// Main entry point of the A* Example.
        /// </summary>
        public static void Main()
        {
            _map = CreateNodeGraph().ToArray();
            var list = AStar.Compute(_start, _end);

            if (list.Count == 0)
            {
                Console.WriteLine("No solution!");
            }
            else
            {
                Console.WriteLine("Solution found as follows:");
                foreach (var current in list)
                {
                    Console.WriteLine(current.Position);
                }
            }

            Console.ReadLine();
        }

        private static List<Node> CreateNodeGraph()
        {
            List<Node> ret = GenerateNodeGraph();

            // ConnectNodes
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    ConnectNodes(ret, x, y);
                }
            }

            return ret;
        }

        private static void ConnectNodes(List<Node> ret, int x, int y)
        {
            // Index of the node that we are connecting
            var idx = y * map[y].Length + x;

            // Fill the Start/End nodes from the string array
            if (map[y][x] == 'A')
            {
                _start = ret[idx];
            }
            else if (map[y][x] == 'B')
            {
                _end = ret[idx];
            }
            else
            {
                // ...
            }

            List<Node> connections = new List<Node>();

            // Loop through the neighbours
            for (int i = y - 1; i <= y + 1; i++)
            {
                for (int j = x - 1; j <= x + 1; j++)
                {
                    if (i < 0 || j < 0 || i >= map.Length || j >= map[i].Length)
                    {
                        continue;
                    }

                    var connectionIndex = i * map[y].Length + j;
                    if (idx != connectionIndex)
                    {
                        connections.Add(ret[connectionIndex]);
                    }
                }
            }

            ret[idx].ConnectedNodes = connections.ToArray();
        }

        private static List<Node> GenerateNodeGraph()
        {
            List<Node> ret = new List<Node>();
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    bool trav = map[y][x] == ' ' || map[y][x] == 'B' || map[y][x] == 'A';
                    ret.Add(new Node(new VecN(x, y), trav, 1));

                    // Create Nodes and make them traversable when empty, start or end node
                }
            }

            return ret;
        }
    }
}
