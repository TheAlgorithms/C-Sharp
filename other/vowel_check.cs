using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VowelCheck_Con
{
    class Program
    {
        static void Main(string[] args)
        {
            string s;
            int a, e, i, o, u, n;
            a = e = i = o = u = 0;
            n = Convert.ToInt32(Console.ReadLine());
            if (n > 1 && n < 10000)
            {
                s = Console.ReadLine();
                if (s.Length <= n)
                {
                    foreach (char c in s)
                    {
                        if (c == 'a') { a++; }
                        else if (c == 'e') { e++; }
                        else if (c == 'i') { i++; }
                        else if (c == 'o') { o++; }
                        else if (c == 'u') { u++; }
                    }
                    if (a == 0 && e == 0 && i == 0 && o == 0 && u == 0)
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

using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'n' };
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
