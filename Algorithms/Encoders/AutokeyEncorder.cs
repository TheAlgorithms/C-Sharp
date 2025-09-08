namespace Algorithms.Encoders
{
    /// <summary>
    ///     Class for AutoKey encoding strings.
    /// </summary>
    public class AutokeyEncorder
    {
        /// <summary>
        ///     Autokey Cipher is a type of polyalphabetic cipher.
        ///     This works by choosing a key (a word or short phrase),
        ///     then you append the plaintext to itself to form a longer key.
        /// </summary>
        /// <param name="plainText">The string to be appended to the key.</param>
        /// <param name="keyword">The string to be appended to the plaintext.</param>
        /// <returns>The Autokey encoded string (All Uppercase).</returns>
        public string Encode(string plainText, string keyword)
        {
            plainText = Regex.Replace(plainText.ToUpper(CultureInfo.InvariantCulture), "[^A-Z]", string.Empty);
            keyword = keyword.ToUpper(CultureInfo.InvariantCulture);

            keyword += plainText;

            StringBuilder cipherText = new StringBuilder();

            for (int i = 0; i < plainText.Length; i++)
            {
                char plainCharacter = plainText[i];
                char keyCharacter = keyword[i];

                int encryptedCharacter = (plainCharacter - 'A' + keyCharacter - 'A') % 26 + 'A';
                cipherText.Append((char)encryptedCharacter);
            }

            return cipherText.ToString();
        }

        /// <summary>
        ///     Removed the key from the encoded string.
        /// </summary>
        /// <param name="cipherText">The encoded string.</param>
        /// <param name="keyword">The key to be removed from the encoded string.</param>
        /// <returns>The plaintext (All Uppercase).</returns>
        public string Decode(string cipherText, string keyword)
        {
            cipherText = Regex.Replace(cipherText.ToUpper(CultureInfo.InvariantCulture), "[^A-Z]", string.Empty);
            keyword = keyword.ToUpper(CultureInfo.InvariantCulture);

            StringBuilder plainText = new StringBuilder();
            StringBuilder extendedKeyword = new StringBuilder(keyword);

            for (int i = 0; i < cipherText.Length; i++)
            {
                char cipherCharacter = cipherText[i];
                char keywordCharacter = extendedKeyword[i];

                int decryptedCharacter = (cipherCharacter - 'A' - (keywordCharacter - 'A') + 26) % 26 + 'A';
                plainText.Append((char)decryptedCharacter);
                extendedKeyword.Append((char)decryptedCharacter);
            }

            return plainText.ToString();
        }
    }
}
