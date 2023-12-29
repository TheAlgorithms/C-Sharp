using System;

namespace Algorithms.Numeric;

/// <summary>
///     Modular exponentiation is a type of exponentiation performed over a modulus
///     Modular exponentiation c is: c = b^e mod m where b is base, e is exponent, m is modulus
///     (Wiki: https://en.wikipedia.org/wiki/Modular_exponentiation).
/// </summary>
public class ModularExponentiation
{
    /// <summary>
    ///     Performs Modular Exponentiation on b, e, m.
    /// </summary>
    /// <param name="b">Base.</param>
    /// <param name="e">Exponent.</param>
    /// <param name="m">Modulus.</param>
    /// <returns>Modular Exponential.</returns>
    public int ModularPow(int b, int e, int m)
    {
        // initialize result in variable res
        int res = 1;
        if (m == 1)
        {
            // 1 divides every number
            return 0;
        }

        if (m <= 0)
        {
            // exponential not defined in this case
            throw new ArgumentException(string.Format("{0} is not a positive integer", m));
        }

        for (int i = 0; i < e; i++)
        {
            res = (res * b) % m;
        }

        return res;
    }
}
