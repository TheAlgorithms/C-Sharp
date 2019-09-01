using System;
using System.Collections.Generic;

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
        private static readonly string[] MapStr = new[]
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
        private static Node end;
        private static Node start;

        private static Node[] map;

        /// <summary>
        /// Gets the Nodes and connecting them.
        /// Returns the cached version once it was generated.
        /// </summary>
        public static Node[] Map
        {
            get
            {
                if (map != null)
                {
                    return map; // Return the cached result
                }

                map = CreateNodeGraph().ToArray();
                return map;
            }
        }

        /// <summary>
        /// Main entry point of the A* Example.
        /// </summary>
        public static void Main()
        {
            map = CreateNodeGraph().ToArray();
            var list = AStar.Compute(start, end);

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

            _ = Console.ReadLine();
        }

        private static List<Node> CreateNodeGraph()
        {
            var ret = GenerateNodeGraph();

            // ConnectNodes
            for (var y = 0; y < MapStr.Length; y++)
            {
                for (var x = 0; x < MapStr[y].Length; x++)
                {
                    ConnectNodes(ret, x, y);
                }
            }

            return ret;
        }

        private static void ConnectNodes(List<Node> ret, int x, int y)
        {
            // Index of the node that we are connecting
            var idx = y * MapStr[y].Length + x;

            // Fill the Start/End nodes from the string array
            if (MapStr[y][x] == 'A')
            {
                start = ret[idx];
            }
            else if (MapStr[y][x] == 'B')
            {
                end = ret[idx];
            }
            else
            {
                // ...
            }

            var connections = new List<Node>();

            // Loop through the neighbours
            for (var i = y - 1; i <= y + 1; i++)
            {
                for (var j = x - 1; j <= x + 1; j++)
                {
                    if (i < 0 || j < 0 || i >= MapStr.Length || j >= MapStr[i].Length)
                    {
                        continue;
                    }

                    var connectionIndex = i * MapStr[y].Length + j;
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
            var ret = new List<Node>();
            for (var y = 0; y < MapStr.Length; y++)
            {
                for (var x = 0; x < MapStr[y].Length; x++)
                {
                    var trav = MapStr[y][x] == ' ' || MapStr[y][x] == 'B' || MapStr[y][x] == 'A';
                    ret.Add(new Node(new VecN(x, y), trav, 1));

                    // Create Nodes and make them traversable when empty, start or end node
                }
            }

            return ret;
        }
    }
}
