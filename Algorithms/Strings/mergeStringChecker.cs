using System;

/*  https://www.codewars.com/kata/merged-string-checker/csharp
    At a job interview, you are challenged to write an algorithm to check if a given string, s, can be formed from two other strings, part1 and part2.

    The restriction is that the characters in part1 and part2 are in the same order as in s.

    The interviewer gives you the following example and tells you to figure out the rest from the given test cases.

    For example:

    'codewars' is a merge from 'cdw' and 'oears':

    s:  c o d e w a r s   = codewars
    part1:  c   d   w         = cdw
    part2:    o   e   a r s   = oears
 */
public class StringMerger
{
    public static bool isMerge(string s, string part1, string part2)
    {
        if (s.Length != part1.Length + part2.Length) return false;
        foreach (var f in s)
        {
            part1 = (part1.Length > 0 && f == part1[0]) ? part1.Remove(0, 1) : part1;
            part2 = (part2.Length > 0 && f == part2[0]) ? part2.Remove(0, 1) : part2;
        }
        return part1.Length == 0 && part2.Length == 0;
    }
}
