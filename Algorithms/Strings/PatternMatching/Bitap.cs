using System;

namespace Algorithms.Strings.PatternMatching;

/// <summary>
/// The Bitap algorithm is a fuzzy string matching technique. It ains to find approximate matches of a pattern within a
/// text, allowing for a certain degree of mismatch (e.g., mistypes, minor variations etc.). It's knowd for its efficiency,
/// using bitwise operations for fast comparisons.
///
/// <para>
/// <b>How it works:</b>
/// <list type="number">
/// <item>
/// <term>Initialization</term>
/// <description>
/// Bitmasks are created for each character in the pattern. These bitmasks are essentially binary numbers where each bit
/// represents a specific character's position within the pattern. An initial state variable <c>R</c> is set to all 1s,
/// indicating that all characters in the pattern are initially unmatched.
/// </description>
/// </item>
/// <item>
/// <term>Iteration</term>
/// <description>
/// The algorithm iterates through each character in the text. For each character, the state <c>R</c> is updated using
/// bitwise operations (shifts and logical ORs). This update reflects whether the current character in the text matches
/// the corresponding character in the pattern.
/// </description>
/// </item>
/// <item>
/// <term>Matching</term>
/// <description>
/// After each iteration, the algorithm checks if the least significant bit of <c>R</c> is set to 1.
/// If it is, it means there's a potential match at that position, with a mismatch distance that's within the allowed
/// threshold.
/// </description>
/// </item>
/// </list>
/// </para>
/// <para>
/// <b> Finding Matches </b>
/// </para>
/// <para>
/// If the least significant bit of <c>R</c> is 1, it means a potential match is found.
/// The number of leading zeros in <c>R</c> indicates the mismatch distance.
/// If this distance is within the allowed threshold, it's considered a valid match.
/// </para>
/// </summary>
public static class Bitap
{
    /// <summary>
    /// <para>
    /// This function implements the Bitap algorithm for finding exact matches of a pattern within a text.
    /// It aims to find the first occurrence of the pattern in the text, allowing for no mismatches.
    /// </para>
    /// <para>
    /// The algorithm iterates through each character in the text. For each character, the state <c>R</c> is updated using
    /// bitwise operations (shifts and logical ORs). This update reflects whether the current character in the text matches
    /// the corresponding character in the pattern.
    /// </para>
    /// <para>
    /// After each iteration, the algorithm checks if the least significant bit of <c>R</c> is set to 1.
    /// If it is, it means there's a potential match at that position, with a mismatch distance of 0.
    /// The function returns the index of the first occurrence of the pattern in the text, or -1 if not found.
    /// </para>
    /// <para>
    /// The function throws an <see cref="ArgumentException"/> if the pattern is longer than 31 characters.
    /// This is because the maximum length of the pattern is 31, because if it's longer than that,
    /// we won't be able to represent the pattern mask in an int.
    /// </para>
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="pattern">The pattern to search for.</param>
    /// <returns>The index of the first occurrence of the pattern in the text, or -1 if not found.</returns>
    /// <exception cref="ArgumentException">The pattern is longer than 31 characters.</exception>
    public static int FindExactPattern(string text, string pattern)
    {
        // The length of the pattern.
        var len = pattern.Length;

        // An array of integers that will be used to mask the pattern.
        // The pattern mask is a bitmask that we will use to search for the pattern characters
        // in the text. We'll set the bit corresponding to the character in the pattern
        // to 0, and then use bitwise operations to check for the pattern.
        var patternMask = new int[128];
        int index;

        // Check if the pattern is empty.
        if (string.IsNullOrEmpty(pattern))
        {
            return 0;
        }

        // Check if the pattern is longer than 31 characters.
        if (len > 31)
        {
            throw new ArgumentException("The pattern is longer than 31 characters.");
        }

        // Initialize the register <c>R</c> to all 1s.
        var r = ~1;

        // Initialize the pattern mask to all 1s.
        for (index = 0; index <= 127; ++index)
        {
            patternMask[index] = ~0;
        }

        // Set the bits corresponding to the characters in the pattern to 0 in the pattern mask.
        for (index = 0; index < len; ++index)
        {
            patternMask[pattern[index]] &= ~(1 << index);
        }

        // Iterate through each character in the text.
        for (index = 0; index < text.Length; ++index)
        {
            // Update the state <c>R</c> by ORing the pattern mask with the character in the text,
            // and then shift it to the left by 1.
            r |= patternMask[text[index]];
            r <<= 1;

            // Check if the least significant bit of <c>R</c> is set to 1.
            // If there's a potential match at that position, with a mismatch distance of 0,
            // return the index of the first occurrence of the pattern in the text.
            if ((r & 1 << len) == 0)
            {
                return index - len + 1;
            }
        }

        // If no match is found, return -1.
        return -1;
    }

    /// <summary>
    /// Finds the first occurrence of a pattern in a given text with a given threshold for mismatches.
    /// </summary>
    /// <param name="text">The text to search in.</param>
    /// <param name="pattern">The pattern to search for.</param>
    /// <param name="threshold">The maximum number of mismatches allowed.</param>
    /// <returns>The index of the first occurrence of the pattern in the text, or -1 if not found.</returns>
    public static int FindFuzzyPattern(string text, string pattern, int threshold)
    {
        // Create a pattern mask for each character in the pattern.
        // The pattern mask is a bitmask that we will use to search for the pattern characters
        // in the text. We'll set the bit corresponding to the character in the pattern
        // to 0, and then use bitwise operations to check for the pattern.
        var patternMask = new int[128];

        // Create a register array.
        // The register array is used to keep track of the pattern mask as we search for the pattern.
        // We'll start with a register that has all bits set to 1, because all bits in the pattern mask
        // will be set to 1 initially.
        var r = new int[(threshold + 1) * sizeof(int)];

        var len = pattern.Length;

        // Check for empty strings.
        // If the text is empty, return 0.
        // If the pattern is empty, return 0.
        if (string.IsNullOrEmpty(text))
        {
            return 0;
        }

        if (string.IsNullOrEmpty(pattern))
        {
            return 0;
        }

        // Check for a pattern that is too long.
        // If the pattern is longer than 31 characters, return -1.
        // The maximum length of the pattern is 31, because if it's longer than that,
        // we won't be able to represent the pattern mask in an int.
        if (len > 31)
        {
            return -1;
        }

        // Initialize the register.
        // Set the least significant bit in the register to 0 or 1
        // depending on whether the current character in the text matches the pattern.
        // This will make it easier to check for the pattern later.
        for (var i = 0; i <= threshold; ++i)
        {
            r[i] = ~1;
        }

        // Initialize the pattern mask.
        // Set the bit corresponding to each character in the pattern to 0 in the pattern mask.
        // This will make it easier to check for the pattern later.
        for (var i = 0; i <= 127; i++)
        {
            patternMask[i] = ~0;
        }

        // Set the pattern mask for each character in the pattern.
        // Use bitwise AND to clear the bit corresponding to the current character.
        for (var i = 0; i < len; ++i)
        {
            patternMask[pattern[i]] &= ~(1 << i);
        }

        // Search for the pattern in the text.
        // Loop through each character in the text.
        for (var i = 0; i < text.Length; ++i)
        {
            // Update the register.
            // Set the least significant bit in the register to 0 or 1
            // depending on whether the current character in the text matches the pattern.
            // This will make it easier to check for the pattern later.
            var oldR = r[0];

            r[0] |= patternMask[text[i]];
            r[0] <<= 1;

            // Update the other registers.
            // Set the least significant bit in each register to 0 or 1
            // depending on whether the current character in the text matches the pattern.
            // This will make it easier to check for the pattern later.
            for (var j = 1; j <= threshold; ++j)
            {
                var tmp = r[j];

                r[j] = (oldR & (r[j] | patternMask[text[i]])) << 1;
                oldR = tmp;
            }

            // If the pattern has been found, return the index.
            // Check the most significant bit in the register.
            // If it's 0, then the pattern has been found.
            if ((r[threshold] & 1 << len) == 0)
            {
                // The pattern has been found.
                // Return the index of the first character in the pattern.
                return i - len + 1;
            }
        }

        // The pattern has not been found.
        return -1;
    }
}
