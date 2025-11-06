namespace Algorithms.Other;

/// <summary>
/// Boyer-Moore Majority Vote algorithm.
/// Finds element appearing more than n/2 times in O(n) time, O(1) space.
/// </summary>
public static class BoyerMooreMajorityVote
{
    /// <summary>
    /// Finds the majority element.
    /// </summary>
    /// <param name="nums">Input array.</param>
    /// <returns>Majority element or null.</returns>
    public static int? FindMajority(int[] nums)
    {
        if (nums?.Length == 0) return null;

        var candidate = FindCandidate(nums!);
        return IsMajority(nums!, candidate) ? candidate : null;
    }

    private static int FindCandidate(int[] nums)
    {
        int candidate = nums[0], count = 1;

        for (int i = 1; i < nums.Length; i++)
        {
            if (count == 0) candidate = nums[i];
            count += nums[i] == candidate ? 1 : -1;
        }

        return candidate;
    }

    private static bool IsMajority(int[] nums, int candidate) =>
        nums.Count(n => n == candidate) > nums.Length / 2;
}
