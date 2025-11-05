namespace Algorithms.Strings;

/// <summary>
///     Manacher's Algorithm is used to find the longest palindromic substring in linear time O(n).
///     This algorithm is significantly more efficient than the naive O(n²) approach.
///
///     KEY CONCEPTS:
///     1. String Transformation: Inserts special characters to handle even/odd palindromes uniformly.
///     2. Palindrome Radius: For each position, stores how far the palindrome extends.
///     3. Center Expansion: Expands around each potential center to find palindromes.
///     4. Symmetry Property: Uses previously computed palindrome information to skip redundant checks.
///     5. Right Boundary Tracking: Maintains the rightmost boundary of any palindrome found.
///
///     WHY IT'S FAST:
///     The algorithm achieves O(n) time by ensuring each character is examined at most twice:
///     - Once when it's inside a known palindrome (using mirror property).
///     - Once when expanding beyond the known boundary.
///
///     Reference: "A New Linear-Time On-Line Algorithm for Finding the Smallest Initial Palindrome of a String"
///     by Glenn Manacher (1975), Journal of the ACM.
/// </summary>
public static class ManachersAlgorithm
{
    /// <summary>
    ///     Finds the longest palindromic substring using Manacher's Algorithm.
    ///
    ///     ALGORITHM STEPS:
    ///     1. PREPROCESSING: Transform "abc" to "^#a#b#c#$" to handle even/odd palindromes uniformly.
    ///        - ^ and $ are sentinels that prevent boundary checks.
    ///        - # characters create uniform spacing.
    ///
    ///     2. INITIALIZATION: Set up tracking variables.
    ///        - palindromeRadii[i]: How far palindrome extends from position i.
    ///        - center: Center of rightmost palindrome found.
    ///        - rightBoundary: Right edge of rightmost palindrome.
    ///
    ///     3. MAIN LOOP: For each position i in transformed string.
    ///        a) If i is within rightBoundary, use mirror property:
    ///           - mirror = 2 * center - i (symmetric position).
    ///           - Start with min(palindromeRadii[mirror], rightBoundary - i).
    ///        b) Expand palindrome by comparing characters on both sides.
    ///        c) Update center and rightBoundary if palindrome extends further right.
    ///        d) Track the longest palindrome found.
    ///
    ///     4. EXTRACTION: Convert transformed coordinates back to original string.
    ///
    ///     Time Complexity: O(n) - Each character examined at most twice.
    ///     Space Complexity: O(n) - For transformed string and radius array.
    /// </summary>
    /// <param name="input">The input string to search for palindromes.</param>
    /// <returns>The longest palindromic substring found in the input.</returns>
    /// <exception cref="ArgumentException">Thrown when the input string is null.</exception>
    /// <example>
    ///     Input: "babad".
    ///     Output: "bab" or "aba" (both are valid longest palindromes with length 3).
    ///
    ///     Detailed Example:
    ///     Input: "abaxyz".
    ///     Transformed: "^#a#b#a#x#y#z#$".
    ///     Process finds "aba" at indices 1-3 with radius 3 in transformed string.
    ///     Maps back to indices 0-2 in original string.
    ///     Output: "aba".
    /// </example>
    public static string FindLongestPalindrome(string input)
    {
        // Validate input
        if (input == null)
        {
            throw new ArgumentException("Input string cannot be null.", nameof(input));
        }

        // Handle edge cases
        if (input.Length == 0)
        {
            return string.Empty;
        }

        if (input.Length == 1)
        {
            return input;
        }

        // STEP 1: Transform the string to handle even-length palindromes uniformly
        // Example: "abc" becomes "^#a#b#c#$"
        //
        // WHY THIS WORKS:
        // - Original "aba" (odd): Center is 'b' at index 1.
        // - Original "abba" (even): No single center character.
        // - Transformed "#a#b#b#a#": Both have clear centers (the middle #).
        //
        // SENTINELS (^ and $):
        // - Prevent index out of bounds during expansion.
        // - Never match any character, so expansion stops naturally.
        string transformed = PreprocessString(input);
        int n = transformed.Length;

        // STEP 2: Initialize data structures

        // palindromeRadii[i] = radius of palindrome centered at position i.
        // Example: If transformed[i] is center of "#a#b#a#", radius = 3.
        // (3 characters on each side match).
        int[] palindromeRadii = new int[n];

        // Track the rightmost palindrome boundary for optimization.
        // center: Position of the palindrome that extends furthest right.
        // rightBoundary: The rightmost index covered by that palindrome.
        // These help us use symmetry to avoid redundant comparisons.
        int center = 0;
        int rightBoundary = 0;

        // Track the longest palindrome found during the scan.
        // maxLength: Radius of the longest palindrome.
        // maxCenter: Position where the longest palindrome is centered.
        int maxLength = 0;
        int maxCenter = 0;

        // STEP 3: Process each position in the transformed string.
        // Skip first and last positions (sentinels ^ and $).
        for (int i = 1; i < n - 1; i++)
        {
            // OPTIMIZATION: Use mirror property for positions within known palindrome
            //
            // If we have a palindrome centered at 'center' extending to 'rightBoundary':
            //     center - radius ... center ... center + radius
            //                 mirror    i
            //
            // The mirror position reflects i across the center:
            // mirror = center - (i - center) = 2 * center - i.
            int mirror = 2 * center - i;

            // KEY INSIGHT: If i is within rightBoundary, we can use symmetry.
            // The palindrome at position i might be similar to the one at mirror position.
            if (i < rightBoundary)
            {
                // We can safely copy the radius from the mirror position, BUT:
                // 1. palindromeRadii[mirror]: What we know from the mirror side.
                // 2. rightBoundary - i: We can't assume anything beyond rightBoundary.
                //
                // Take the minimum because:
                // - If mirror's palindrome fits within bounds, we can use it.
                // - If it extends beyond, we only know up to rightBoundary.
                palindromeRadii[i] = Math.Min(rightBoundary - i, palindromeRadii[mirror]);
            }

            // EXPANSION PHASE: Try to extend the palindrome further.
            // We start from palindromeRadii[i] (not 0) to avoid redundant checks.
            // This is why the algorithm is O(n) - we never re-check characters.
            //
            // Example: If palindromeRadii[i] = 2, we already know:
            // transformed[i-2] == transformed[i+2] and
            // transformed[i-1] == transformed[i+1].
            // So we start checking at distance 3.
            //
            // The sentinels (^ and $) guarantee we never go out of bounds.
            // Expansion stops naturally when characters don't match.
            while (transformed[i + palindromeRadii[i] + 1] == transformed[i - palindromeRadii[i] - 1])
            {
                palindromeRadii[i]++;
            }

            // UPDATE TRACKING: If this palindrome extends further right than any before.
            //
            // WHY THIS MATTERS:
            // By tracking the rightmost boundary, we can use the mirror property
            // for future positions, avoiding redundant character comparisons.
            //
            // Example: If we find a large palindrome early, all positions within it
            // can benefit from the symmetry property.
            if (i + palindromeRadii[i] > rightBoundary)
            {
                center = i;  // This position is now our reference center.
                rightBoundary = i + palindromeRadii[i];  // Update the rightmost boundary.
            }

            // TRACK MAXIMUM: Remember the longest palindrome found so far.
            // We need both the length and position to extract it later.
            if (palindromeRadii[i] > maxLength)
            {
                maxLength = palindromeRadii[i];  // Radius of longest palindrome.
                maxCenter = i;  // Where it's centered in transformed string.
            }
        }

        // STEP 4: Extract the longest palindrome from the original string.
        //
        // COORDINATE MAPPING:
        // Transformed string has format: ^#a#b#c#$.
        // Position in transformed -> Position in original: (pos - 1) / 2.
        //
        // Example: "aba" -> "^#a#b#a#$".
        // - maxCenter = 4 (the middle 'b' in transformed).
        // - maxLength = 3 (radius).
        // - Original center = (4 - 1) / 2 = 1 (index of 'b' in "aba").
        // - Start = (maxCenter - maxLength) / 2 = (4 - 3) / 2 = 0.
        // - Length = maxLength = 3.
        // - Result: input.Substring(0, 3) = "aba".
        int start = (maxCenter - maxLength) / 2;
        return input.Substring(start, maxLength);
    }

    /// <summary>
    ///     Finds the longest palindromic substring and returns detailed information.
    ///     This method provides more detailed information than FindLongestPalindrome,
    ///     including the exact starting position and length in the original string.
    ///
    ///     USE CASE:
    ///     When you need to know WHERE the palindrome is located, not just what it is.
    ///     Useful for highlighting, replacing, or further processing the palindrome.
    /// </summary>
    /// <param name="input">The input string to search for palindromes.</param>
    /// <returns>
    ///     A tuple containing:
    ///     - The longest palindromic substring
    ///     - The starting index of the longest palindrome in the original string
    ///     - The length of the longest palindrome.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when the input string is null.</exception>
    /// <example>
    ///     Input: "babad".
    ///     Output: (Palindrome: "bab", StartIndex: 0, Length: 3).
    /// </example>
    public static (string Palindrome, int StartIndex, int Length) FindLongestPalindromeWithDetails(string input)
    {
        // Validate input
        if (input == null)
        {
            throw new ArgumentException("Input string cannot be null.", nameof(input));
        }

        // Handle edge cases
        if (input.Length == 0)
        {
            return (string.Empty, 0, 0);
        }

        if (input.Length == 1)
        {
            return (input, 0, 1);
        }

        // Apply the same algorithm as FindLongestPalindrome.
        // (See detailed comments in that method for step-by-step explanation).
        string transformed = PreprocessString(input);
        int n = transformed.Length;
        int[] palindromeRadii = new int[n];
        int center = 0;
        int rightBoundary = 0;
        int maxLength = 0;
        int maxCenter = 0;

        // Main algorithm loop
        for (int i = 1; i < n - 1; i++)
        {
            // Use mirror property if within known palindrome.
            int mirror = 2 * center - i;

            if (i < rightBoundary)
            {
                palindromeRadii[i] = Math.Min(rightBoundary - i, palindromeRadii[mirror]);
            }

            // Expand palindrome.
            // Sentinels guarantee no out-of-bounds access.
            while (transformed[i + palindromeRadii[i] + 1] == transformed[i - palindromeRadii[i] - 1])
            {
                palindromeRadii[i]++;
            }

            // Update rightmost boundary.
            if (i + palindromeRadii[i] > rightBoundary)
            {
                center = i;
                rightBoundary = i + palindromeRadii[i];
            }

            // Track maximum.
            if (palindromeRadii[i] > maxLength)
            {
                maxLength = palindromeRadii[i];
                maxCenter = i;
            }
        }

        // Calculate the start index and extract the palindrome.
        // Map from transformed coordinates to original string coordinates.
        int startIndex = (maxCenter - maxLength) / 2;
        string palindrome = input.Substring(startIndex, maxLength);

        // Return all three pieces of information.
        return (palindrome, startIndex, maxLength);
    }

    /// <summary>
    ///     Checks if the entire string is a palindrome using Manacher's Algorithm.
    ///
    ///     EFFICIENCY:
    ///     - This approach: O(n) time using Manacher's algorithm.
    ///     - Naive approach: O(n) time for reversing + O(n) for comparison.
    ///     - Both are O(n), but this avoids creating a reversed copy.
    ///
    ///     LOGIC:
    ///     If the longest palindrome in the string equals the string length,
    ///     then the entire string must be a palindrome.
    /// </summary>
    /// <param name="input">The string to check.</param>
    /// <returns>True if the entire string is a palindrome, false otherwise.</returns>
    /// <exception cref="ArgumentException">Thrown when the input string is null.</exception>
    /// <example>
    ///     Input: "racecar".
    ///     Output: true.
    /// </example>
    public static bool IsPalindrome(string input)
    {
        if (input == null)
        {
            throw new ArgumentException("Input string cannot be null.", nameof(input));
        }

        // Strings of length 0 or 1 are always palindromes.
        if (input.Length <= 1)
        {
            return true;
        }

        // Find the longest palindrome in the string.
        // If it spans the entire string, then the string is a palindrome.
        var (_, _, length) = FindLongestPalindromeWithDetails(input);
        return length == input.Length;
    }

    /// <summary>
    ///     Preprocesses the input string by inserting special characters.
    ///     This transformation is the KEY to making Manacher's algorithm work efficiently.
    ///
    ///     PROBLEM IT SOLVES:
    ///     - Odd-length palindromes ("aba") have a center character.
    ///     - Even-length palindromes ("abba") have a center between characters.
    ///     - Without transformation, we'd need separate logic for each case.
    ///
    ///     SOLUTION:
    ///     Insert '#' between every character, making all palindromes odd-length.
    ///
    ///     EXAMPLES:
    ///     - "aba" (odd) -> "^#a#b#a#$" (center is 'b').
    ///     - "abba" (even) -> "^#a#b#b#a#$" (center is '#' between the two 'b's).
    ///
    ///     SENTINELS (^ and $):
    ///     - Placed at start and end.
    ///     - Never match any character (including each other).
    ///     - Automatically stop expansion without explicit boundary checks.
    ///     - Prevent IndexOutOfBoundsException.
    ///
    ///     COORDINATE MAPPING:
    ///     - Original index i maps to transformed index (2*i + 2).
    ///     - Transformed index j maps to original index (j - 1) / 2.
    /// </summary>
    /// <param name="input">The original input string.</param>
    /// <returns>The transformed string with inserted special characters.</returns>
    private static string PreprocessString(string input)
    {
        // Calculate the size of transformed string.
        // Original: n characters.
        // Transformed: ^ + # + (n chars with # between each) + # + $.
        // Total: 1 + 1 + n + (n-1) + 1 + 1 = 2n + 3.
        int n = input.Length;
        char[] transformed = new char[n * 2 + 3];

        // Place sentinels at boundaries.
        transformed[0] = '^'; // Start sentinel (never matches anything).
        transformed[n * 2 + 2] = '$'; // End sentinel (never matches anything).

        // Build the transformed string: #a#b#c#.
        // For input "abc":
        // Position 0: ^ (sentinel).
        // Position 1: # (separator).
        // Position 2: a (input[0]).
        // Position 3: # (separator).
        // Position 4: b (input[1]).
        // Position 5: # (separator).
        // Position 6: c (input[2]).
        // Position 7: # (separator).
        // Position 8: $ (sentinel).
        for (int i = 0; i < n; i++)
        {
            transformed[2 * i + 1] = '#';  // Separator before character.
            transformed[2 * i + 2] = input[i];  // Original character.
        }

        transformed[n * 2 + 1] = '#';  // Final separator.

        return new string(transformed);
    }
}
