using System;
using System.Runtime.CompilerServices;
using Algorithms.Crypto.Utils;

namespace Algorithms.Crypto.Digests;

/// <summary>
/// Implements the Ascon cryptographic hash algorithm, providing both the standard Ascon-Hash and the Ascon-HashA variants.
/// </summary>
/// <remarks>
/// The <see cref="AsconDigest"/> class implements the Ascon hash function, a lightweight cryptographic algorithm designed for
/// resource-constrained environments such as IoT devices. It provides two variants:
/// <list type="bullet">
/// <item>
/// <description>
/// <see cref="AsconParameters.AsconHash"/>: The standard Ascon-Hash variant with 12 rounds of the permutation function for enhanced security.
/// </description>
/// </item>
/// <item>
/// <description>
/// <see cref="AsconParameters.AsconHashA"/>: A performance-optimized variant with 8 rounds of the permutation function, offering a trade-off between security and performance.
/// </description>
/// </item>
/// </list>
/// <br />
/// The AsconDigest processes data in 8-byte blocks, accumulating input until a block is complete, at which point it applies
/// the permutation function to update the internal state. After all data has been processed, the hash value can be finalized
/// and retrieved.
/// <br />
/// Ascon was designed to meet the requirements of lightweight cryptography, making it ideal for devices with limited computational power.
/// </remarks>
public class AsconDigest : IDigest
{
    public enum AsconParameters
    {
        /// <summary>
        /// Represents the Ascon Hash variant, the standard cryptographic hashing function of the Ascon family.
        /// </summary>
        /// <remarks>
        /// AsconHash is the primary hashing algorithm in the Ascon family. It is designed for efficiency and security
        /// in resource-constrained environments, such as IoT devices, and provides high resistance to cryptanalytic attacks.
        /// This variant uses 12 rounds of the permutation function for increased security.
        /// </remarks>
        AsconHash,

        /// <summary>
        /// Represents the Ascon HashA variant, an alternative variant of the Ascon hashing function with fewer permutation rounds.
        /// </summary>
        /// <remarks>
        /// AsconHashA is a variant of the Ascon hashing function that uses fewer rounds (8 rounds) of the permutation function,
        /// trading off some security for improved performance in specific scenarios. It is still designed to be secure for many
        /// applications, but it operates faster in environments where computational resources are limited.
        /// </remarks>
        AsconHashA,
    }

    /// <summary>
    /// Specifies the Ascon variant being used (either Ascon-Hash or Ascon-HashA). This defines the cryptographic algorithm's behavior.
    /// </summary>
    private readonly AsconParameters asconParameters;

    /// <summary>
    /// The number of permutation rounds applied in the Ascon cryptographic process. This is determined by the selected Ascon variant.
    /// </summary>
    private readonly int asconPbRounds;

    /// <summary>
    /// Internal buffer that temporarily stores input data before it is processed in 8-byte blocks. The buffer is cleared after each block is processed.
    /// </summary>
    private readonly byte[] buffer = new byte[8];

    /// <summary>
    /// Internal state variable <c>x0</c> used in the cryptographic permutation function. This is updated continuously as input data is processed.
    /// </summary>
    private ulong x0;

    /// <summary>
    /// Internal state variable <c>x1</c> used in the cryptographic permutation function. This, along with other state variables, is updated during each round.
    /// </summary>
    private ulong x1;

    /// <summary>
    /// Internal state variable <c>x2</c> used in the cryptographic permutation function. It helps track the evolving state of the digest.
    /// </summary>
    private ulong x2;

    /// <summary>
    /// Internal state variable <c>x3</c> used in the cryptographic permutation function, contributing to the mixing and non-linearity of the state.
    /// </summary>
    private ulong x3;

    /// <summary>
    /// Internal state variable <c>x4</c> used in the cryptographic permutation function. This, along with <c>x0</c> to <c>x3</c>, ensures cryptographic security.
    /// </summary>
    private ulong x4;

    /// <summary>
    /// Tracks the current position within the <c>buffer</c> array. When <c>bufferPosition</c> reaches 8, the buffer is processed and reset.
    /// </summary>
    private int bufferPosition;

    /// <summary>
    /// Initializes a new instance of the <see cref="AsconDigest"/> class with the specified Ascon parameters.
    /// </summary>
    /// <param name="parameters">The Ascon variant to use, either <see cref="AsconParameters.AsconHash"/> or <see cref="AsconParameters.AsconHashA"/>.</param>
    /// <remarks>
    /// This constructor sets up the digest by selecting the appropriate number of permutation rounds based on the Ascon variant.
    /// <list type="bullet">
    /// <item><description>For <see cref="AsconParameters.AsconHash"/>, 12 permutation rounds are used.</description></item>
    /// <item><description>For <see cref="AsconParameters.AsconHashA"/>, 8 permutation rounds are used.</description></item>
    /// </list>
    /// If an unsupported parameter is provided, the constructor throws an <see cref="ArgumentException"/> to indicate that the parameter is invalid.
    /// The internal state of the digest is then reset to prepare for processing input data.
    /// </remarks>
    /// <exception cref="ArgumentException">Thrown when an invalid parameter setting is provided for Ascon Hash.</exception>
    public AsconDigest(AsconParameters parameters)
    {
        // Set the Ascon parameter (AsconHash or AsconHashA) for this instance.
        asconParameters = parameters;

        // Determine the number of permutation rounds based on the Ascon variant.
        asconPbRounds = parameters switch
        {
            AsconParameters.AsconHash => 12,  // 12 rounds for Ascon-Hash variant.
            AsconParameters.AsconHashA => 8,  // 8 rounds for Ascon-HashA variant.
            _ => throw new ArgumentException("Invalid parameter settings for Ascon Hash"), // Throw exception for invalid parameter.
        };

        // Reset the internal state to prepare for new input.
        Reset();
    }

    /// <summary>
    /// Gets the name of the cryptographic algorithm based on the selected Ascon parameter.
    /// </summary>
    /// <value>
    /// A string representing the name of the algorithm variant, either "Ascon-Hash" or "Ascon-HashA".
    /// </value>
    /// <remarks>
    /// This property determines the algorithm name based on the selected Ascon variant when the instance was initialized.
    /// It supports two variants:
    /// <list type="bullet">
    /// <item><description>"Ascon-Hash" for the <see cref="AsconParameters.AsconHash"/> variant.</description></item>
    /// <item><description>"Ascon-HashA" for the <see cref="AsconParameters.AsconHashA"/> variant.</description></item>
    /// </list>
    /// If an unsupported or unknown parameter is used, the property throws an <see cref="InvalidOperationException"/>.
    /// </remarks>
    /// <exception cref="InvalidOperationException">Thrown if an unknown Ascon parameter is encountered.</exception>
    public string AlgorithmName
    {
        get
        {
            return asconParameters switch
            {
                AsconParameters.AsconHash => "Ascon-Hash",  // Return "Ascon-Hash" for AsconHash variant.
                AsconParameters.AsconHashA => "Ascon-HashA", // Return "Ascon-HashA" for AsconHashA variant.
                _ => throw new InvalidOperationException(), // Throw an exception for unknown Ascon parameters.
            };
        }
    }

    /// <summary>
    /// Gets the size of the resulting hash produced by the digest, in bytes.
    /// </summary>
    /// <returns>The size of the hash, which is 32 bytes (256 bits) for this digest implementation.</returns>
    /// <remarks>
    /// This method returns the fixed size of the hash output produced by the digest algorithm. In this implementation,
    /// the digest produces a 256-bit hash, which corresponds to 32 bytes. This is typical for cryptographic hash functions
    /// that aim to provide a high level of security by generating a large output size.
    /// </remarks>
    public int GetDigestSize() => 32;

    /// <summary>
    /// Gets the internal block size of the digest in bytes.
    /// </summary>
    /// <returns>The internal block size of the digest, which is 8 bytes (64 bits).</returns>
    /// <remarks>
    /// This method returns the block size that the digest algorithm uses when processing input data. The input is processed
    /// in chunks (blocks) of 8 bytes at a time. This block size determines how the input data is split and processed in multiple
    /// steps before producing the final hash.
    /// </remarks>
    public int GetByteLength() => 8;

    /// <summary>
    /// Updates the cryptographic state by processing a single byte of input and adding it to the internal buffer.
    /// </summary>
    /// <param name="input">The byte to be added to the internal buffer and processed.</param>
    /// <remarks>
    /// This method collects input bytes in an internal buffer. Once the buffer is filled (reaching 8 bytes), the buffer is processed
    /// by converting it into a 64-bit unsigned integer in big-endian format and XORing it with the internal state variable <c>x0</c>.
    /// After processing the buffer, the permutation function <see cref="P(int)"/> is applied to mix the internal state, and the buffer position is reset to zero.
    /// <br/><br/>
    /// If the buffer has not yet reached 8 bytes, the method simply adds the input byte to the buffer and waits for further input.
    /// </remarks>
    public void Update(byte input)
    {
        // Add the input byte to the buffer.
        buffer[bufferPosition] = input;

        // If the buffer is not full (less than 8 bytes), increment the buffer position and return early.
        if (++bufferPosition != 8)
        {
            return; // Wait for more input to fill the buffer before processing.
        }

        // Once the buffer is full (8 bytes), convert the buffer to a 64-bit integer (big-endian) and XOR it with the state.
        x0 ^= ByteEncodingUtils.BigEndianToUint64(buffer, 0);

        // Apply the permutation function to mix the state.
        P(asconPbRounds);

        // Reset the buffer position for the next block of input.
        bufferPosition = 0;
    }

    /// <summary>
    /// Updates the cryptographic state by processing a segment of input data from a byte array, starting at a specified offset and length.
    /// </summary>
    /// <param name="input">The byte array containing the input data to be processed.</param>
    /// <param name="inOff">The offset in the input array where processing should begin.</param>
    /// <param name="inLen">The number of bytes from the input array to process.</param>
    /// <remarks>
    /// This method ensures that the input data is valid by checking the array length, starting from the provided offset,
    /// and making sure it is long enough to accommodate the specified length. It then processes the data by converting
    /// the relevant section of the byte array to a <see cref="ReadOnlySpan{T}"/> and delegating the actual block update to
    /// the <see cref="BlockUpdate(ReadOnlySpan{byte})"/> method for further processing.
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// Thrown if the input data is too short, starting from <paramref name="inOff"/> and for the length <paramref name="inLen"/>.
    /// </exception>
    public void BlockUpdate(byte[] input, int inOff, int inLen)
    {
        // Validate the input data to ensure there is enough data to process from the specified offset and length.
        ValidationUtils.CheckDataLength(input, inOff, inLen, "input buffer too short");

        // Convert the input byte array into a ReadOnlySpan<byte> and delegate the processing to the span-based method.
        BlockUpdate(input.AsSpan(inOff, inLen));
    }

    /// <summary>
    /// Processes the input data by updating the internal cryptographic state, handling both partial and full blocks.
    /// </summary>
    /// <param name="input">A read-only span of bytes representing the input data to be processed.</param>
    /// <remarks>
    /// This method processes the input data in chunks of 8 bytes. It manages the internal buffer to accumulate data
    /// until there are enough bytes to process a full 8-byte block. When the buffer is full or enough input is provided,
    /// it XORs the buffered data with the internal state variable <c>x0</c> and applies the permutation function
    /// <see cref="P"/> to update the cryptographic state.
    /// <br/><br/>
    /// If the input contains more than 8 bytes, the method continues to process full 8-byte blocks in a loop until
    /// the input is exhausted. Any remaining bytes (less than 8) are stored in the internal buffer for future processing.
    /// </remarks>
    public void BlockUpdate(ReadOnlySpan<byte> input)
    {
        // Calculate the number of available bytes left in the buffer before it reaches 8 bytes.
        var available = 8 - bufferPosition;

        // If the input length is smaller than the remaining space in the buffer, copy the input into the buffer.
        if (input.Length < available)
        {
            input.CopyTo(buffer.AsSpan(bufferPosition)); // Copy the small input into the buffer.
            bufferPosition += input.Length; // Update the buffer position.
            return; // Return early since we don't have enough data to process a full block.
        }

        // If there is data in the buffer, but it isn't full, fill it and process the full 8-byte block.
        if (bufferPosition > 0)
        {
            // Copy enough bytes from the input to complete the buffer.
            input[..available].CopyTo(buffer.AsSpan(bufferPosition));

            // XOR the full buffer with the internal state (x0) and apply the permutation.
            x0 ^= ByteEncodingUtils.BigEndianToUint64(buffer);
            P(asconPbRounds); // Apply the permutation rounds.

            // Update the input to exclude the bytes we've already processed from the buffer.
            input = input[available..];
        }

        // Process full 8-byte blocks directly from the input.
        while (input.Length >= 8)
        {
            // XOR the next 8-byte block from the input with the internal state and apply the permutation.
            x0 ^= ByteEncodingUtils.BigEndianToUint64(input);
            P(asconPbRounds);

            // Move to the next 8-byte chunk in the input.
            input = input[8..];
        }

        // Copy any remaining bytes (less than 8) into the buffer to store for future processing.
        input.CopyTo(buffer);
        bufferPosition = input.Length; // Update the buffer position to reflect the remaining unprocessed data.
    }

    /// <summary>
    /// Finalizes the cryptographic hash computation, absorbing any remaining data, applying the final permutation,
    /// and writing the resulting hash to the specified position in the provided output byte array.
    /// </summary>
    /// <param name="output">The byte array where the final 32-byte hash will be written.</param>
    /// <param name="outOff">The offset in the output array at which to start writing the hash.</param>
    /// <returns>The size of the hash (32 bytes).</returns>
    /// <remarks>
    /// This method finalizes the hash computation by converting the output array to a <see cref="Span{T}"/> and
    /// calling the <see cref="DoFinal(Span{byte})"/> method. It provides flexibility in placing the result in an
    /// existing byte array with a specified offset.
    /// </remarks>
    /// <exception cref="ArgumentException">Thrown if the output buffer is too small to hold the resulting hash.</exception>
    public int DoFinal(byte[] output, int outOff)
    {
        // Call the Span-based DoFinal method with the output byte array and offset.
        return DoFinal(output.AsSpan(outOff));
    }

    /// <summary>
    /// Finalizes the cryptographic hash computation, absorbing any remaining data, applying the final permutation, and
    /// writing the resulting hash to the provided output buffer.
    /// </summary>
    /// <param name="output">A span of bytes where the final 32-byte hash will be written.</param>
    /// <returns>The size of the hash (32 bytes).</returns>
    /// <remarks>
    /// This method completes the hash computation by absorbing any remaining input data, applying the final permutation,
    /// and extracting the state variables to produce the final hash. The method processes the state in 8-byte chunks,
    /// writing the result into the output buffer in big-endian format. After the final permutation is applied, the internal
    /// state is reset to prepare for a new hashing session.
    /// </remarks>
    /// <exception cref="ArgumentException">Thrown if the output buffer is too small to hold the resulting hash.</exception>
    public int DoFinal(Span<byte> output)
    {
        // Validate that the output buffer is at least 32 bytes in length.
        ValidationUtils.CheckOutputLength(output, 32, "output buffer too short");

        // Absorb any remaining input and apply the final permutation.
        AbsorbAndFinish();

        // Convert the first part of the state (x0) to big-endian format and write it to the output.
        ByteEncodingUtils.UInt64ToBigEndian(x0, output);

        // Loop to process the remaining parts of the internal state (x1, x2, etc.).
        for (var i = 0; i < 3; ++i)
        {
            // Move to the next 8-byte segment in the output buffer.
            output = output[8..];

            // Apply the permutation rounds to mix the state.
            P(asconPbRounds);

            // Convert the updated state variable (x0) to big-endian format and write it to the output.
            ByteEncodingUtils.UInt64ToBigEndian(x0, output);
        }

        // Reset the internal state for the next hash computation.
        Reset();

        // Return the size of the hash (32 bytes).
        return 32;
    }

    /// <summary>
    /// Computes the cryptographic hash of the input byte array and returns the result as a lowercase hexadecimal string.
    /// </summary>
    /// <param name="input">The input byte array to be hashed.</param>
    /// <returns>A string containing the computed hash in lowercase hexadecimal format.</returns>
    /// <remarks>
    /// This method takes a byte array as input, processes it to compute the Ascon hash, and returns the result as a hexadecimal string.
    /// It internally converts the byte array to a <see cref="Span{T}"/> and delegates the actual hashing to the
    /// <see cref="Digest(Span{byte})"/> method.
    /// </remarks>
    public string Digest(byte[] input)
    {
        return Digest(input.AsSpan());
    }

    /// <summary>
    /// Computes the cryptographic hash of the input span of bytes and returns the result as a lowercase hexadecimal string.
    /// </summary>
    /// <param name="input">A span of bytes representing the input data to be hashed.</param>
    /// <returns>A string containing the computed hash in lowercase hexadecimal format.</returns>
    /// <remarks>
    /// This method processes the input span using the Ascon cryptographic algorithm to compute the hash. It accumulates
    /// the input, applies the necessary permutations and internal state updates, and finally produces a hash in the form
    /// of a 32-byte array. The result is then converted into a lowercase hexadecimal string using <see cref="BitConverter"/>.
    /// </remarks>
    public string Digest(Span<byte> input)
    {
        // Update the internal state with the input data.
        BlockUpdate(input);

        // Create an array to hold the final hash output (32 bytes).
        var output = new byte[GetDigestSize()];

        // Finalize the hash computation and store the result in the output array.
        DoFinal(output, 0);

        // Convert the hash (byte array) to a lowercase hexadecimal string.
        return BitConverter.ToString(output).Replace("-", string.Empty).ToLowerInvariant();
    }

    /// <summary>
    /// Resets the internal state of the Ascon cryptographic hash algorithm to its initial state based on the selected variant.
    /// </summary>
    /// <remarks>
    /// This method clears the internal buffer and resets the buffer position to zero. Depending on the specified
    /// Ascon variant (<see cref="AsconParameters.AsconHash"/> or <see cref="AsconParameters.AsconHashA"/>), it also reinitializes
    /// the internal state variables (<c>x0</c>, <c>x1</c>, <c>x2</c>, <c>x3</c>, <c>x4</c>) to their starting values.
    /// <br/><br/>
    /// The reset is necessary to prepare the hash function for a new message. It ensures that previous messages do not
    /// affect the new one and that the internal state is consistent with the algorithm’s specification for the selected variant.
    /// </remarks>
    public void Reset()
    {
        // Clear the buffer to remove any leftover data from previous operations.
        Array.Clear(buffer, 0, buffer.Length);

        // Reset the buffer position to zero to start processing fresh input.
        bufferPosition = 0;

        // Initialize the internal state variables (x0, x1, x2, x3, x4) based on the selected Ascon variant.
        switch (asconParameters)
        {
            // If using the AsconHashA variant, set the specific initial state values for x0 through x4.
            case AsconParameters.AsconHashA:
                x0 = 92044056785660070UL;
                x1 = 8326807761760157607UL;
                x2 = 3371194088139667532UL;
                x3 = 15489749720654559101UL;
                x4 = 11618234402860862855UL;
                break;

            // If using the AsconHash variant, set the specific initial state values for x0 through x4.
            case AsconParameters.AsconHash:
                x0 = 17191252062196199485UL;
                x1 = 10066134719181819906UL;
                x2 = 13009371945472744034UL;
                x3 = 4834782570098516968UL;
                x4 = 3787428097924915520UL;
                break;

            // If an unknown Ascon variant is encountered, throw an exception.
            default:
                throw new InvalidOperationException();
        }
    }

    /// <summary>
    /// Finalizes the absorption phase of the cryptographic hash by padding the buffer and applying the final permutation round.
    /// </summary>
    /// <remarks>
    /// This method is called when the input data has been fully absorbed into the internal state, and it needs to be finalized.
    /// The buffer is padded with a specific value (0x80) to signify the end of the data, and the remaining portion of the buffer is
    /// XORed with the internal state variable <c>x0</c>. After padding, the final permutation round is applied using 12 rounds of
    /// the permutation function <see cref="P(int)"/>. This ensures the internal state is fully mixed and the cryptographic hash
    /// is securely finalized.
    /// </remarks>
    private void AbsorbAndFinish()
    {
        // Pad the buffer with 0x80 to indicate the end of the data.
        buffer[bufferPosition] = 0x80;

        // XOR the buffer (after padding) with the internal state x0, but only the relevant portion of the buffer is considered.
        // The (56 - (bufferPosition << 3)) shifts ensure that only the unprocessed part of the buffer is XORed into x0.
        x0 ^= ByteEncodingUtils.BigEndianToUint64(buffer, 0) & (ulong.MaxValue << (56 - (bufferPosition << 3)));

        // Apply 12 rounds of the permutation function to fully mix and finalize the internal state.
        P(12);
    }

    /// <summary>
    /// Executes the cryptographic permutation function by applying a sequence of rounds that transform the internal state variables.
    /// </summary>
    /// <param name="numberOfRounds">
    /// The number of rounds to execute. If set to 12, additional rounds are performed with specific constants to enhance the security of the transformation.
    /// </param>
    /// <remarks>
    /// In the Ascon cryptographic algorithm, the permutation function <c>P</c> transforms the internal state over multiple rounds.
    /// This method applies a set of round constants, each of which alters the state variables (<c>x0</c>, <c>x1</c>, <c>x2</c>, <c>x3</c>, <c>x4</c>) differently,
    /// ensuring that the transformation introduces non-linearity and diffusion, which are essential for cryptographic security.
    /// <br/><br/>
    /// When <paramref name="numberOfRounds"/> is set to 12, the method first applies four unique round constants.
    /// Afterward, it applies a fixed set of six additional constants regardless of the number of rounds.
    /// </remarks>
    private void P(int numberOfRounds)
    {
        if (numberOfRounds == 12)
        {
            Round(0xf0UL);
            Round(0xe1UL);
            Round(0xd2UL);
            Round(0xc3UL);
        }

        Round(0xb4UL);
        Round(0xa5UL);

        Round(0x96UL);
        Round(0x87UL);
        Round(0x78UL);
        Round(0x69UL);
        Round(0x5aUL);
        Round(0x4bUL);
    }

    /// <summary>
    /// Executes a single round of the cryptographic permutation function, transforming the internal state
    /// variables <c>x0</c>, <c>x1</c>, <c>x2</c>, <c>x3</c>, and <c>x4</c> using XOR, AND, and NOT operations, along with circular bit rotations.
    /// This function is designed to introduce diffusion and non-linearity into the state for cryptographic security.
    /// </summary>
    /// <param name="circles">
    /// A 64-bit unsigned integer constant that influences the round's transformation. Each round uses a unique value of this constant
    /// to ensure that the transformation applied to the state differs for each round.
    /// </param>
    /// <remarks>
    /// The <c>Round</c> function uses a series of bitwise operations (XOR, AND, NOT) and circular bit rotations to mix
    /// the internal state. Each transformation step introduces non-linearity and ensures that small changes in the input or state
    /// variables propagate widely across the internal state, enhancing the security of the cryptographic process.
    /// <br/><br/>
    /// The round constant (<paramref name="circles"/>) plays a crucial role in altering the state at each round, ensuring
    /// that each round contributes uniquely to the overall cryptographic transformation. Circular rotations are applied using
    /// <see cref="LongUtils.RotateRight(ulong, int)"/> to spread bits throughout the 64-bit word.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Round(ulong circles)
    {
        // Step 1: Perform XOR and AND operations to mix inputs and state variables
        var t0 = x0 ^ x1 ^ x2 ^ x3 ^ circles ^ (x1 & (x0 ^ x2 ^ x4 ^ circles));
        var t1 = x0 ^ x2 ^ x3 ^ x4 ^ circles ^ ((x1 ^ x2 ^ circles) & (x1 ^ x3));
        var t2 = x1 ^ x2 ^ x4 ^ circles ^ (x3 & x4);
        var t3 = x0 ^ x1 ^ x2 ^ circles ^ (~x0 & (x3 ^ x4));
        var t4 = x1 ^ x3 ^ x4 ^ ((x0 ^ x4) & x1);

        // Step 2: Apply circular right shifts and update the internal state variables
        x0 = t0 ^ LongUtils.RotateRight(t0, 19) ^ LongUtils.RotateRight(t0, 28);
        x1 = t1 ^ LongUtils.RotateRight(t1, 39) ^ LongUtils.RotateRight(t1, 61);
        x2 = ~(t2 ^ LongUtils.RotateRight(t2, 1) ^ LongUtils.RotateRight(t2, 6));
        x3 = t3 ^ LongUtils.RotateRight(t3, 10) ^ LongUtils.RotateRight(t3, 17);
        x4 = t4 ^ LongUtils.RotateRight(t4, 7) ^ LongUtils.RotateRight(t4, 41);
    }
}
