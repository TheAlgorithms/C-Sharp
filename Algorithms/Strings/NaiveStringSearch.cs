using System;

namespace Algorithms.Strings
{
    /// <summary>
    /// Implements the traditional naive string matching algorithm in C#.
    /// Cost:  O(n*m), where m is pattern length and n is content length.
    /// </summary>
    class NaiveStringSearch
    {
      public static void NaiveSearch(String content, String pattern)
      {
          int m = pattern.Length;
          int n = content.Length;

          for (int e = 0; e <= (n - m); e++)
          {
            int j;

            for (j = 0; j < m; j++)
                {
                  if (content[e + j] != pattern[j])
                  break;
                 }

            if (j == m)
                Console.WriteLine("Pattern occurs in content at index " + e);
           }
      }

      public static void Main()
      {
        String content = "The foxy cheetah hunted a brown fox";
        String pattern = "fox";
        NaiveSearch(content, pattern);
      }
    }

}
