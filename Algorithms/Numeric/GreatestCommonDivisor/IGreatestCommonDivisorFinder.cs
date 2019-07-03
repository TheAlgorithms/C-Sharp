namespace Algorithms.Numeric.GreatestCommonDivisor
{
    public interface IGreatestCommonDivisorFinder
    {
        /// <summary>
        /// Finds greatest common divisor for numbers a and b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>Greatest common divisor</returns>
        int Find(int a, int b);
    }
}
