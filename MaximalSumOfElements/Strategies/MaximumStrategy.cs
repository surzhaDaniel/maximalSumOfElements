using System.Collections.Generic;
using System.Linq;
using MaximalSumOfElements.Interfaces;

namespace MaximalSumOfElements.Strategies;

/// <summary>
/// Strategy for finding maximum value.
/// </summary>
public class MaximumStrategy : ICalculationStrategy
{
    public double GetExtremeValue(IEnumerable<double> values)
    {
        return values.Max();
    }
} 