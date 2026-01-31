using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SportsBet.Core;

/// <summary>
/// Represents a sport and its associated supported positions.
/// </summary>
/// <remarks>Use this record to manage the depth chart and player assignments for a specific sport. The set of
/// supported positions defines which positions are valid for player assignment within this sport.</remarks>
/// <param name="name">The name of the sport.</param>
/// <param name="SupportedPositions">A set of positions that are valid for this sport.</param>
public record Sport(string name, HashSet<Position> SupportedPositions)
{
    private readonly Dictionary<Position, IList<Player>> _positionSlots = new() { };

    /// <summary>
    /// Adds a player to the specified position in the depth chart at the given depth, or at the end if no depth is
    /// specified.
    /// </summary>
    /// <remarks>If the player already exists at the specified position, the method does not add the player
    /// again or modify the depth chart.</remarks>
    /// <param name="player">The player to add to the depth chart. If the player is already present at the specified position, the method has
    /// no effect.</param>
    /// <param name="position">The position to which the player will be added. Must be a supported position.</param>
    /// <param name="position_depth">The zero-based depth at which to insert the player within the position's depth chart. If null, the player is
    /// added to the end.</param>
    /// <exception cref="ArgumentException">Thrown if the specified position is not supported.</exception>
    public void AddPlayerToDepthChart(Player player, Position position, int? position_depth = null)
    {
        if (SupportedPositions.Contains(position) == false)
        {
            throw new ArgumentException($"Position {position} is not supported in '{name}'.");
        }

        if (!_positionSlots.TryGetValue(position, out var players))
        {
            players = [];
            _positionSlots[position] = players;
        }

        // guard for duplicates
        if (players.Contains(player))
        {
            return;
        }

        if (position_depth == null)
        {
            players.Add(player);
            return;
        }

        players.Insert(position_depth.Value, player);
    }

    /// <summary>
    /// Removes the specified player from the depth chart at the given position.
    /// </summary>
    /// <param name="player">The player to remove from the depth chart.</param>
    /// <param name="position">The position from which to remove the player.</param>
    /// <exception cref="InvalidOperationException">Thrown if the specified position does not exist in the depth chart.</exception>
    public void RemovePlayerFromDepthChart(Player player, Position position)
    {
        if (!_positionSlots.TryGetValue(position, out var players))
        {
            throw new InvalidOperationException($"Player '{player.Name}' does not exist in position '{position}'.");
        }

        players.Remove(player);
    }

    /// <summary>
    /// Retrieves the complete depth chart, listing all players assigned to each position.
    /// </summary>
    /// <remarks>The returned dictionary and its lists are read-only. Modifying the returned collections will
    /// result in a runtime exception.</remarks>
    /// <returns>An immutable dictionary mapping each position to a list of players in depth order. The dictionary is empty if no
    /// players are assigned.</returns>
    public IReadOnlyDictionary<Position, IList<Player>> GetFullDepthChart() => _positionSlots.AsReadOnly();

    /// <summary>
    /// Gets a read-only collection of players who are listed below the specified player at the given position in the
    /// depth chart.
    /// </summary>
    /// <param name="player">The player whose position in the depth chart is used as the reference point. Only players listed below this
    /// player will be included in the result.</param>
    /// <param name="position">The position in the depth chart to search for players under the specified player.</param>
    /// <returns>A read-only collection of players listed below the specified player at the given position. Returns an empty
    /// collection if the player is not found at the position or if there are no players below.</returns>
    /// <exception cref="ArgumentException">Thrown if the specified position does not exist in the depth chart.</exception>
    public IReadOnlyCollection<Player> GetPlayersUnderPlayerInDepthChart(Player player, Position position)
    {
        if (!_positionSlots.TryGetValue(position, out var players))
        {
            throw new ArgumentException($"Position {position} does not exist in '{name}'.");
        }

        var playerIndex = players.IndexOf(player);
        if (playerIndex == -1)
        {
            return [];
        }

        return [.. players.Skip(playerIndex + 1)];
    }
}
