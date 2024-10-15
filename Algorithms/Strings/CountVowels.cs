using System;
namespace Algorithms.Strings;

public static class Vowels {
  // comment
  // In main method we are reading the input and calling CountVowels method to count the vowels of a given string
  // comment
  public static void Main(string[] args) {
    string input= Console.ReadLine();
    CountVowels(input.Trim());
  }
  // comment
  // In CountVowels Method we are printing the count of vowels in given string
  // comment
  public static void CountVowels(string input)
  {
      string vowels = "aeiouAEIOU";
      int count=0;
      for(int i=0;i<input.Length;i++)
      {
         for(int j=0;j<vowels.Length;j++)
         {
             if(input[i]==vowels[j])
             {
                 count+=1;
                 break;
             }
         }
      }
      Console.WriteLine($"No of Vowels in a given string is {count}");
  }
}
