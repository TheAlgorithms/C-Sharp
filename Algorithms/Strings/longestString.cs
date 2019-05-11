using System;

/*
     https://www.codewars.com/kata/consecutive-strings/csharp

     You are given an array strarr of strings and an integer k. Your task is to return the first longest string consisting of k consecutive strings taken in the array.

    Example:
    longest_consec(["zone", "abigail", "theta", "form", "libe", "zas", "theta", "abigail"], 2) --> "abigailtheta"

    n being the length of the string array, if n = 0 or k > n or k <= 0 return "".

    Note
    consecutive strings : follow one after another without an interruption
*/
namespace Example
{
    public static partial class StringViewModel
    {
        public static string LongestConsec(string[] arrStr, int kk)
        {
            int counter = 0, cnt = 0;
            string longestString = "";
            string outputer = "";
            for (int i = 0; i < arrStr.Length; i++)
            {
                outputer += arrStr[i];
                if (i == kk - 1)
                {
                    if (outputer.Length > counter)
                    {
                        counter = outputer.Length;
                        longestString = outputer;
                    }
                    outputer = "";
                    kk++;
                    cnt++;
                    i = cnt - 1;
                }
            }
            return longestString;
        }
    }
}
