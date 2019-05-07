using System;

namespace Algorithms.Numeric
{
    public class Example
    {
        public static void Main()
        {
            while (true)
            {
                Console.Write("Please enter two numbers separated by a space (exit with none inputs): ");
                var input = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (input.Length == 2)
                {
                    if (!int.TryParse(input[0], out var a))
                    {
                        Console.WriteLine("\"{0}\" is not a number.", input[0]);
                    }
                    else if (!int.TryParse(input[1], out var b))
                    {
                        Console.WriteLine("\"{0}\" is not a number.", input[1]);
                    }
                    else
                    {
                        var value = Binary_gcd(a, b);
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
        public static int Binary_gcd(int u, int v)
        {
            // GCD(0,v) == v; GCD(u,0) == u, GCD(0,0) == 0
            if (u == 0)
            {
                return v;
            }

            if (v == 0)
            {
                return u;
            }

            // It has been proven that: GCD(-a, -b) == GCD(-a, b) == GCD(a, -b) == GCD(a, b) 
            if (u < 0)
            {
                u *= -1;
            }

            if (v < 0)
            {
                v *= -1;
            }

            // Let shift := lg K, where K is the greatest power of 2 dividing both u and v. 
            var shift = 0;
            while (((u | v) & 1) == 0)
            {
                u >>= 1;
                v >>= 1;
                shift++;
            }

            while ((u & 1) == 0)
            {
                u >>= 1;
            }

            // From here on, u is always odd.
            do
            {
                /* remove all factors of 2 in v -- they are not common
                 * note: v is not zero, so while will terminate
                 */
                while ((v & 1) == 0)  /* Loop X */
                {
                    v >>= 1;
                }

                /* Now u and v are both odd. Swap if necessary so u <= v,
                   then set v = v - u (which is even). For bignums, the
                   swapping is just pointer movement, and the subtraction
                   can be done in-place.
                   */

                if (u > v)
                {
                    var t = v;
                    v = u;
                    u = t;
                }

                // Here v >= u.
                v -= u;
            } while (v != 0);

            /* restore common factors of 2 */
            return u << shift;
        }
    }
}
