using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBet.Core;

/// <summary>
/// Represents a named position or identifier within a collection or domain.
/// </summary>
/// <param name="name">The name that uniquely identifies the position. Cannot be null.</param>
/// <remarks>
/// Use of a partial struct allows for future extensibility, such as adding predefined positions or additional functionality.
/// </remarks>
public partial struct Position(string name) : IEquatable<Position>
{
    private readonly string _name = name;

    public readonly string Name { get => _name; }

    public readonly bool Equals(Position other) => _name.Equals(other._name, StringComparison.OrdinalIgnoreCase);

    public override readonly string ToString()
    {
        return _name;
    }

    public override readonly bool Equals(object? obj)
    {
        return obj is Position && Equals((Position)obj);
    }

    public override readonly int GetHashCode() => _name.GetHashCode(StringComparison.OrdinalIgnoreCase);
}
