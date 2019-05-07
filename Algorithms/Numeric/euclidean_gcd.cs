using System;

namespace numeric
{
    public class Program
    {
        public static void Main()
        {
            while (true)
            {
                Console.Write("Please enter two positive integers separated by space: ");
                var input = Console.ReadLine();
                var integers = input.Split(' ');
                if (integers.Length == 2)
                {

                    if (int.TryParse(integers[0], out var a) &&
                        int.TryParse(integers[1], out var b))
                    {
                        var value = Euclidean_gcd(a, b);
                        Console.WriteLine("Greates common divisor of values " + a + " and " + b + " is " + value + ".");
                    }

                }
                Console.WriteLine();
            }
        }

        public static int Euclidean_gcd(int a, int b)
        {
            if (a == 0)
            {
                return b;
            }
            else if (b == 0)
            {
                return a;
            }

            var aa = a;
            var bb = b;
            var cc = aa % bb;

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