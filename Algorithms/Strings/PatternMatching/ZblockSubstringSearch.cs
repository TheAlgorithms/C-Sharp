namespace Algorithms.Strings.PatternMatching;

/// <summary>Implementation Z-block substring search.
/// </summary>
public static class ZblockSubstringSearch
{
    /// <summary>
    ///     This algorithm finds all occurrences of a pattern in a text in linear time - O(m+n).
    /// </summary>
    public static int FindSubstring(string pattern, string text)
    {
        var concatStr = $"{pattern}${text}";
        var patternLength = pattern.Length;
        var n = concatStr.Length;
        var zArray = new int[n];

        var left = 0;
        var right = 0;

        for(var i = 1; i < n; i++)
        {
            if(i > right)
            {
                left = i;
                right = ComputeNewRightValue(concatStr, n, left, i);

                zArray[i] = right - left;
                right--;
            }
            else
            {
                var k = i - left;
                if (zArray[k] < (right - i + 1))
                {
                    zArray[i] = zArray[k];
                }
                else
                {
                    left = i;
                    right = ComputeNewRightValue(concatStr, n, left, right);
                    zArray[i] = right - left;
                    right--;
                }
            }
        }

        var found = 0;
        foreach(var z_value in zArray)
        {
            if(z_value == patternLength)
            {
                found++;
            }
        }

        return found;
    }

    private static int ComputeNewRightValue(string concatStr, int n, int left, int right)
    {
        while (right < n && concatStr[right - left].Equals(concatStr[right]))
        {
            right++;
        }

        return right;
    }
}
