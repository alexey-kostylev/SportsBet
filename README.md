Here's the improved `README.md` file, incorporating the new content while maintaining the existing structure and information:

# SportsBet.Core

A small .NET 9 library that models sports domain concepts used by the SportsBet solution. This repository contains core domain models (players, positions, sports) and unit tests validating business rules and behavior.

## Contents

- `SportsBet.Core` — Core domain models and logic.
- `SportsBet.Core.UnitTests` — xUnit-based unit and acceptance tests.

## Requirements

- .NET 9 SDK
- Visual Studio 2022 (or newer) or `dotnet` CLI

This project targets C# 13 and .NET 9.

## Building

To build the project, navigate to the solution root using the CLI and run:

```bash
dotnet build```

Alternatively, you can open the solution in Visual Studio 2022 and build it using the menu option _Build > Build Solution_.

## Running Tests

To run all tests, you can use the CLI with the following command:
```bash
dotnet test
```

You can also run tests from the Test Explorer in Visual Studio for a more interactive experience.

## Project Structure

- `SportsBet.Core` — Domain model types (e.g., `Player`, `Sport`, `Position`, `PositionExtension`).
- `SportsBet.Core.UnitTests` — Tests that exercise and validate the core model behavior.
