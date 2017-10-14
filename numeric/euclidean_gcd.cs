using System;

namespace numeric
{
    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Please enter two positive integers separated by space: ");
                string input = Console.ReadLine();
                string[] integers = input.Split(' ');
                if (integers.Length == 2)
                {
                    int a;
                    int b;

                    if ((Int32.TryParse(integers[0], out a) &&
                        Int32.TryParse(integers[1], out b)))
                    {
                        int value = euclidean_gcd(a, b);
                        Console.WriteLine("Greates common divisor of values " + a + " and " + b + " is " + value + ".");
                    }

                }
                Console.WriteLine();
            }
        }

        public static int euclidean_gcd(int a, int b)
        {
            if (a == 0)
            {
                return b;
            }
            else if (b == 0)
            {
                return a;
            }

            int aa = a;
            int bb = b;
            int cc = aa % bb;

            while (cc != 0)
            {
                aa = bb;
                bb = cc;
                cc = aa % bb;
            }

            return bb;
        }
    }
}