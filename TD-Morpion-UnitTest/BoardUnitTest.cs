using Morpion;

namespace TD_Morpion_UnitTest;

public class BoardUnitTest
{
    [Fact]
    public void Constructor_ShouldInitializeEmptyBoard()
    {
        var board = new Board();

        var availableMoves = board.GetAvailableMoves();
        Assert.Equal(9, availableMoves.Count);
    }

    [Theory]
    [InlineData(1, 0, 0)]
    [InlineData(2, 0, 1)]
    [InlineData(3, 0, 2)]
    [InlineData(4, 1, 0)]
    [InlineData(5, 1, 1)]
    [InlineData(6, 1, 2)]
    [InlineData(7, 2, 0)]
    [InlineData(8, 2, 1)]
    [InlineData(9, 2, 2)]
    public void IsValidMove_WithValidPosition_ShouldReturnCorrectCoordinates(int position, int expectedLine, int expectedColumn)
    {
        var board = new Board();

        var result = board.IsValidMove(position);

        Assert.NotNull(result);
        Assert.Equal(expectedLine, result.Value.line);
        Assert.Equal(expectedColumn, result.Value.column);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    [InlineData(-1)]
    [InlineData(100)]
    public void IsValidMove_WithInvalidPosition_ShouldReturnNull(int position)
    {
        var board = new Board();

        var result = board.IsValidMove(position);

        Assert.Null(result);
    }

    [Fact]
    public void IsValidMove_WithOccupiedPosition_ShouldReturnNull()
    {
        var board = new Board();
        board.PlayMove(0, 0, 'X');

        var result = board.IsValidMove(1);

        Assert.Null(result);
    }

    [Fact]
    public void PlayMove_ShouldPlaceSymbolOnBoard()
    {
        var board = new Board();

        board.PlayMove(1, 1, 'X');

        var availableMoves = board.GetAvailableMoves();
        Assert.Equal(8, availableMoves.Count);
        Assert.DoesNotContain(5, availableMoves); // Position 5 est (1,1)
    }

    [Theory]
    [InlineData(0, 0, 0, 1, 0, 2)] // Ligne 1
    [InlineData(1, 0, 1, 1, 1, 2)] // Ligne 2
    [InlineData(2, 0, 2, 1, 2, 2)] // Ligne 3
    public void CheckWin_WithWinningRow_ShouldReturnTrue(int line1, int col1, int line2, int col2, int line3, int col3)
    {
        var board = new Board();
        board.PlayMove(line1, col1, 'X');
        board.PlayMove(line2, col2, 'X');
        board.PlayMove(line3, col3, 'X');

        var result = board.CheckWin('X');

        Assert.True(result);
    }

    [Theory]
    [InlineData(0, 0, 1, 0, 2, 0)] // Colonne 1
    [InlineData(0, 1, 1, 1, 2, 1)] // Colonne 2
    [InlineData(0, 2, 1, 2, 2, 2)] // Colonne 3
    public void CheckWin_WithWinningColumn_ShouldReturnTrue(int line1, int col1, int line2, int col2, int line3, int col3)
    {
        var board = new Board();
        board.PlayMove(line1, col1, 'O');
        board.PlayMove(line2, col2, 'O');
        board.PlayMove(line3, col3, 'O');

        var result = board.CheckWin('O');

        Assert.True(result);
    }

    [Fact]
    public void CheckWin_WithWinningDiagonalTopLeftToBottomRight_ShouldReturnTrue()
    {
        var board = new Board();
        board.PlayMove(0, 0, 'X');
        board.PlayMove(1, 1, 'X');
        board.PlayMove(2, 2, 'X');

        var result = board.CheckWin('X');

        Assert.True(result);
    }

    [Fact]
    public void CheckWin_WithWinningDiagonalTopRightToBottomLeft_ShouldReturnTrue()
    {
        var board = new Board();
        board.PlayMove(0, 2, 'O');
        board.PlayMove(1, 1, 'O');
        board.PlayMove(2, 0, 'O');

        var result = board.CheckWin('O');

        Assert.True(result);
    }

    [Fact]
    public void CheckWin_WithNoWin_ShouldReturnFalse()
    {
        var board = new Board();
        board.PlayMove(0, 0, 'X');
        board.PlayMove(1, 1, 'X');

        var result = board.CheckWin('X');

        Assert.False(result);
    }

    [Fact]
    public void IsFull_WithEmptyBoard_ShouldReturnFalse()
    {
        var board = new Board();

        var result = board.IsFull();

        Assert.False(result);
    }

    [Fact]
    public void IsFull_WithPartiallyFilledBoard_ShouldReturnFalse()
    {
        var board = new Board();
        board.PlayMove(0, 0, 'X');
        board.PlayMove(1, 1, 'O');

        var result = board.IsFull();

        Assert.False(result);
    }

    [Fact]
    public void IsFull_WithFullBoard_ShouldReturnTrue()
    {
        var board = new Board();
        board.PlayMove(0, 0, 'X');
        board.PlayMove(0, 1, 'O');
        board.PlayMove(0, 2, 'X');
        board.PlayMove(1, 0, 'O');
        board.PlayMove(1, 1, 'X');
        board.PlayMove(1, 2, 'O');
        board.PlayMove(2, 0, 'O');
        board.PlayMove(2, 1, 'X');
        board.PlayMove(2, 2, 'O');

        var result = board.IsFull();

        Assert.True(result);
    }

    [Fact]
    public void GetAvailableMoves_WithEmptyBoard_ShouldReturn9Moves()
    {
        var board = new Board();

        var moves = board.GetAvailableMoves();

        Assert.Equal(9, moves.Count);
        Assert.Equal(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, moves);
    }

    [Fact]
    public void GetAvailableMoves_WithSomeMoves_ShouldReturnRemainingMoves()
    {
        var board = new Board();
        board.PlayMove(0, 0, 'X'); // Position 1
        board.PlayMove(1, 1, 'O'); // Position 5
        board.PlayMove(2, 2, 'X'); // Position 9

        var moves = board.GetAvailableMoves();

        Assert.Equal(6, moves.Count);
        Assert.DoesNotContain(1, moves);
        Assert.DoesNotContain(5, moves);
        Assert.DoesNotContain(9, moves);
    }

    [Fact]
    public void GetAvailableMoves_WithFullBoard_ShouldReturnEmptyList()
    {
        var board = new Board();
        board.PlayMove(0, 0, 'X');
        board.PlayMove(0, 1, 'O');
        board.PlayMove(0, 2, 'X');
        board.PlayMove(1, 0, 'O');
        board.PlayMove(1, 1, 'X');
        board.PlayMove(1, 2, 'O');
        board.PlayMove(2, 0, 'O');
        board.PlayMove(2, 1, 'X');
        board.PlayMove(2, 2, 'O');

        var moves = board.GetAvailableMoves();

        Assert.Empty(moves);
    }
}
