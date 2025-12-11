namespace Morpion;

public class Game
{
    private Board _board;
    private Player _currentPlayer;
    private Player _playerX;
    private Player _playerO;

    public Game()
    {
        _board = new Board();
        _playerX = new Player('X');
        _playerO = new Player('O');
        _currentPlayer = _playerX;
    }
    
    public void Run()
    {
        Console.WriteLine("=== JEU DE MORPION ===\n");

        while (true)
        {
            _board.Display();

            var (line, column) = GetPlayerMove();
            _board.PlayMove(line, column, _currentPlayer.Symbol);

            if (_board.CheckWin(_currentPlayer.Symbol))
            {
                _board.Display();
                Console.WriteLine($"🎉 Le joueur {_currentPlayer.Symbol} a gagné !");
                break;
            }

            if (_board.IsFull())
            {
                _board.Display();
                Console.WriteLine("Match nul ! Égalité !");
                break;
            }

            SwitchPlayer();
        }
    }

    private (int line, int column) GetPlayerMove()
    {
        Console.Write($"Joueur {_currentPlayer.Symbol}, choisissez une position (1-9) : ");
        string? input = Console.ReadLine();

        if (int.TryParse(input, out int position))
        {
            var coordinates = _board.IsValidMove(position);
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

        return GetPlayerMove();
    }

    private void SwitchPlayer()
    {
        _currentPlayer = _currentPlayer == _playerX ? _playerO : _playerX;
    }
}
