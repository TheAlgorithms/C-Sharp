using System;
using System.Collections.Generic;

namespace AStar
{
	internal class Program
	{
        //Map to use
		public static string[] map = new string[] {
			"+------+",
			"|      |",
			"|A X   |",
			"|XXX   |",
			"|   X  |",
			"| B    |",
			"|      |",
			"+------+"
		};

        //Begin and end
		public static Location end;
		public static Location start;

        //get valid adjacent steps to the current location
		public static List<Location> adjacentSteps(Location l)
		{
			List<Location> proposedLocations = new List<Location> {
				new Location (l.X - 1, l.Y, l),
				new Location (l.X, l.Y - 1, l),
				new Location (l.X + 1, l.Y, l),
				new Location (l.X, l.Y + 1, l)
			};

			List<Location> actualLocations = new List<Location>();
            foreach (Location a in proposedLocations)
            {	
				if (Program.map[a.Y][a.X] == ' ' || Program.map[a.Y][a.X] == 'B')
				{
                    actualLocations.Add(a);
				}
			}

            return actualLocations;
		}

        //The one and only AStar algorithm
		public static List<Location> AStar()
		{
            //Going there
            List<Location> OpenedList = new List<Location>();

            //Been there
            List<Location> ClosedList = new List<Location>();

			Program.end = Program.FindEnd();
			Program.start = Program.FindStart();

			OpenedList.Add(Program.start);

            //While there are still nodes to visit
			while (OpenedList.Count > 0)
			{
                //Get the node that has the best chance
                Location bestChoice = Program.MinimumF(OpenedList);

                //Mark as visited
				OpenedList.Remove(bestChoice);
				ClosedList.Add(bestChoice);

                //Did we hit the end?
				if (bestChoice.X == Program.end.X && bestChoice.Y == Program.end.Y)
				{
					break;
				}

                //Find the next moves
                List<Location> adjacentChoices = Program.adjacentSteps(bestChoice);
                foreach (Location l in adjacentChoices)
				{
                    //Been there
                    if (ClosedList.Contains(l))
                        continue;

                    //Haven't gone there yet!
                    if (!OpenedList.Contains(l))
                    {
                        OpenedList.Insert(0, l);
                    }

                    //We are going to go there, but did we come from a better path?
                    else
                    {
                        //Find the same location we had
                        Location sameLocation = OpenedList.Find((Location a) => a.X == l.X && a.Y == l.Y);

                        //If our current location is better than the location we found earlier, update it with 
                        //our new location
                        if (bestChoice.G + 1 + l.H < sameLocation.F)
                        {
                            OpenedList.Remove(sameLocation);
                            OpenedList.Add(l);
                        }
                    }
				}
			}

            //Path to return
			List<Location> result;


			if (!ClosedList.Contains(Program.end))
				result = null;
            
			else
				result = ReconstructPath(ClosedList);
			

			return result;
		}

		public static int ComputeHScore(int x, int y)
		{
            //If we created a new location for the end node, 
            //don't worry about the Hueristic
            int result;

			if (Program.end == null)
				result = 0;
			
			else
				result = Math.Abs(x - Program.end.X) + Math.Abs(y - Program.end.Y);
			
			return result;
		}

		public static Location FindEnd()
		{
			Location result = null;
			for (int i = 0; i < Program.map.Length; i++)
			{
				bool flag = Program.map[i].Contains("B");
				if (flag)
				{
					result = new Location(Program.map[i].IndexOf('B'), i, null);
					return result;
				}
			}
			
			return result;
		}

		public static Location FindStart()
		{
			Location result = null;
			for (int i = 0; i < Program.map.Length; i++)
			{
				bool flag = Program.map[i].Contains("A");
				if (flag)
				{
					result = new Location(Program.map[i].IndexOf('A'), i, null);
					return result;
				}
			}

			return result;
		}

        //Get the best F score out of the list of locations
		public static Location MinimumF(List<Location> l)
		{
			Location min = l[0];
			foreach (Location current in l)
			{
				if (current.F < min.F)
				{
					min = current;
				}
			}
			return min;
		}

        //Reconstructs the path from beginning to end
		public static List<Location> ReconstructPath(List<Location> ClosedList)
		{
			List<Location> path = new List<Location>();

			Location location = ClosedList.Find(x => x.X == end.X && x.Y == end.Y);

			path.Add(location);

			while (location.Parent != null)
			{
				location = location.Parent;
                path.Insert(0, location);
			}

			return path;
		}

		public static void Main(string[] args)
        {
            List<Location> list = Program.AStar();
            
            if (list == null)
            {
                Console.WriteLine("No solution!");
            }
            else
            {
                Console.WriteLine("Solution found as follows:");
                foreach (Location current in list)
                {
                    Console.WriteLine(current.X + ", " + current.Y);
                }
            }
        }
    }
}
