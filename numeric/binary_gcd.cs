using System;

public class Example
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("Please enter two numbers separated by a space: ");
            string[] input = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (input.Length == 2)
            {
                int a;
                int b;

                if (!Int32.TryParse(input[0], out a))
                {
                    Console.WriteLine("\"{0}\" is not a number.", input[0]);
                }
                else if (!Int32.TryParse(input[1], out b))
                {
                    Console.WriteLine("\"{0}\" is not a number.", input[1]);
                }
                else
                {
                    int value = binary_gcd(a, b);
                    Console.WriteLine("Greates common divisor of values {0} and {1} is {2}.", a, b, value);
                }
            }
            else if (input.Length == 0)
            {
                return;
            }
            else
            {
                Console.WriteLine("Give two numbers, {0} were given", input.Length);
            }
            Console.WriteLine();
        }
    }

    // As found on https://en.wikipedia.org/wiki/Binary_GCD_algorithm
    public static int binary_gcd(int u, int v)
    {
        // GCD(0,v) == v; GCD(u,0) == u, GCD(0,0) == 0
        if (u == 0) return v;
        if (v == 0) return u;

        // It has been proven that: GCD(-a, -b) == GCD(-a, b) == GCD(a, -b) == GCD(a, b) 
        if (u < 0) u *= -1;
        if (v < 0) v *= -1;

        // Let shift := lg K, where K is the greatest power of 2 dividing both u and v. 
        int shift = 0;
        while (((u | v) & 1) == 0)
        {
            u >>= 1;
            v >>= 1;
            shift++;
        }

        while ((u & 1) == 0)
            u >>= 1;

        // From here on, u is always odd.
        do
        {
            /* remove all factors of 2 in v -- they are not common
             * note: v is not zero, so while will terminate
             */
            while ((v & 1) == 0)  /* Loop X */
                v >>= 1;

            /* Now u and v are both odd. Swap if necessary so u <= v,
               then set v = v - u (which is even). For bignums, the
               swapping is just pointer movement, and the subtraction
               can be done in-place.
               */

            if (u > v)
            {
                int t = v;
                v = u;
                u = t;
            }

            // Here v >= u.
            v = v - u;
        } while (v != 0);

        /* restore common factors of 2 */
        return u << shift;
    }
}