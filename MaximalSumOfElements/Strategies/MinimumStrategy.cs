using System.Collections.Generic;
using System.Linq;
using MaximalSumOfElements.Interfaces;

namespace MaximalSumOfElements.Strategies;

/// <summary>
/// Strategy for finding minimum value.
/// </summary>
public class MinimumStrategy : ICalculationStrategy
{
    public double GetExtremeValue(IEnumerable<double> values)
    {
        return values.Min();
    }
} 