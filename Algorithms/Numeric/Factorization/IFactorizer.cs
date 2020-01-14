namespace Algorithms.Numeric.Factorization
{
    /// <summary>
    /// Finds a factor of a given number or returns false if it's prime.
    /// </summary>
    public interface IFactorizer
    {
        /// <summary>
        /// Finds a factor of a given number or returns false if it's prime.
        /// </summary>
        /// <param name="n">Integer to factor.</param>s
        /// <param name="factor">Found factor.</param>
        /// <returns><see langword="true"/> if factor is found, <see langword="false"/> if <paramref name="n"/> is prime.</returns>
        bool TryFactor(int n, out int factor);
    }
}
