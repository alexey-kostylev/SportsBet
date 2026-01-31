using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBet.Core;

/// <summary>
/// Represents a player with a unique identifier and a display name.
/// </summary>
/// <param name="Id">The unique identifier for the player.</param>
/// <param name="Name">The display name of the player.</param>
public record Player(int Id, string Name)
{
    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    /// <summary>
    /// Determines whether the specified player is equal to the current player based on their unique identifiers.
    /// </summary>
    /// <param name="obj">The player to compare with the current player. Can be null.</param>
    /// <returns>true if the specified player is not null and has the same unique identifier as the current player; otherwise,
    /// false.</returns>
    public virtual bool Equals(Player? obj)
    {
        if (obj is not Player other)
        {
            return false;
        }
        return Id == other.Id;
    }
}
