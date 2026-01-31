using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SportsBet.Core.UnitTests.AcceptanceTests;

[Trait("Category", "Acceptance Tests")]
public class SportAcceptanceTests
{



    /*
        Adds a player to a depth chart for a given position (at a specific spot). If
        no position_depth is provided, then add them to the end of the depth chart
        for that position. If you are entering two players into the same slot, the last
        player entered gets priority and bumps the existing player down a depth spot.
     */
    [Fact]
    public void ShouldAddPlayersAndGetFullDepthChartAndDisplayAPosityionUnderPlayer()
    {
        var bob = new Player(1, "Bob");
        var alice = new Player(2, "Alice");
        var charlie = new Player(3, "Charlie");
        var sport = new Sport(
            "NFL",
            [Position.WR, Position.KR]
            );
        sport.AddPlayerToDepthChart(bob, Position.WR, 0);
        sport.AddPlayerToDepthChart(alice, Position.WR, 0);
        sport.AddPlayerToDepthChart(charlie, Position.WR, 2);
        sport.AddPlayerToDepthChart(bob, Position.KR);

        /*
        Output:
        WR: [2, 1, 3],
        KR: [1]
        */
        var fullDepthChart = sport.GetFullDepthChart();
        fullDepthChart
            // get only player ids for easier assertion
            .Select(kv => new KeyValuePair<Position, List<int>>(
                kv.Key,
                kv.Value.Select(p => p.Id).ToList()
            ))
            .Should().BeEquivalentTo(new Dictionary<Position, List<int>>
        {
            { Position.WR, new List<int> { 2, 1, 3 } },
            { Position.KR, new List<int> { 1 } }
        });

        /*
        Output:
        [1,3]
        */
        var playersUnderAlice = sport.GetPlayersUnderPlayerInDepthChart(alice, Position.WR);
        playersUnderAlice
            .Select(p => p.Id)
            .Should().BeEquivalentTo([1, 3]);
    }

    /*
        Adds a player to a depth chart for a given position (at a specific spot). If
        no position_depth is provided, then add them to the end of the depth chart
        for that position. If you are entering two players into the same slot, the last
        player entered gets priority and bumps the existing player down a depth spot.
     */
    [Theory]
    [MemberData(nameof(GetTestDataForAddingPlayers))]
    public void Case1_ShouldAddPlayers(
        Sport sport,
        PlayerPosition[] playerPositions,
        Dictionary<Position, int[]> expectedDepthChart
    )
    {
        foreach (var (player, position, depth) in playerPositions)
        {
            sport.AddPlayerToDepthChart(player, position, depth);
        }

        var fullDepthChart = sport.GetFullDepthChart();

        var chartModel =
            fullDepthChart
                // get only player ids for easier assertion
                .Select(kv => new KeyValuePair<Position, List<int>>(
                    kv.Key,
                    kv.Value.Select(p => p.Id).ToList()
                ))
                .ToArray();

        chartModel.Should().BeEquivalentTo(expectedDepthChart);
    }

    /// <summary>
    /// Removes a player from the depth chart for a position
    /// </summary>
    /// <param name="sport"></param>
    /// <param name="position"></param>
    /// <param name="player"></param>
    /// <param name="expectedDepthChart"></param>
    [Theory]
    [MemberData(nameof(GetTestDataForRemovingPlayers))]
    public void Case2_ShouldRemovePlayer(Sport sport, PlayerPosition[] playerPositions, PlayerPosition playerPositionToRemove, Dictionary<Position, int[]> expectedDepthChart)
    {
        foreach (var (player, position, _) in playerPositions)
        {
            sport.AddPlayerToDepthChart(player, position);
        }

        sport.RemovePlayerFromDepthChart(playerPositionToRemove.Player, playerPositionToRemove.Position);

        var fullDepthChart = sport.GetFullDepthChart();
        var depthChartModel =
            fullDepthChart
                // get only player ids for easier assertion
                .Select(kv => new KeyValuePair<Position, List<int>>(
                    kv.Key,
                    [.. kv.Value.Select(p => p.Id)]
                ))
                .ToArray();

        depthChartModel
            .Should().BeEquivalentTo(expectedDepthChart);
    }

    /// <summary>
    /// Prints out all depth chart positions
    /// </summary>
    /// <param name="sport"></param>
    /// <param name="players"></param>
    /// <param name="expectedDepthChart"></param>
    [Theory]
    [MemberData(nameof(GetTestDataForGetFullDepthChart))]
    public void Case3_ShouldGetFullDepthChart(
        Sport sport,
        PlayerPosition[] playerPositions,
        Dictionary<Position, int[]> expectedDepthChart)
    {
        foreach(var (player, position, _) in playerPositions)
        {
            sport.AddPlayerToDepthChart(player, position);
        }

        var fullDepthChart = sport.GetFullDepthChart();
        var depthChartModel =
            fullDepthChart
                // get only player ids for easier assertion
                .Select(kv => new KeyValuePair<Position, List<int>>(
                    kv.Key,
                    [.. kv.Value.Select(p => p.Id)]
                ))
                .ToArray();

        depthChartModel
            .Should().BeEquivalentTo(expectedDepthChart);
    }

    [Theory]
    [MemberData(nameof(GetTestDataForGettingPlayersUnderPlayer))]
    public void Case4_ShouldGetPlayersUnderPlayer(
        Sport sport,
        PlayerPosition[] playerPositions,
        PlayerPosition playerPosition,
        ICollection<int> expectedPlayers)
    {
        foreach (var (player, position, _) in playerPositions)
        {
            sport.AddPlayerToDepthChart(player, position);
        }

        var actualPlayers = sport.GetPlayersUnderPlayerInDepthChart(playerPosition.Player, playerPosition.Position);

        actualPlayers
            .Select(p => p.Id)
            .Should().BeEquivalentTo(expectedPlayers);
    }

    private static Sport CreateNFL() => new("NFL", [Position.QB, Position.WR, Position.RB, Position.TE, Position.K, Position.P, Position.KR, Position.PR]);

    private static Sport CreateMLB() => new("MLB", [Position.SP, Position.RP, Position.C, Position.FirstBase, Position.SecondBase, Position.ThirdBase, Position.SS, Position.LF, Position.RF, Position.CF, Position.DH]);

    public record PlayerPosition(Player Player, Position Position, int? positionDepth = null);

    /// <summary>
    /// input: sport, player positions to use and expected depth chart after additions
    /// </summary>
    /// <returns></returns>
    public static TheoryData<Sport, PlayerPosition[], Dictionary<Position, int[]>> GetTestDataForAddingPlayers()
    {
        return new TheoryData<Sport, PlayerPosition[], Dictionary<Position, int[]>>
        {
            {
                CreateNFL(),
                [
                    new PlayerPosition(new Player(2, "alice"), Position.WR, 0),
                    new PlayerPosition(new Player(1, "bob"), Position.WR, 0),
                    new PlayerPosition(new Player(3, "charlie"), Position.WR, 2),
                    new PlayerPosition(new Player(1, "bob"), Position.KR)
                ],
                new Dictionary<Position, int[]>{
                    [Position.WR] = [2,1,3],
                    [Position.KR] = [1]
                }
            },
            {
                CreateMLB(),
                [
                    new PlayerPosition(new Player(2, "alice"), Position.SP, 0),
                    new PlayerPosition(new Player(1, "bob"), Position.SP, 0),
                    new PlayerPosition(new Player(3, "charlie"), Position.SP, 2),
                    new PlayerPosition(new Player(1, "bob"), Position.RP)
                ],
                new Dictionary<Position, int[]>{
                    [Position.SP] = [2,1,3],
                    [Position.RP] = [1]
                }
            }
        };
    }

    /// <summary>
    /// Get Test data for removing players, Sport, players to add, player to remove and expected depth chart after removal
    /// </summary>
    /// <returns></returns>
    public static TheoryData<Sport, PlayerPosition[], PlayerPosition, Dictionary<Position, int[]>> GetTestDataForRemovingPlayers()
    {
        return new TheoryData<Sport, PlayerPosition[], PlayerPosition, Dictionary<Position, int[]>>
        {
            {
                CreateNFL(),
                [
                    new PlayerPosition(new Player(1, "bob"), Position.WR),
                    new PlayerPosition(new Player(2, "alice"), Position.WR),
                    new PlayerPosition(new Player(3, "charlie"), Position.WR)
                ],
                new PlayerPosition(new Player(2, "alice"), Position.WR),
                new Dictionary<Position, int[]>{
                    [Position.WR] = [1,3]
                }
            },
            {
                CreateMLB(),
                [
                    new PlayerPosition(new Player(1, "bob"), Position.SP),
                    new PlayerPosition(new Player(2, "alice"), Position.SP),
                    new PlayerPosition(new Player(3, "charlie"), Position.SP)
                ],
                new PlayerPosition(new Player(2, "alice"), Position.SP),
                new Dictionary<Position, int[]>{
                    [Position.SP] = [1,3]
                }
            }
        };
    }

    public static TheoryData<Sport, PlayerPosition[], Dictionary<Position, int[]>> GetTestDataForGetFullDepthChart()
    {
        return new TheoryData<Sport, PlayerPosition[], Dictionary<Position, int[]>>
        {
            {
                CreateNFL(),
                [
                    new PlayerPosition(new Player(1, "bob"), Position.WR),
                    new PlayerPosition(new Player(2, "alice"), Position.WR),
                    new PlayerPosition(new Player(3, "charlie"), Position.QB)
                ],
                new Dictionary<Position, int[]>{
                    [Position.WR] = [1,2],
                    [Position.QB] = [3]
                }
            },
            {
                CreateMLB(),
                [
                    new PlayerPosition(new Player(1, "bob"), Position.SP),
                    new PlayerPosition(new Player(2, "alice"), Position.SP),
                    new PlayerPosition(new Player(3, "charlie"), Position.RP)
                ],
                new Dictionary<Position, int[]>{
                    [Position.SP] = [1,2],
                    [Position.RP] = [3]
                }
            }
        };
    }

    /// <summary>
    /// Test data for getting players under a specific player.
    /// Args: Sport, players to add, player to check under and expected players under
    /// </summary>
    /// <returns></returns>
    public static TheoryData<Sport, PlayerPosition[], PlayerPosition, ICollection<int>> GetTestDataForGettingPlayersUnderPlayer()
    {
        return new TheoryData<Sport, PlayerPosition[], PlayerPosition, ICollection<int>>
        {
            {
                CreateNFL(),
                [
                    new PlayerPosition(new Player(1, "bob"), Position.WR),
                    new PlayerPosition(new Player(2, "alice"), Position.WR),
                    new PlayerPosition(new Player(3, "charlie"), Position.WR)
                ],
                new PlayerPosition(new Player(1, "bob"), Position.WR),
                [2,3]
            },
            {
                CreateMLB(),
                [
                    new PlayerPosition(new Player(1, "bob"), Position.SP),
                    new PlayerPosition(new Player(2, "alice"), Position.SP),
                    new PlayerPosition(new Player(3, "charlie"), Position.SP)
                ],
                new PlayerPosition(new Player(1, "bob"), Position.SP),
                [2,3]
            }
        };
    }
}
