using System;

namespace search_string
{
    class Program
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("Please enter the outer string which you want to be searched in:");
            String outerStr = Console.ReadLine();

            Console.WriteLine("Now enter the string to be searched:");
            String innerStr = Console.ReadLine();

            int index = SearchString(outerStr, innerStr);
            if (index == -1)
                Console.WriteLine("String does not contain the string!");
            else
                Console.WriteLine("String begins at index " + index);
        }

        private static int SearchString(String outerStr, String innerStr)
        {
            int charPos = 0;
            for (int i = 0; i < outerStr.Length; i++)
            {
                if (outerStr[i] == innerStr[charPos])
                {
                    if (charPos == innerStr.Length - 1)
                        return i - charPos;
                    charPos++;
                }
                else
                    charPos = 0;
            }

            return -1;
        }
    }
}