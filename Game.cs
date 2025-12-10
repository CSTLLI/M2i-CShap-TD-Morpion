namespace Morpion;

public class Game
{
    private Board _board;
    private char _currentPlayer;

    public Game()
    {
        _board = new Board();
        _currentPlayer = 'X';
    }
    
    public void Run()
    {
        Console.WriteLine("=== JEU DE MORPION ===\n");

        while (true)
        {
            _board.Display();

            // Étape 4 : Saisir et valider un coup
            int position = GetPlayerMove();
            _board.PlayMove(position, _currentPlayer);

            // Étape 5 : Vérifier la victoire
            if (_board.CheckWin(_currentPlayer))
            {
                _board.Display();
                Console.WriteLine($"🎉 Le joueur {_currentPlayer} a gagné !");
                break;
            }

            SwitchPlayer();
        }
    }

    private int GetPlayerMove()
    {
        Console.Write($"Joueur {_currentPlayer}, choisissez une position (1-9) : ");
        string? input = Console.ReadLine();

        if (int.TryParse(input, out int position) && position >= 1 && position <= 9)
        {
            if (_board.IsValidMove(position))
            {
                return position;
            }
            Console.WriteLine("Cette case est déjà occupée !");
        }
        else
        {
            Console.WriteLine("Position invalide ! Réessayez.");
        }

        return GetPlayerMove();
    }

    private void SwitchPlayer()
    {
        _currentPlayer = _currentPlayer == 'X' ? 'O' : 'X';
    }
}
