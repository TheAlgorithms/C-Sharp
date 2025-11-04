using Algorithms.Other;
using NUnit.Framework;
using System;

namespace Algorithms.Tests.Other;

/// <summary>
///     Comprehensive test suite for Kadane's Algorithm implementation.
///     Tests cover various scenarios including:
///     - Arrays with all positive numbers
///     - Arrays with mixed positive and negative numbers
///     - Arrays with all negative numbers
///     - Edge cases (single element, empty array, null array)
///     - Index tracking functionality
///     - Long integer support for large numbers
/// </summary>
public static class KadanesAlgorithmTests
{
    [Test]
    public static void FindMaximumSubarraySum_WithPositiveNumbers_ReturnsCorrectSum()
    {
        // Arrange: When all numbers are positive, the entire array is the maximum subarray
        int[] array = { 1, 2, 3, 4, 5 };

        // Act
        int result = KadanesAlgorithm.FindMaximumSubarraySum(array);

        // Assert: Sum of all elements = 1 + 2 + 3 + 4 + 5 = 15
        Assert.That(result, Is.EqualTo(15));
    }

    [Test]
    public static void FindMaximumSubarraySum_WithMixedNumbers_ReturnsCorrectSum()
    {
        // Arrange: Classic example with mixed positive and negative numbers
        // The maximum subarray is [4, -1, 2, 1] starting at index 3
        int[] array = { -2, 1, -3, 4, -1, 2, 1, -5, 4 };

        // Act
        int result = KadanesAlgorithm.FindMaximumSubarraySum(array);

        // Assert: Maximum sum is 4 + (-1) + 2 + 1 = 6
        Assert.That(result, Is.EqualTo(6)); // Subarray [4, -1, 2, 1]
    }

    [Test]
    public static void FindMaximumSubarraySum_WithAllNegativeNumbers_ReturnsLargestNegative()
    {
        // Arrange: When all numbers are negative, the algorithm returns the least negative number
        // This represents a subarray of length 1 containing the largest (least negative) element
        int[] array = { -5, -2, -8, -1, -4 };

        // Act
        int result = KadanesAlgorithm.FindMaximumSubarraySum(array);

        // Assert: -1 is the largest (least negative) number in the array
        Assert.That(result, Is.EqualTo(-1));
    }

    [Test]
    public static void FindMaximumSubarraySum_WithSingleElement_ReturnsThatElement()
    {
        // Arrange: Edge case with only one element
        // The only possible subarray is the element itself
        int[] array = { 42 };

        // Act
        int result = KadanesAlgorithm.FindMaximumSubarraySum(array);

        // Assert: The single element is both the subarray and its sum
        Assert.That(result, Is.EqualTo(42));
    }

    [Test]
    public static void FindMaximumSubarraySum_WithNullArray_ThrowsArgumentException()
    {
        // Arrange: Test defensive programming - null input validation
        int[]? array = null;

        // Act & Assert: Should throw ArgumentException for null input
        Assert.Throws<ArgumentException>(() => KadanesAlgorithm.FindMaximumSubarraySum(array!));
    }

    [Test]
    public static void FindMaximumSubarraySum_WithEmptyArray_ThrowsArgumentException()
    {
        // Arrange
        int[] array = Array.Empty<int>();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => KadanesAlgorithm.FindMaximumSubarraySum(array));
    }

    [Test]
    public static void FindMaximumSubarraySum_WithAlternatingNumbers_ReturnsCorrectSum()
    {
        // Arrange: Alternating positive and negative numbers
        // Despite negative values, the entire array gives the maximum sum
        int[] array = { 5, -3, 5, -3, 5 };

        // Act
        int result = KadanesAlgorithm.FindMaximumSubarraySum(array);

        // Assert: Sum of entire array = 5 - 3 + 5 - 3 + 5 = 9
        Assert.That(result, Is.EqualTo(9)); // Entire array
    }

    [Test]
    public static void FindMaximumSubarrayWithIndices_ReturnsCorrectIndices()
    {
        // Arrange: Test the variant that returns indices of the maximum subarray
        // Array: [-2, 1, -3, 4, -1, 2, 1, -5, 4]
        // Index:   0  1   2  3   4  5  6   7  8
        int[] array = { -2, 1, -3, 4, -1, 2, 1, -5, 4 };

        // Act
        var (maxSum, startIndex, endIndex) = KadanesAlgorithm.FindMaximumSubarrayWithIndices(array);

        // Assert: Maximum subarray is [4, -1, 2, 1] from index 3 to 6
        Assert.That(maxSum, Is.EqualTo(6));
        Assert.That(startIndex, Is.EqualTo(3));
        Assert.That(endIndex, Is.EqualTo(6));
    }

    [Test]
    public static void FindMaximumSubarrayWithIndices_WithSingleElement_ReturnsZeroIndices()
    {
        // Arrange
        int[] array = { 10 };

        // Act
        var (maxSum, startIndex, endIndex) = KadanesAlgorithm.FindMaximumSubarrayWithIndices(array);

        // Assert
        Assert.That(maxSum, Is.EqualTo(10));
        Assert.That(startIndex, Is.EqualTo(0));
        Assert.That(endIndex, Is.EqualTo(0));
    }

    [Test]
    public static void FindMaximumSubarrayWithIndices_WithNullArray_ThrowsArgumentException()
    {
        // Arrange
        int[]? array = null;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => KadanesAlgorithm.FindMaximumSubarrayWithIndices(array!));
    }

    [Test]
    public static void FindMaximumSubarraySum_WithLongArray_ReturnsCorrectSum()
    {
        // Arrange: Test the long integer overload with same values as int test
        // Verifies that the algorithm works correctly with long data type
        long[] array = { -2L, 1L, -3L, 4L, -1L, 2L, 1L, -5L, 4L };

        // Act
        long result = KadanesAlgorithm.FindMaximumSubarraySum(array);

        // Assert: Should produce same result as int version
        Assert.That(result, Is.EqualTo(6L));
    }

    [Test]
    public static void FindMaximumSubarraySum_WithLargeLongNumbers_ReturnsCorrectSum()
    {
        // Arrange: Test with large numbers that would overflow int type
        // This demonstrates why the long overload is necessary
        // Sum would be 1,500,000,000 which fits in long but is near int.MaxValue
        long[] array = { 1000000000L, -500000000L, 1000000000L };

        // Act
        long result = KadanesAlgorithm.FindMaximumSubarraySum(array);

        // Assert: Entire array sum = 1,000,000,000 - 500,000,000 + 1,000,000,000 = 1,500,000,000
        Assert.That(result, Is.EqualTo(1500000000L));
    }

    [Test]
    public static void FindMaximumSubarraySum_WithLongNullArray_ThrowsArgumentException()
    {
        // Arrange
        long[]? array = null;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => KadanesAlgorithm.FindMaximumSubarraySum(array!));
    }

    [Test]
    public static void FindMaximumSubarraySum_WithZeros_ReturnsZero()
    {
        // Arrange: Edge case with all zeros
        // Any subarray will have sum of 0
        int[] array = { 0, 0, 0, 0 };

        // Act
        int result = KadanesAlgorithm.FindMaximumSubarraySum(array);

        // Assert: Maximum sum is 0
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public static void FindMaximumSubarraySum_WithMixedZerosAndNegatives_ReturnsZero()
    {
        // Arrange: Mix of zeros and negative numbers
        // The best subarray is any single zero (or multiple zeros)
        int[] array = { -5, 0, -3, 0, -2 };

        // Act
        int result = KadanesAlgorithm.FindMaximumSubarraySum(array);

        // Assert: Zero is better than any negative number
        Assert.That(result, Is.EqualTo(0));
    }
}
