namespace Algorithms.Financial;

/// <summary>
/// PresentValue is the value of an expected income stream determined as of the date of valuation.
/// </summary>
public static class PresentValue
{
    public static double Calculate(double discountRate, List<double> cashFlows)
    {
        if (discountRate < 0)
        {
            throw new ArgumentException("Discount rate cannot be negative");
        }

        if (cashFlows.Count == 0)
        {
            throw new ArgumentException("Cash flows list cannot be empty");
        }

        double presentValue = cashFlows.Select((t, i) => t / Math.Pow(1 + discountRate, i)).Sum();

        return Math.Round(presentValue, 2);
    }
}
