using System;
using System.Collections.Generic;
using System.Linq;

namespace sieve_of_Eratosthenes
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Enter the number of prime numbers:");
			var numStr = Console.ReadLine();

			int num;
			if (!int.TryParse(numStr, out num) | num < 1)
			{
				Console.WriteLine("Invalid input! Please enter a number higher than 0");
				return;
			}

			Console.WriteLine(num + " prime numbers: " + string.Join(", ", GetPrimeNumbers(num)));
		}

		public static List<int> GetPrimeNumbers(int count)
		{
			var output = new List<int>();

			for (var i = 2; i < int.MaxValue && count > 0; i++)
			{
				if (output.Any(x => i % x == 0))
                {
                    continue;
                }

                output.Add(i);
				count--;
			}
			
			return output;
		}
	}
}
