using System;
using System.Linq;

namespace VowelCheck_Con
{
    class Program
    {
        static void Main()
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
            var n = Convert.ToInt32(Console.ReadLine());
            if (n > 1 && n < 10000)
            {
                var s = Console.ReadLine();
                if (s.Length <= n)
                {
                    if (!s.Any(x => vowels.Contains(x)))
                    {
                        Console.WriteLine("NO");
                    }
                    else
                    {
                        Console.WriteLine("YES");
                    }
                }
                else
                {
                    Console.WriteLine("String length should be less than " + n + "");
                }
            }
            else
            {
                Console.WriteLine("Enter a value which is >1 and <10000");
            }

            Console.ReadKey();
        }
    }
}
