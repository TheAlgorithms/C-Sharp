using System;
using System.Collections.Generic;

namespace Algorithms.Strings.Similarity;

public class JaccardSimilarity
{
    public double Calculate(string left, string right)
    {
        ValidateInput(left, right);

        var leftLength = left.Length;
        var rightLength = right.Length;

        if (leftLength == 0 && rightLength == 0)
        {
            return 1.0d;
        }

        if (leftLength == 0 || rightLength == 0)
        {
            return 0.0d;
        }

        var leftSet = new HashSet<char>(left);
        var rightSet = new HashSet<char>(right);

        var unionSet = new HashSet<char>(leftSet);
        foreach (var c in rightSet)
        {
            unionSet.Add(c);
        }

        var intersectionSize = leftSet.Count + rightSet.Count - unionSet.Count;
        return 1.0d * intersectionSize / unionSet.Count;
    }

    private void ValidateInput(string left, string right)
    {
        if (left == null || right == null)
        {
            var paramName = left == null ? nameof(left) : nameof(right);
            throw new ArgumentNullException(paramName, "Input cannot be null");
        }
    }
}
