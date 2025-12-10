namespace Morpion;

public class Board
{
    private char[,] _cells;
    private int[][] _winningCombinations = new int[][]
    {
        new int[] { 0, 1, 2 }, // Ligne 1
        new int[] { 3, 4, 5 }, // Ligne 2
        new int[] { 6, 7, 8 }, // Ligne 3
        new int[] { 0, 3, 6 }, // Colonne 1
        new int[] { 1, 4, 7 }, // Colonne 2
        new int[] { 2, 5, 8 }, // Colonne 3
        new int[] { 0, 4, 8 }, // Diagonale \
        new int[] { 2, 4, 6 }  // Diagonale /
    };

    public Board()
    {
        _cells = new char[3, 3];
        Initialize();
    }
    
    private void Initialize()
    {
        int position = 1;
        for (int line = 0; line < 3; line++)
        {
            for (int column = 0; column < 3; column++)
            {
                _cells[line, column] = (char)(' ');
                position++;
            }
        }
    }
    
    public void Display()
    {
        Console.WriteLine();

        for (int line = 0; line < 3; line++)
        {
            Console.Write(" ");
            for (int column = 0; column < 3; column++)
            {
                Console.Write(_cells[line, column]);
                if (column < 2)
                {
                    Console.Write(" | ");
                }
            }
            Console.WriteLine();

            if (line < 2)
            {
                Console.WriteLine("---|---|---");
            }
        }

        Console.WriteLine();
    }
    
    public bool IsValidMove(int position)
    {
        if (position < 1 || position > 9) return false;

        int line = (position - 1) / 3;
        int column = (position - 1) % 3;

        char cell = _cells[line, column];
        return cell != 'X' && cell != 'O';
    }
    
    public void PlayMove(int position, char player)
    {
        int line = (position - 1) / 3;
        int column = (position - 1) % 3;
        _cells[line, column] = player;
    }
    
    public bool CheckWin(char player)
    {
        foreach (int[] combination in _winningCombinations)
        {
            if (GetCellAt(combination[0]) == player &&
                GetCellAt(combination[1]) == player &&
                GetCellAt(combination[2]) == player)
            {
                return true;
            }
        }
        return false;
    }
    
    private char GetCellAt(int position)
    {
        int line = position / 3;
        int column = position % 3;
        return _cells[line, column];
    }
}
