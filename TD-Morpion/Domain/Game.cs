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
        _playerO = new Player('O', isBot: true);
        _currentPlayer = _playerX;
    }
    
    public async Task Run()
    {
        while (true)
        {
            Console.WriteLine("=== JEU DE MORPION ===\n");
            await PlayRound();

            if (!AskForReplay())
                break;

            ResetGame();
        }
    }

    private async Task PlayRound()
    {
        while (true)
        {
            _board.Display();

            var (line, column) = await _currentPlayer.GetMove(_board);
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

    private bool AskForReplay()
    {
        Console.WriteLine("\nAppuyez sur Entrée pour rejouer ou tapez 'q' pour quitter...");
        string? input = Console.ReadLine();

        if (input?.ToLower() == "q")
        {
            Console.WriteLine("Merci d'avoir joué !");
            return false;
        }

        return true;
    }

    private void ResetGame()
    {
        Console.Clear();
        _board = new Board();
        _currentPlayer = _playerX;
    }

    private void SwitchPlayer()
    {
        _currentPlayer = _currentPlayer == _playerX ? _playerO : _playerX;
    }
}
