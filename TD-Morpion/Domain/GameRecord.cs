namespace Morpion;

public class GameRecord
{
    public int Id { get; set; }
    public string Winner { get; set; } = string.Empty; // "X", "O", ou "Draw"
    public DateTime PlayedAt { get; set; } = DateTime.Now;
}
