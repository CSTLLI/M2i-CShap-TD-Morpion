namespace Morpion;

public class GameState
{
    public int Id { get; set; }
    public string BoardCells { get; set; } = string.Empty; // 9 chars: "XO  X O  "
    public char CurrentPlayer { get; set; } = 'X';
    public DateTime SavedAt { get; set; } = DateTime.Now;
}
