using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AStar
{
    internal class Program
    {
        // Begin and end
        public static Node End;
        public static Node Start;
        private static Node[] _map = null;
        // Map to use

        /// <summary>
        /// The string representation of the map
        /// A = Start
        /// B = End
        /// X = Wall
        /// Q = Quicksand(Traversal Multiplier: 5)
        /// </summary>
        private static string[] map = new string[]
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

        /// <summary>
        /// Creating the Nodes and connecting them.
        /// Returns the cached version once it was generated.
        /// </summary>
        public static Node[] Map
        {
            get
            {
                if (_map != null) return _map; // Return the cached result
                List<Node> ret = new List<Node>();
                for (int y = 0; y < map.Length; y++)
                {
                    for (int x = 0; x < map[y].Length; x++)
                    {
                        bool trav = map[y][x] == ' ' || map[y][x] == 'B' || map[y][x] == 'A';
                        ret.Add(new Node(new VecN(x, y), null, trav));
                        // Create Nodes and make them traversable when empty, start or end node
                    }
                }

                for (int y = 0; y < map.Length; y++)
                {
                    for (int x = 0; x < map[y].Length; x++)
                    {
                        // Index of the node that we are connecting
                        var idx = y * map[y].Length + x;

                        // Fill the Start/End nodes from the string array
                        if (map[y][x] == 'A') Start = ret[idx];
                        else if (map[y][x] == 'B') End = ret[idx];

                        List<Node> connections = new List<Node>();
                        // Loop through the neighbours
                        for (int i = y - 1; i <= y + 1; i++)
                        {
                            for (int j = x - 1; j <= x + 1; j++)
                            {
                                if (i < 0 || j < 0 || i >= map.Length || j >= map[i].Length) continue;

                                var connectionIndex = i * map[y].Length + j;
                                if (idx != connectionIndex) // If not self
                                    connections.Add(ret[connectionIndex]);
                            }
                        }

                        ret[idx].ConnectedNodes = connections.ToArray();
                    }
                }

                _map = ret.ToArray();
                return _map;
            }
        }

        public static void Main()
        {
            List<Node> map = Map.ToList();
            var list = AStar.Compute(Start, End);

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
    }
}
