using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.Probabilistic
{
    public class HyperLogLog<T> where T : notnull
    {
        private const int P = 16;
        private const double Alpha = .673;
        private readonly int m;
        private readonly int b;
        private readonly int[] registers;
        private readonly HashSet<int> setRegisters;

        public HyperLogLog()
        {
            m = 1 << P;
            b = (int)Math.Ceiling(Math.Log(m));
            registers = new int[m];
            setRegisters = new HashSet<int>();
        }

        public static HyperLogLog<T> Merge(HyperLogLog<T> first, HyperLogLog<T> second)
        {
            var output = new HyperLogLog<T>();
            for (var i = 0; i < second.registers.Length; i++)
            {
                output.registers[i] = Math.Max(first.registers[i], second.registers[i]);
            }

            output.setRegisters.UnionWith(first.setRegisters);
            output.setRegisters.UnionWith(second.setRegisters);
            return output;
        }

        public void Add(T item)
        {
            var x = item.GetHashCode();
            var binString = Convert.ToString(x, 2);
            var j = Convert.ToInt32(binString.Substring(0, Math.Min(P, binString.Length)), 2);
            var w = (int)Math.Log2(x ^ (x & (x - 1))); // find position of the right most 1.
            registers[j] = Math.Max(registers[j], w);
            setRegisters.Add(j);
        }

        public int Cardinality()
        {
            double z = 0;
            foreach (var index in setRegisters)
            {
                z += Math.Pow(2, -1 * registers[index]);
            }

            return (int)Math.Ceiling(Alpha * setRegisters.Count * (setRegisters.Count / z));
        }
    }
}
