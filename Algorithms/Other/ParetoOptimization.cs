using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Other;

/// <summary>
/// Almost all real complex decision-making task is described by more than one criterion.
/// Therefore, the methods of multicriteria optimization are important. For a wide range
/// of tasks multicriteria optimization, described by some axioms of "reasonable"
/// behavior in the process of choosing from a set of possible solutions X, each set of
/// selected solutions Sel X should be contained in a set optimal for Pareto.
/// </summary>
public class ParetoOptimization
{
    /// <summary>
    /// Performs decision optimizations by using Paretor's optimization algorithm.
    /// </summary>
    /// <param name="matrix">Contains a collection of the criterias sets.</param>
    /// <returns>An optimized collection of the criterias sets.</returns>
    public List<List<decimal>> Optimize(List<List<decimal>> matrix)
    {
        var optimizedMatrix = new List<List<decimal>>(matrix.Select(i => i));
        int i = 0;
        while (i < optimizedMatrix.Count)
        {
            for (int j = i + 1; j < optimizedMatrix.Count; j++)
            {
                decimal directParwiseDifference = GetMinimalPairwiseDifference(optimizedMatrix[i], optimizedMatrix[j]);
                decimal indirectParwiseDifference = GetMinimalPairwiseDifference(optimizedMatrix[j], optimizedMatrix[i]);
                /*
                 * in case all criteria of one set are larger that the criteria of another, this
                 * decision is not optimal and it has to be removed
                */
                if (directParwiseDifference >= 0 || indirectParwiseDifference >= 0)
                {
                    optimizedMatrix.RemoveAt(directParwiseDifference >= 0 ? j : i);
                    i--;
                    break;
                }
            }

            i++;
        }

        return optimizedMatrix;
    }

    /// <summary>
    /// Calculates the smallest difference between criteria of input decisions.
    /// </summary>
    /// <param name="arr1">Criterias of the first decision.</param>
    /// <param name="arr2">Criterias of the second decision.</param>
    /// <returns>Values that represent the smallest difference between criteria of input decisions.</returns>
    private decimal GetMinimalPairwiseDifference(List<decimal> arr1, List<decimal> arr2)
    {
        decimal min = decimal.MaxValue;
        if (arr1.Count == arr2.Count)
        {
            for (int i = 0; i < arr1.Count; i++)
            {
                decimal difference = arr1[i] - arr2[i];
                if (min > difference)
                {
                    min = difference;
                }
            }
        }

        return min;
    }
}
