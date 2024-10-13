using System;
using System.Text;

namespace Algorithms.Strings;

public static class ReversedString
{
    //comment
    //In this Main Method we are taking the input and calling ReverseString Method which will return reversedString and we will print in main Method
    //comment
    public static void Main(string[] args)
    {
        // Sample Input : "reverse"
        // Sample Output : "esrever"
        string input= Console.ReadLine();
        ReverseString(input.Trim());
    }
    
    //comment
    //This Method will take input as string and return the reversed string
    //comment
    
    public static void ReverseString (string input)
    {
        StringBuilder reverseString= new StringBuilder();
        int length = input.Length;
        while(length!=0)
        {
            reverseString.Append(input[length-1]);
            length-=1;
        }
        Console.WriteLine(reverseString);
    }
}
