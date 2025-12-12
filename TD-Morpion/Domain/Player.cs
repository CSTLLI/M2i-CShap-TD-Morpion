namespace Morpion;

public class Player
{
    public char Symbol { get; }
    public bool IsBot { get; }
    private Random? _random;

    public Player(char symbol, bool isBot = false)
    {
        Symbol = symbol;
        IsBot = isBot;
        if (isBot)
        {
            _random = new Random();
        }
    }

    public async Task<(int line, int column)> GetMove(Board board)
    {
        if (IsBot)
        {
            Console.WriteLine($"Le robot {Symbol} réfléchit...");

            int thinkingTime = _random!.Next(500, 2500);
            await Task.Delay(thinkingTime);

            var availableMoves = board.GetAvailableMoves();
            int randomPosition = availableMoves[_random!.Next(availableMoves.Count)];
            Console.WriteLine($"Le robot {Symbol} joue en position {randomPosition}");
            return board.IsValidMove(randomPosition)!.Value;
        }

        Console.Write($"Joueur {Symbol}, choisissez une position (1-9) : ");
        string? input = Console.ReadLine();

        if (int.TryParse(input, out int position))
        {
            var coordinates = board.IsValidMove(position);
            if (coordinates.HasValue)
            {
                return coordinates.Value;
            }
            Console.WriteLine("Cette case est déjà occupée ou position invalide !");
        }
        else
        {
            Console.WriteLine("Position invalide ! Réessayez.");
        }

        return await GetMove(board);
    }
}
