# Maximal Sum of Elements - Learning Project

## Overview
This project is designed as a learning exercise to demonstrate fundamental C# programming concepts, clean code practices, and software design patterns. It provides a practical example of how to build a well-structured console application that processes and analyzes numerical data from files.

## Learning Objectives
By creating this project, I learnt about:

- **Clean Code Principles**
  - Single Responsibility Principle
  - Meaningful naming conventions
  - Code organization and structure
  - Documentation best practices

- **Design Patterns**
  - Strategy Pattern implementation
  - Factory Pattern implementation
  - Separation of concerns
  - Interface-based design

- **C# Best Practices**
  - Exception handling
  - File I/O operations
  - LINQ usage
  - Enum implementation
  - Command-line argument parsing

## Project Structure
```
MaximalSumOfElements/
├── MaximalSumOfElements/           # Main project
│   ├── Interfaces/                 # Interface definitions
│   │   └── ICalculationStrategy.cs
│   ├── Strategies/                 # Strategy implementations
│   │   ├── MaximumStrategy.cs
│   │   └── MinimumStrategy.cs
│   ├── Enums/                     # Enum definitions
│   │   └── CalculationStrategyType.cs
│   ├── Factories/                 # Factory implementations
│   │   └── CalculationStrategyFactory.cs
│   ├── FileSumsAnalyzer.cs        # Core analysis logic
│   └── Program.cs                 # Application entry point
├── LineSumAnalyzerTests/          # Test project
└── README.md                      # This file
```

## How It Works
The application reads a text file containing sets of numbers separated by commas. For each line, it:
1. Calculates the sum of all numbers
2. Identifies lines with invalid data
3. Finds the line(s) with the maximum or minimum sum

## Getting Started

### Building the Project
```bash
dotnet build
```

### Running the Application
```bash
# Find maximum sum
dotnet run -- path/to/your/file.txt

# Find minimum sum
dotnet run -- path/to/your/file.txt -min

# Show help
dotnet run -- -h
```

### Example Input File
```
1,2,3
4,5,6
7,8,9
1,2,invalid
5,6,7
```

### Example Output
```
Broken lines: 4
Extreme sum(s): 
Line '3' with sum '24'
```

## Key Learning Points

### 1. Strategy Pattern Implementation
The project demonstrates a proper implementation of the Strategy pattern:
- `ICalculationStrategy` interface defines the contract
- Concrete strategies (`MaximumStrategy` and `MinimumStrategy`) implement the interface
- `FileSumsAnalyzer` uses the strategy through the interface
- Strategies can be swapped at runtime

### 2. Factory Pattern
The `CalculationStrategyFactory` demonstrates the Factory pattern:
- Encapsulates strategy creation logic
- Provides a clean way to create strategy instances
- Makes it easy to add new strategies

### 3. Clean Code
The project shows how to:
- Write self-documenting code
- Use meaningful variable names
- Implement proper error handling
- Follow single responsibility principle
- Organize code into logical folders

### 4. Error Handling
The project demonstrates proper exception handling and user feedback:
- File not found scenarios
- Invalid input handling
- User-friendly error messages

### 5. Testing
The included test project shows how to:
- Write unit tests
- Test edge cases
- Verify business logic