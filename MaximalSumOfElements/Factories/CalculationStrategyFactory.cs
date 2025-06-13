using MaximalSumOfElements.Enums;
using MaximalSumOfElements.Interfaces;
using MaximalSumOfElements.Strategies;

namespace MaximalSumOfElements.Factories;

/// <summary>
/// Factory for creating calculation strategies.
/// </summary>
public static class CalculationStrategyFactory
{
    public static ICalculationStrategy CreateStrategy(CalculationStrategyType type)
    {
        return type switch
        {
            CalculationStrategyType.Maximum => new MaximumStrategy(),
            CalculationStrategyType.Minimum => new MinimumStrategy(),
            _ => throw new ArgumentException($"Unknown strategy type: {type}")
        };
    }
} 