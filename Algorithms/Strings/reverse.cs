using System;

public static class ReversedString
{
    //comment
    // In this Main Method we are taking the input and calling ReverseString Method which will return reversedString and we will print in main method
    // comment
    
    public static void Main (string[] args)
    {
        // Sameple Input: "reverse"
        // Sample Output: "esrever"
        string input = Console.ReadLine();
        string reversedString = ReverseString(input.Trim());
        Console.WriteLine(reversedString);
    }

    // comment
    //This Method will take input as string and return the reversed string.
    // comment
        
    public static string ReverseString (string input)
    {
        // empty string
        string reverseString = "";
        int length = input.Length;
        while(length!=0)
        {
            reverseString = reverseString+input[length-1];
            length-=1;
        }
        return reverseString;
    }
    
}
