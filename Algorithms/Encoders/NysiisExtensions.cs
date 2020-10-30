using System.Text;

namespace Algorithms.Encoders
{
    /// <summary>
    /// Static class containing Extension Methods used by the NYSIIS algorithm.
    /// </summary>
    public static class NysiisExtensions
    {
        /// <summary>
        /// Removes the last character of a StringBuilder.
        /// </summary>
        /// <param name="b">The StringBuilder to remove from.</param>
        public static void RemoveLast(this StringBuilder b) =>
            b.Remove(b.Length - 1, 1);

        /// <summary>
        /// Replaces a number of characters starting at a given index with another set of characters.
        /// </summary>
        /// <param name="torep">The string in which to replace.</param>
        /// <param name="index">The index where the substring to replace starts.</param>
        /// <param name="repl">The characters with which to replace the substring.</param>
        /// <param name="len">The length of the substring to replace.</param>
        /// <returns>The given string where the given substring was replaced by the given characters.</returns>
        public static string ReplaceAt(this string torep, int index, string repl, int len) =>
            $"{torep.Substring(0, index)}{repl}{(index + len < torep.Length ? torep.Substring(index + len) : string.Empty)}";

        /// <summary>
        /// Replaces one character at a given index with another character.
        /// </summary>
        /// <param name="torep">The string in which to replace.</param>
        /// <param name="index">The index of the character to replace.</param>
        /// <param name="rep">The character with which to replace.</param>
        /// <returns>The given string where the given character was replaced.</returns>
        public static string ReplaceSingle(this string torep, int index, char rep) =>
            torep.ReplaceAt(index, $"{rep}", 1);
    }
}
