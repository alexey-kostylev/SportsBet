namespace SportsBet.Core.UnitTests;


[Trait("Category", "Unit")]
public class SportTests
{
    private readonly Sport _sport = new(
        "NFL",
        [Position.QB, Position.WR]
        );

    [Fact]
    public void AddPlayerToDepthChart_ShouldAddPlayer()
    {
        var player = new Player(1, "Player 1");
        _sport.AddPlayerToDepthChart(player, Position.QB, 0);

        _sport.GetFullDepthChart()[Position.QB].Should().HaveCount(1);
    }

    [Fact]
    public void AddPlayerToDepthChart_ShouldAddPlayerToSecondaryPosition()
    {
        var player = new Player(1, "Player 1");
        _sport.AddPlayerToDepthChart(player, Position.QB, 0);
        _sport.AddPlayerToDepthChart(player, Position.WR);

        var depthChart = _sport.GetFullDepthChart();
        depthChart[Position.QB].Should().HaveCount(1);
        depthChart[Position.WR].Should().HaveCount(1);
    }

    [Fact]
    public void AddPlayerToDepthChart_ShouldAddPlayersAndMaintainCorrectOrder()
    {
        _sport.AddPlayerToDepthChart(new Player(1, "Player 1"), Position.QB, 0);
        _sport.AddPlayerToDepthChart(new Player(2, "Player 2"), Position.QB, 1);
        _sport.AddPlayerToDepthChart(new Player(3, "Player 3"), Position.QB, 1);

        var fullDepthChart = _sport.GetFullDepthChart();
        fullDepthChart[Position.QB][0].Name.Should().Be("Player 1");
        fullDepthChart[Position.QB][1].Name.Should().Be("Player 3");
        fullDepthChart[Position.QB][2].Name.Should().Be("Player 2");
    }

    [Fact]
    public void AddPlayerToDepthChart_ShouldNotAddDuplicatePlayer()
    {
        _sport.AddPlayerToDepthChart(new Player(1, "Player 1"), Position.QB, 0);
        _sport.AddPlayerToDepthChart(new Player(1, "Player 1"), Position.QB, 0);

        _sport.GetFullDepthChart()[Position.QB].Should().HaveCount(1);
    }

    [Fact]
    public void AddPlayerToDepthChart_UnsupportedPosition_ShouldThrow()
    {
        Action act = () => _sport.AddPlayerToDepthChart(new Player(1, "Player 1"), Position.KR, 0);

        act.Should().Throw<ArgumentException>().WithMessage("Position KR is not supported in 'NFL'.");
    }

    [Fact]
    public void AddPlayerToDepthChart_PlayerIsNull_ShouldThrow()
    {
        Action act = () => _sport.AddPlayerToDepthChart(null!, Position.KR, 0);

        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void RemovePlayerFromDepthChart_ShouldRemovePlayer()
    {
        var player1 = new Player(1, "Player 1");
        _sport.AddPlayerToDepthChart(player1, Position.QB, 0);
        _sport.RemovePlayerFromDepthChart(player1, Position.QB);
        _sport.GetFullDepthChart()[Position.QB].Should().BeEmpty();
    }

    [Fact]
    public void RemovePlayerFromDepthChart_PlayerDoesNotExist_ShouldThrowException()
    {
        var player1 = new Player(1, "Player 1");
        _sport.AddPlayerToDepthChart(player1, Position.QB, 0);

        Action act = () => _sport.RemovePlayerFromDepthChart(new Player(100, "test"), Position.QB);

        act.Should().Throw<InvalidOperationException>().WithMessage("Player 'test' does not exist in position 'QB'.");

        _sport.GetFullDepthChart()[Position.QB].Should().HaveCount(1);
    }

    [Fact]
    public void RemovePlayerFromDepthChart_PlayerIsNull_ShouldThrowException()
    {
        _sport.AddPlayerToDepthChart(new Player(1, "Player 1"), Position.QB, 0);

        Action act = () => _sport.RemovePlayerFromDepthChart(null!, Position.QB);

        act.Should().Throw<ArgumentNullException>();

        _sport.GetFullDepthChart()[Position.QB].Should().HaveCount(1);
    }

    [Fact]
    public void RemovePlayerFromDepthChart_PositionDoesNotExist_ShouldThrowException()
    {
        var player1 = new Player(1, "Player 1");
        _sport.AddPlayerToDepthChart(player1, Position.QB, 0);

        Action act = () => _sport.RemovePlayerFromDepthChart(player1, Position.WR);

        act.Should().Throw<InvalidOperationException>().WithMessage("Player 'Player 1' does not exist in position 'WR'.");

        _sport.GetFullDepthChart()[Position.QB].Should().HaveCount(1);
    }

    [Theory]
    [InlineData(1, "2, 3")]
    [InlineData(2, "3")]
    [InlineData(3, "")]
    [InlineData(4, "")]
    [InlineData(0, "")]
    public void GetPlayersUnderPlayerInDepthChart_ShouldReturnCorrectPlayers(int playerId, string expectedPlayersString)
    {
        var player1 = new Player(1, "Player 1");
        var player2 = new Player(2, "Player 2");
        var player3 = new Player(3, "Player 3");
        _sport.AddPlayerToDepthChart(player1, Position.QB, 0);
        _sport.AddPlayerToDepthChart(player2, Position.QB, 1);
        _sport.AddPlayerToDepthChart(player3, Position.QB, 2);
        var playersUnderPlayer = _sport.GetPlayersUnderPlayerInDepthChart(
            new Player(playerId, ""),
            Position.QB
        );

        var expectedIds = expectedPlayersString
            .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x))
            .ToArray();

        playersUnderPlayer
            .Select(p => p.Id)
            .Should().BeEquivalentTo(expectedIds);
    }

    [Fact]
    public void GetPlayersUnderPlayerInDepthChart_PlayerIsNull_ShouldThrowException()
    {
        Action act = () => _sport.GetPlayersUnderPlayerInDepthChart(
            null!,
            Position.QB
        );

        act.Should().Throw<ArgumentNullException>();
    }
}
