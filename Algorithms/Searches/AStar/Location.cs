using System;

namespace AStar
{
    public class Location : IEquatable<Location>
	{
		//
		// Properties
		//
		public int F
		{
			get;
			set;
		}

		public int G
		{
			get;
			set;
		}

		public int H
		{
            get;
		}

		public Location Parent
		{
			get;
			set;
		}

		public int X
		{
            get;
		}

		public int Y
		{
            get;
		}

		//
		// Constructors
		//
		public Location(int a, int b, Location p)
		{
			X = a;
			Y = b;
			Parent = p;
			
			if (p == null)
            {
                G = 0;
            }
            else
            {
                G = p.G + 1;
            }

            H = Program.ComputeHScore(X, Y);
			F = G + H;
		}

        //
        // Methods
        //
        public bool Equals(Location other) => X == other.X && Y == other.Y;
    }
}
