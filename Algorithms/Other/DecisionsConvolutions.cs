using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Other;

/// <summary>
/// Almost all real complex decision-making task is described by more than one criterion.
/// There are different methods to select the best decisions from the defined set of decisions.
/// This class contains implementations of the popular convolution methods: linear and maxmin.
/// </summary>
public static class DecisionsConvolutions
{
    /// <summary>
    /// This method implements the linear method of decision selection. It is based on
    /// the calculation of the "value" for each decision and the selection of the most
    /// valuable one.
    /// </summary>
    /// <param name="matrix">Contains a collection of the criteria sets.</param>
    /// <param name="priorities">Contains a set of priorities for each criterion.</param>
    /// <returns>The most effective decision that is represented by a set of criterias.</returns>
    public static List<decimal> Linear(List<List<decimal>> matrix, List<decimal> priorities)
    {
        var decisionValues = new List<decimal>();

        foreach (var decision in matrix)
        {
            decimal sum = 0;
            for (int i = 0; i < decision.Count; i++)
            {
                sum += decision[i] * priorities[i];
            }

            decisionValues.Add(sum);
        }

        decimal bestDecisionValue = decisionValues.Max();
        int bestDecisionIndex = decisionValues.IndexOf(bestDecisionValue);

        return matrix[bestDecisionIndex];
    }

    /// <summary>
    /// This method implements maxmin method of the decision selection. It is based on
    /// the calculation of the least criteria value and comparison of decisions based
    /// on the calculated results.
    /// </summary>
    /// <param name="matrix">Contains a collection of the criteria sets.</param>
    /// <param name="priorities">Contains a set of priorities for each criterion.</param>
    /// <returns>The most effective decision that is represented by a set of criterias.</returns>
    public static List<decimal> MaxMin(List<List<decimal>> matrix, List<decimal> priorities)
    {
        var decisionValues = new List<decimal>();

        foreach (var decision in matrix)
        {
            decimal minValue = decimal.MaxValue;
            for (int i = 0; i < decision.Count; i++)
            {
                decimal result = decision[i] * priorities[i];
                if (result < minValue)
                {
                    minValue = result;
                }
            }

            decisionValues.Add(minValue);
        }

        decimal bestDecisionValue = decisionValues.Max();
        int bestDecisionIndex = decisionValues.IndexOf(bestDecisionValue);

        return matrix[bestDecisionIndex];
    }
}
