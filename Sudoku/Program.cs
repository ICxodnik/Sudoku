using System.Data.Common;

public class Solution
{
    public static void Main()
    {
        char[][] board = [
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

        var solution = new Solution();
        var result = solution.IsValidSudoku(board);
        Console.WriteLine(result);
        Console.ReadLine();
    }

    const int _subBordSize = 3;

    public bool IsValidSudoku(char[][] board)
    {
        //vaidate rows
        for (int i = 0; i < board.Length; i++)
        {
            if (IsValidRow(board[i]) == false)
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
            if (IsValidRow(column) == false)
            {
                Console.WriteLine("Dublicate in column " + (i + 1));
                return false;
            }
        }
        return IsValidSubBoxes(board);

    }

    public bool IsValidRow(char[] row)
    {
        char[] array = (char[])row.ToArray();
        Array.Sort(array);

        for (int i = 0; i < array.Length - 1; i++)
        {
            if (array[i] == '.')
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
                if (IsValidRow(subBox) == false)
                {
                    Console.WriteLine("Dublicate in subBox " + boxRow + "," + boxColumn);
                    return false;
                }
            }
        }
        return true;
    }

}