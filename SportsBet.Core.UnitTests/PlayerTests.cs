using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBet.Core.UnitTests;

[Trait("Category", "Unit")]
public class PlayerTests
{
    [Fact]
    public void Player_Equals_SameId_ShouldBeEqual()
    {
        var player1 = new Player(1, "Player 1");
        var player2 = new Player(1, "Player 2");

        player1.Equals(player2).Should().BeTrue();
    }

    [Fact]
    public void Player_GetHashCode_SameId_ShouldBeSameHashCode()
    {
        var player1 = new Player(1, "Player 1");
        var player2 = new Player(1, "Player 2");

        player1.GetHashCode().Should().Be(player2.GetHashCode());
    }

    [Fact]
    public void Player_SetPrimaryPositionIfNotSet_ShouldSetPositionOnce()
    {
        var player = new Player(1, "Player 1");
        player.SetPrimaryPosition(Position.QB);
        player.Position.Should().Be(Position.QB);
    }

    [Fact]
    public void Player_Equals_DifferentId_ShouldNotBeEqual()
    {
        var player1 = new Player(1, "Player 1");
        var player2 = new Player(2, "Player 2");

        player1.Equals(player2).Should().BeFalse();
    }

    [Fact]
    public void Player_Equals_DIfferentType_ShouldNotBeEqual()
    {
        var player1 = new Player(1, "Player 1");

        player1.Equals("text").Should().BeFalse();
    }
}
