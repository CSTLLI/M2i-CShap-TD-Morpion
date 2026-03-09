using Microsoft.EntityFrameworkCore;

namespace Morpion;

public class Game
{
    private Board _board;
    private Player _currentPlayer;
    private Player _playerX;
    private Player _playerO;
    private readonly MorpionDbContext _db;

    public Game()
    {
        _board = new Board();
        _playerX = new Player('X');
        _playerO = new Player('O', isBot: true);
        _currentPlayer = _playerX;
        _db = new MorpionDbContext();
        _db.Database.EnsureCreated();
    }

    public async Task Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== JEU DE MORPION ===\n");
            DisplayStats();

            int choice = ShowMenu();

            switch (choice)
            {
                case 1:
                    ResetGame();
                    await PlayRound();
                    break;
                case 2:
                    if (!await ResumeGame())
                        Console.WriteLine("Aucune partie en cours à reprendre.");
                    break;
                case 3:
                    Console.WriteLine("Merci d'avoir joué !");
                    return;
            }

            Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu...");
            Console.ReadLine();
        }
    }

    private void DisplayStats()
    {
        int totalGames = _db.GameRecords.Count();
        int humanWins = _db.GameRecords.Count(g => g.Winner == "X");
        int botWins = _db.GameRecords.Count(g => g.Winner == "O");
        int draws = _db.GameRecords.Count(g => g.Winner == "Draw");

        Console.WriteLine($"Parties jouées : {totalGames}");
        if (totalGames > 0)
        {
            Console.WriteLine($"  Victoires Humain (X) : {humanWins} ({100 * humanWins / totalGames}%)");
            Console.WriteLine($"  Victoires Bot (O)    : {botWins} ({100 * botWins / totalGames}%)");
            Console.WriteLine($"  Matchs nuls          : {draws} ({100 * draws / totalGames}%)");
        }
        Console.WriteLine();
    }

    private int ShowMenu()
    {
        bool hasSavedGame = _db.GameStates.Any();

        Console.WriteLine("1. Nouvelle partie");
        if (hasSavedGame)
            Console.WriteLine("2. Reprendre la partie en cours");
        Console.WriteLine("3. Quitter");
        Console.Write("\nVotre choix : ");

        string? input = Console.ReadLine();
        if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 3)
            return choice;

        return ShowMenu();
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
                Console.WriteLine($"Le joueur {_currentPlayer.Symbol} a gagné !");
                SaveResult(_currentPlayer.Symbol.ToString());
                DeleteSavedGame();
                break;
            }

            if (_board.IsFull())
            {
                _board.Display();
                Console.WriteLine("Match nul ! Égalité !");
                SaveResult("Draw");
                DeleteSavedGame();
                break;
            }

            SwitchPlayer();
            SaveCurrentGame();
        }
    }

    private void SaveResult(string winner)
    {
        _db.GameRecords.Add(new GameRecord { Winner = winner });
        _db.SaveChanges();
    }

    private void SaveCurrentGame()
    {
        var existing = _db.GameStates.FirstOrDefault();
        if (existing != null)
        {
            existing.BoardCells = _board.ExportCells();
            existing.CurrentPlayer = _currentPlayer.Symbol;
            existing.SavedAt = DateTime.Now;
        }
        else
        {
            _db.GameStates.Add(new GameState
            {
                BoardCells = _board.ExportCells(),
                CurrentPlayer = _currentPlayer.Symbol
            });
        }
        _db.SaveChanges();
    }

    private void DeleteSavedGame()
    {
        var saved = _db.GameStates.ToList();
        if (saved.Any())
        {
            _db.GameStates.RemoveRange(saved);
            _db.SaveChanges();
        }
    }

    private async Task<bool> ResumeGame()
    {
        var saved = _db.GameStates.FirstOrDefault();
        if (saved == null) return false;

        _board = new Board();
        _board.ImportCells(saved.BoardCells);
        _currentPlayer = saved.CurrentPlayer == 'X' ? _playerX : _playerO;

        Console.WriteLine("\nReprise de la partie sauvegardée !\n");
        await PlayRound();
        return true;
    }

    private void ResetGame()
    {
        _board = new Board();
        _currentPlayer = _playerX;
        DeleteSavedGame();
    }

    private void SwitchPlayer()
    {
        _currentPlayer = _currentPlayer == _playerX ? _playerO : _playerX;
    }
}
