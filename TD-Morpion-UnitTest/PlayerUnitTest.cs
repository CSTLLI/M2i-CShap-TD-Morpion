using Morpion;

namespace TD_Morpion_UnitTest;

public class PlayerUnitTest
{
    [Fact]
    public void Constructor_WithSymbolOnly_ShouldCreateHumanPlayer()
    {
        var player = new Player('X');

        Assert.Equal('X', player.Symbol);
        Assert.False(player.IsBot);
    }

    [Fact]
    public void Constructor_WithSymbolAndIsBot_ShouldCreateBotPlayer()
    {
        var player = new Player('O', isBot: true);

        Assert.Equal('O', player.Symbol);
        Assert.True(player.IsBot);
    }

    [Fact]
    public void Constructor_WithSymbolAndIsBotFalse_ShouldCreateHumanPlayer()
    {
        var player = new Player('X', isBot: false);

        Assert.Equal('X', player.Symbol);
        Assert.False(player.IsBot);
    }

    [Theory]
    [InlineData('X')]
    [InlineData('O')]
    [InlineData('A')]
    [InlineData('Z')]
    public void Symbol_ShouldReturnCorrectSymbol(char symbol)
    {
        var player = new Player(symbol);

        Assert.Equal(symbol, player.Symbol);
    }

    [Fact]
    public void GetMove_WithBot_ShouldReturnValidMove()
    {
        var board = new Board();
        var bot = new Player('O', isBot: true);

        var (line, column) = bot.GetMove(board);

        Assert.InRange(line, 0, 2);
        Assert.InRange(column, 0, 2);
        Assert.NotNull(board.IsValidMove(line * 3 + column + 1));
    }

    [Fact]
    public void GetMove_WithBot_ShouldReturnDifferentMovesOnDifferentCalls()
    {
        var board = new Board();
        var bot = new Player('O', isBot: true);
        var moves = new HashSet<(int, int)>();

        for (int i = 0; i < 20; i++)
        {
            var move = bot.GetMove(board);
            moves.Add(move);
            if (moves.Count >= 2)
            {
                break;
            }
        }

        Assert.True(moves.Count >= 2, "Le bot devrait choisir différentes positions de manière aléatoire");
    }

    [Fact]
    public void GetMove_WithBot_ShouldOnlySelectAvailableMoves()
    {
        var board = new Board();
        board.PlayMove(0, 0, 'X'); // Position 1
        board.PlayMove(0, 1, 'X'); // Position 2
        board.PlayMove(0, 2, 'X'); // Position 3
        board.PlayMove(1, 0, 'X'); // Position 4
        board.PlayMove(1, 1, 'X'); // Position 5
        board.PlayMove(1, 2, 'X'); // Position 6
        board.PlayMove(2, 0, 'X'); // Position 7
        board.PlayMove(2, 1, 'X'); // Position 8

        var bot = new Player('O', isBot: true);

        var (line, column) = bot.GetMove(board);

        Assert.Equal(2, line);
        Assert.Equal(2, column);
    }

    [Fact]
    public void GetMove_WithBotOnPartiallyFilledBoard_ShouldSelectFromAvailableMoves()
    {
        var board = new Board();
        board.PlayMove(0, 0, 'X'); // Position 1
        board.PlayMove(1, 1, 'O'); // Position 5
        board.PlayMove(2, 2, 'X'); // Position 9

        var bot = new Player('O', isBot: true);
        var validPositions = new List<(int, int)>
        {
            (0, 1), (0, 2), // Positions 2, 3
            (1, 0), (1, 2), // Positions 4, 6
            (2, 0), (2, 1)  // Positions 7, 8
        };

        var move = bot.GetMove(board);

        Assert.Contains(move, validPositions);
    }

    [Fact]
    public void IsBot_ForBotPlayer_ShouldReturnTrue()
    {
        var bot = new Player('O', isBot: true);

        Assert.True(bot.IsBot);
    }

    [Fact]
    public void IsBot_ForHumanPlayer_ShouldReturnFalse()
    {
        var human = new Player('X', isBot: false);

        Assert.False(human.IsBot);
    }

    [Fact]
    public void GetMove_WithBot_ShouldAlwaysReturnValidCoordinates()
    {
        var board = new Board();
        var bot = new Player('O', isBot: true);

        for (int i = 0; i < 100; i++)
        {
            var (line, column) = bot.GetMove(board);
            Assert.InRange(line, 0, 2);
            Assert.InRange(column, 0, 2);
        }
    }

    [Fact]
    public void Constructor_MultipleBots_ShouldEachHaveIndependentRandomness()
    {
        var board = new Board();
        var bot1 = new Player('O', isBot: true);
        var bot2 = new Player('X', isBot: true);

        var moves1 = new HashSet<(int, int)>();
        var moves2 = new HashSet<(int, int)>();
        
        for (int i = 0; i < 10; i++)
        {
            moves1.Add(bot1.GetMove(board));
            moves2.Add(bot2.GetMove(board));
        }

        Assert.NotEmpty(moves1);
        Assert.NotEmpty(moves2);
    }
}
