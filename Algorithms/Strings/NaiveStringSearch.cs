using System;
using System.Collections.Generic;

/// <summary>
/// Implements the traditional naive string matching algorithm in C# for TheAlgorithms/C-Sharp.
/// </summary>
namespace Algorithms.Strings
{
	/// <summary>
	/// Implements the traditional naive string matching algorithm in C#.
	/// </summary>
	public static class NaiveStringSearch
	{
		public static int[] NaiveSearch(String content, String pattern)
		{
			/// <summary>
			/// NaiveSearch(Content, Pattern) will return an array containing each index of Content in which Pattern appears.
			/// Cost:  O(n*m)
			/// <param name="content">The text body across which to search for a given pattern</param>
			/// <param name="pattern">The pattern against which to check the given text body.</param>
			/// </summary>
			int m = pattern.Length;
			int n = content.Length;
			List<int> indices = new List<int>();
			for (int e = 0; e <= (n - m); e++)
			{
				int j;
				for (j = 0; j < m; j++)
				{
					if (content[e + j] != pattern[j])
					{
						break;
					}
				}
				if (j == m)
				{
					indices.Add(e);
				}
			}

			return indices.ToArray();
		}
		public static void Main()
		{
			/// <summary>
			/// Main() demonstrates the Naive string search against a sample content body and pattern.
			/// </summary>
			String content = "The foxy cheetah hunted a brown fox";
			String pattern = "fox";
			NaiveSearch(content, pattern);
		}
	}
}
