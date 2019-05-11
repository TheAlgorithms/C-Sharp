using System;
using System.Linq;

namespace DC4
{
    class Program
    {
        class Fano
        {
            public float pro;
            public int[] arr = new int[20];
            public int top;
        }
        static void Main()
        {
            var f = Enumerable.Range(0, 20).Select(ind => new Fano()).ToArray();
            Console.Write("Enter String: ");
            var str = Console.ReadLine().ToLower().Replace(" ", "#");
            Console.WriteLine("Space will be represented by #");
            int n = str.Length, count = 1, flag = 0, pos = 0;
            var d = new int[n];
            var d1 = new int[n];
            var c1 = new char[n];
            var c = str.ToCharArray();
            for (var i = 0; i < n; i++)
            {
                for (var j = i + 1; j < n; j++)
                {
                    if (c[i] == c[j])
                    {
                        count++;
                    }
                }
                d[i] = count;
                count = 1;
            }
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (c[i] == c1[j])
                    {
                        flag++;
                    }
                }
                if (flag == 0)
                {
                    c1[pos] = c[i];
                    d1[pos] = d[i];
                    pos++;
                }
                flag = 0;
            }
            for (var i = 0; i < pos; i++)
            {
                Console.Write("{0}\t", c1[i]);
            }
            Console.WriteLine();
            for (var i = 0; i < pos; i++)
            {
                Console.Write("{0}\t", d1[i]);
            }
            int temp;
            char ch;
            for (var i = 0; i < pos; i++)
            {
                for (var j = i + 1; j < pos; j++)
                {
                    if (d1[i] > d1[j])
                    {
                        temp = d1[i];
                        ch = c1[i];
                        d1[i] = d1[j];
                        c1[i] = c1[j];
                        d1[j] = temp;
                        c1[j] = ch;
                    }
                }
            }
            Console.WriteLine("\n\nAfter Sorting: ");
            for (var i = pos - 1; i >= 0; i--)
            {
                Console.Write("{0}\t", c1[i]);
            }
            Console.WriteLine();
            for (var i = pos - 1; i >= 0; i--)
            {
                Console.Write("{0}\t", d1[i]);
            }
            double infoBit = 0;
            for (var i = 0; i < pos; i++)
            {
                double prob, si;
                prob = d1[i] / (double)n;
                f[i].pro = (float)prob;
                si = -(Math.Log(prob) / Math.Log(2));
                infoBit += si * d1[i];
            }
            Console.WriteLine("\nTotal Information Count: {0}", infoBit + " Bits");
            Console.WriteLine("Number of Bits required before Compression: {0}", n * 8);
            for (var i = 0; i < pos; i++)
            {
                f[i].top = -1;
            }

            Shannon(0, pos - 1, f);
            var sfBits = 0;
            var code = new string[pos];
            Console.WriteLine("\nShannon-Fano Code:");
            for (var i = pos - 1; i >= 0; i--)
            {
                var codec = " ";
                var sfSize = 0;
                Console.Write("{0}: {1}\t", c1[i], d1[i]);
                for (var j = 0; j <= f[i].top; j++)
                {
                    Console.Write("{0}", f[i].arr[j]);
                    codec += f[i].arr[j];
                    sfSize++;
                }
                code[i] = codec;
                sfBits += sfSize * d1[i];
                Console.Write("\n");
            }
            Console.WriteLine("Number of Shannon-Fano Bits: {0}", sfBits);
            var cStr = " ";
            foreach (var item in str)
            {
                var index = Array.IndexOf(c1, item);
                cStr += code.ElementAt(index);
            }
            Console.WriteLine("\nCoded String: ");
            Console.WriteLine(cStr.Replace(" ", ""));
            Console.ReadKey();
        }
        static void Shannon(int l, int h, Fano[] f)
        {
            float set1 = 0, set2 = 0;
            int i, j, k = 0;
            if ((l + 1) == h || l == h || l > h)
            {
                if (l == h || l > h)
                {
                    return;
                }

                f[h].arr[++f[h].top] = 0;
                f[l].arr[++f[l].top] = 1;
                return;
            }
            else
            {
                for (i = l; i <= h - 1; i++)
                {
                    set1 += f[i].pro;
                }

                set2 += f[h].pro;
                var diff1 = set1 - set2;
                if (diff1 < 0)
                {
                    diff1 *= -1;
                }

                j = 2;
                while (j != h - l + 1)
                {
                    k = h - j;
                    set1 = set2 = 0;
                    for (i = l; i <= k; i++)
                    {
                        set1 += f[i].pro;
                    }

                    for (i = h; i > k; i--)
                    {
                        set2 += f[i].pro;
                    }

                    var diff2 = set1 - set2;
                    if (diff2 < 0)
                    {
                        diff2 *= -1;
                    }

                    if (diff2 >= diff1)
                    {
                        break;
                    }

                    diff1 = diff2;
                    j++;
                }
                k++;
                for (i = l; i <= k; i++)
                {
                    f[i].arr[++f[i].top] = 1;
                }

                for (i = k + 1; i <= h; i++)
                {
                    f[i].arr[++f[i].top] = 0;
                }

                Shannon(l, k, f);
                Shannon(k + 1, h, f);
            }
        }
    }
}