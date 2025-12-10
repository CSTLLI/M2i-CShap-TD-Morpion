namespace Morpion;

public class Board
{
    private char[,] _cells;

    public Board()
    {
        _cells = new char[3, 3];
        Initialize();
    }

    // Étape 1 : Initialisation du plateau
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

    // Étape 2 : Affichage du plateau
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

    // Étape 4 : Validation d'un coup
    public bool IsValidMove(int position)
    {
        if (position < 1 || position > 9) return false;

        int line = (position - 1) / 3;
        int column = (position - 1) % 3;

        char cell = _cells[line, column];
        return cell != 'X' && cell != 'O';
    }

    // Étape 4 : Jouer un coup
    public void PlayMove(int position, char player)
    {
        int line = (position - 1) / 3;
        int column = (position - 1) % 3;
        _cells[line, column] = player;
    }
}
