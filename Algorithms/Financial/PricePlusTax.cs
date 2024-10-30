using System;
using System.Numerics;

namespace Algorithms.Financial
{
    public static class PricePlusTax
    {
        public static double Calculate(double price, double taxRate)
        => price * (1 + taxRate);

        public static decimal Calculate(decimal price, decimal taxRate)
        => price * (1 + taxRate);

        public static int Calculate(int price, int taxRate)
        => price * (1 + taxRate);

        public static float Calculate(float price, float taxRate)
        => price * (1 + taxRate);
    }
}
