using Morpion;
using System.Reflection;

namespace TD_Morpion_UnitTest;

public class GameUnitTest
{
    [Fact]
    public void Constructor_ShouldInitializeGameWithBoardAndPlayers()
    {
        var game = new Game();

        Assert.NotNull(game);

        var boardField = typeof(Game).GetField("_board", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerXField = typeof(Game).GetField("_playerX", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerOField = typeof(Game).GetField("_playerO", BindingFlags.NonPublic | BindingFlags.Instance);
        var currentPlayerField = typeof(Game).GetField("_currentPlayer", BindingFlags.NonPublic | BindingFlags.Instance);

        Assert.NotNull(boardField?.GetValue(game));
        Assert.NotNull(playerXField?.GetValue(game));
        Assert.NotNull(playerOField?.GetValue(game));
        Assert.NotNull(currentPlayerField?.GetValue(game));
    }

    [Fact]
    public void Constructor_ShouldInitializePlayerXAsCurrentPlayer()
    {
        var game = new Game();

        var playerXField = typeof(Game).GetField("_playerX", BindingFlags.NonPublic | BindingFlags.Instance);
        var currentPlayerField = typeof(Game).GetField("_currentPlayer", BindingFlags.NonPublic | BindingFlags.Instance);

        var playerX = playerXField?.GetValue(game);
        var currentPlayer = currentPlayerField?.GetValue(game);

        Assert.Equal(playerX, currentPlayer);
    }

    [Fact]
    public void Constructor_ShouldInitializePlayerXWithSymbolX()
    {
        var game = new Game();

        var playerXField = typeof(Game).GetField("_playerX", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerX = playerXField?.GetValue(game) as Player;

        Assert.NotNull(playerX);
        Assert.Equal('X', playerX.Symbol);
    }

    [Fact]
    public void Constructor_ShouldInitializePlayerOWithSymbolO()
    {
        var game = new Game();

        var playerOField = typeof(Game).GetField("_playerO", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerO = playerOField?.GetValue(game) as Player;

        Assert.NotNull(playerO);
        Assert.Equal('O', playerO.Symbol);
    }

    [Fact]
    public void Constructor_ShouldInitializePlayerOAsBot()
    {
        var game = new Game();

        var playerOField = typeof(Game).GetField("_playerO", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerO = playerOField?.GetValue(game) as Player;

        Assert.NotNull(playerO);
        Assert.True(playerO.IsBot);
    }

    [Fact]
    public void Constructor_ShouldInitializePlayerXAsHuman()
    {
        var game = new Game();

        var playerXField = typeof(Game).GetField("_playerX", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerX = playerXField?.GetValue(game) as Player;

        Assert.NotNull(playerX);
        Assert.False(playerX.IsBot);
    }

    [Fact]
    public void SwitchPlayer_ShouldAlternateFromPlayerXToPlayerO()
    {
        var game = new Game();
        var switchPlayerMethod = typeof(Game).GetMethod("SwitchPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
        var currentPlayerField = typeof(Game).GetField("_currentPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerOField = typeof(Game).GetField("_playerO", BindingFlags.NonPublic | BindingFlags.Instance);

        switchPlayerMethod?.Invoke(game, null);

        var currentPlayer = currentPlayerField?.GetValue(game);
        var playerO = playerOField?.GetValue(game);
        Assert.Equal(playerO, currentPlayer);
    }

    [Fact]
    public void SwitchPlayer_ShouldAlternateFromPlayerOToPlayerX()
    {
        var game = new Game();
        var switchPlayerMethod = typeof(Game).GetMethod("SwitchPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
        var currentPlayerField = typeof(Game).GetField("_currentPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerXField = typeof(Game).GetField("_playerX", BindingFlags.NonPublic | BindingFlags.Instance);

        switchPlayerMethod?.Invoke(game, null);
        switchPlayerMethod?.Invoke(game, null);

        var currentPlayer = currentPlayerField?.GetValue(game);
        var playerX = playerXField?.GetValue(game);
        Assert.Equal(playerX, currentPlayer);
    }

    [Fact]
    public void SwitchPlayer_CalledMultipleTimes_ShouldAlternateBetweenPlayers()
    {
        var game = new Game();
        var switchPlayerMethod = typeof(Game).GetMethod("SwitchPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
        var currentPlayerField = typeof(Game).GetField("_currentPlayer", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerXField = typeof(Game).GetField("_playerX", BindingFlags.NonPublic | BindingFlags.Instance);
        var playerOField = typeof(Game).GetField("_playerO", BindingFlags.NonPublic | BindingFlags.Instance);

        var playerX = playerXField?.GetValue(game);
        var playerO = playerOField?.GetValue(game);

        for (int i = 0; i < 10; i++)
        {
            switchPlayerMethod?.Invoke(game, null);
            var currentPlayer = currentPlayerField?.GetValue(game);

            if (i % 2 == 0)
            {
                Assert.Equal(playerO, currentPlayer);
            }
            else
            {
                Assert.Equal(playerX, currentPlayer);
            }
        }
    }
}
