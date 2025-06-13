using System.Collections.Generic;

namespace MaximalSumOfElements.Interfaces;

/// <summary>
/// Interface defining the strategy for calculating extreme values.
/// </summary>
public interface ICalculationStrategy
{
    double GetExtremeValue(IEnumerable<double> values);
} 