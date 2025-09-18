namespace Algorithms.Other;

/// <summary>
///     Luhn algorithm is a simple
///     checksum formula used to validate
///     a variety of identification numbers,
///     such as credit card numbers.
///     More information on the link:
///     https://en.wikipedia.org/wiki/Luhn_algorithm.
/// </summary>
public static class Luhn
{
    /// <summary>
    ///     Checking the validity of a sequence of numbers.
    /// </summary>
    /// <param name="number">The number that will be checked for validity.</param>
    /// <returns>
    ///     True: Number is valid.
    ///     False: Number isn`t valid.
    /// </returns>
    public static bool Validate(string number) => GetSum(number) % 10 == 0;

    /// <summary>
    ///     This algorithm finds one missing digit.
    ///     In place of the unknown digit, put "x".
    /// </summary>
    /// <param name="number">The number in which to find the missing digit.</param>
    /// <returns>Missing digit.</returns>
    public static int GetLostNum(string number)
    {
        var missingDigitIndex = number.Length - 1 - number.LastIndexOf("x", StringComparison.CurrentCultureIgnoreCase);
        var checkDigit = GetSum(number.Replace("x", "0", StringComparison.CurrentCultureIgnoreCase)) * 9 % 10;

        // Case 1: If the index of the missing digit is even.
        if (missingDigitIndex % 2 == 0)
        {
            return checkDigit;
        }

        var candidateDigit = checkDigit / 2;

        // Case 2: if the index of the missing digit is odd and the candidate is valid.
        // Case 3: if the index of the missing digit is odd and we need the alternative.
        return Validate(number.Replace("x", candidateDigit.ToString())) ? candidateDigit : (checkDigit + 9) / 2;
    }

    /// <summary>
    ///     Computes the sum found by the Luhn algorithm.
    /// </summary>
    /// <param name="number">The number for which the sum will be calculated.</param>
    /// <returns>Sum.</returns>
    private static int GetSum(string number)
    {
        var sum = 0;
        for (var i = 0; i < number.Length; i++)
        {
            var digit = number[i] - '0';
            digit = (i + number.Length) % 2 == 0
                ? 2 * digit
                : digit;
            if (digit > 9)
            {
                digit -= 9;
            }

            sum += digit;
        }

        return sum;
    }
}
