using System.Data.Common;

public class Solution
{
    const char emptyValue = '.';

    static char[][] board1 = [
        ['♥', '♦', '♣', '.'],
        ['♠', '♣', '♦', '.'],
        ['♣', '♠', '♥', '♦'],
        ['♦', '.', '♠', '♥']
    ];

    static char[][] board2 = [
        ['5', '3', '.', '.', '7', '.', '.', '.', '.'],
        ['6', '.', '.', '1', '9', '5', '.', '.', '.'],
        ['.', '9', '2', '.', '.', '.', '.', '6', '.'],
        ['8', '.', '.', '.', '6', '.', '.', '.', '3'],
        ['4', '.', '.', '8', '.', '3', '.', '.', '1'],
        ['7', '.', '.', '.', '2', '.', '.', '.', '6'],
        ['.', '6', '.', '.', '.', '.', '2', '8', '.'],
        ['.', '.', '.', '4', '1', '9', '.', '.', '5'],
        ['.', '.', '.', '.', '8', '.', '.', '7', '9']
    ];

    static char[][] board3 = [
        ['0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F'],
        ['4','5','6','7','0','1','2','3','C','D','E','F','8','9','A','B'],
        ['8','9','A','B','C','D','E','F','0','1','2','3','4','5','6','7'],
        ['C','D','E','F','8','9','A','B','4','5','6','7','0','1','2','3'],

        ['1','0','3','2','5','4','7','6','9','8','B','A','D','C','F','E'],
        ['5','4','7','6','1','0','3','2','D','C','F','E','9','8','B','A'],
        ['9','8','B','A','D','C','F','E','1','0','3','2','5','4','7','6'],
        ['D','C','F','E','9','8','B','A','5','4','7','6','1','0','3','2'],

        ['2','3','0','1','6','7','4','5','A','B','8','9','E','F','C','D'],
        ['6','7','4','5','2','3','0','1','E','F','C','D','A','B','8','9'],
        ['A','B','8','9','E','F','C','D','2','3','0','1','6','7','4','5'],
        ['E','F','C','D','A','B','8','9','6','7','4','5','2','3','0','1'],

        ['3','2','1','0','7','6','5','4','B','A','9','8','F','E','D','C'],
        ['7','6','5','4','3','2','1','0','F','E','D','C','B','A','9','8'],
        ['B','A','9','8','F','E','D','C','3','2','1','0','7','6','5','4'],
        ['F','E','D','C','B','A','9','8','7','6','5','4','3','2','1','0']
    ];

    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("• Sudoku Validator •");

        while (true)
        {
            Console.WriteLine("\nEnter 1 for 4x4 board, 2 for 9x9 board, 3 for 16x16 board");
            int boardNumber = int.Parse(Console.ReadLine() ?? "1");
            var currentBoard = boardNumber switch
            {
                1 => board1,
                2 => board2,
                3 => board3,
                _ => board2
            };
            //currentBoard = board2;
            var solution = new Solution();
            solution.PrintBoard(currentBoard);
            var isValid = solution.IsValid(currentBoard);
            var isFilled = solution.IsFilled(currentBoard);
            Console.WriteLine($"Valid: {isValid}\nFilled: {isFilled}\nSolved: {isValid && isFilled}");
        }
    }

    public bool IsSolved(char[][] board)
    {
        return IsValid(board) && IsFilled(board);
    }

    public bool IsFilled(char[][] board)
    {
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                if (board[i][j] == emptyValue)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool IsValid(char[][] board)
    {
        //vaidate rows
        for (int i = 0; i < board.Length; i++)
        {
            if (IsValidLine(board[i]) == false)
            {
                Console.WriteLine("Dublicate in row " + (i + 1));
                return false;
            }

            //validate columns
            char[] column = new char[board[i].Length];
            for (int j = 0; j < board[i].Length; j++)
            {
                column[j] = board[j][i];
            }
            if (IsValidLine(column) == false)
            {
                Console.WriteLine("Dublicate in column " + (i + 1));
                return false;
            }
        }
        return IsValidSubBoxes(board);
    }

    public bool IsValidLine(char[] row)
    {
        char[] array = row.ToArray();
        Array.Sort(array);

        for (int i = 0; i < array.Length - 1; i++)
        {
            if (array[i] == emptyValue)
            {
                continue;
            }
            if (array[i] == array[i + 1])
            {
                return false;
            }
        }
        return true;
    }

    public bool IsValidSubBoxes(char[][] board)
    {
        int _subBordSize = (int)Math.Sqrt(board.Length);
        for (int boxRow = 0; boxRow < _subBordSize; boxRow++)//boxRow
        {
            for (int boxColumn = 0; boxColumn < _subBordSize; boxColumn++)//boxColumn
            {
                int subBoxCounter = 0;
                char[] subBox = new char[_subBordSize * _subBordSize];
                for (int i = boxRow * _subBordSize; i < boxRow * _subBordSize + _subBordSize; i++)//row
                {
                    for (int j = boxColumn * _subBordSize; j < boxColumn * _subBordSize + _subBordSize; j++)//column
                    {
                        subBox[subBoxCounter++] = board[i][j];
                    }
                }
                if (IsValidLine(subBox) == false)
                {
                    Console.WriteLine("Dublicate in subBox " + ++boxRow  + "," + ++boxColumn);
                    return false;
                }
            }
        }
        return true;
    }

    public void PrintBoard(char[][] board)
    {
        Console.WriteLine($"Sudoku:");
        int _subBordSize = (int)Math.Sqrt(board.Length);

        for (int i = 0; i < board.Length; i++)
        {
            if (i % _subBordSize == 0) { Console.WriteLine(new string('\u2500', board.Length * 2 + _subBordSize + 1)); }

            for (int j = 0; j < board[i].Length; j++)
            {
                if (j % _subBordSize == 0) { Console.Write("\u2502"); }
                Console.Write(board[i][j] + " ");
            }
            Console.Write("\u2502");
            Console.WriteLine();
        }
        Console.WriteLine(new string('\u2500', board.Length * 2 + _subBordSize + 1));
    }
}