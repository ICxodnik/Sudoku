using System.Data.Common;

public class Solution
{
    public static void Main()
    {
        char[][] board = [
            ['8', '3', '.', '.', '7', '.', '.', '.', '.'],
            ['6', '.', '.', '1', '9', '5', '.', '.', '.'],
            ['.', '9', '8', '.', '.', '.', '.', '6', '.'],
            ['8', '.', '.', '.', '6', '.', '.', '.', '3'],
            ['4', '.', '.', '8', '.', '3', '.', '.', '1'],
            ['7', '.', '.', '.', '2', '.', '.', '.', '6'],
            ['.', '6', '.', '.', '.', '.', '2', '8', '.'],
            ['.', '.', '.', '4', '1', '9', '.', '.', '5'],
            ['.', '.', '.', '.', '8', '.', '.', '7', '9']
        ];
        var result = IsValidSudoku(board);
        Console.WriteLine(result);
        Console.ReadLine();
    }

    public static bool IsValidSudoku(char[][] board)
    {
        //vaidate rows
        for (int i = 0; i < board.Length - 1; i++)
        {
            if (IsValidRow(board[i]) == false)
            {
                Console.WriteLine("Dublicate in row " + i);
                return false;
            }

            //validate columns
            char[] column = new char[board[i].Length];
            for (int j = 0; j <= board[i].Length - 1; j++)
            {
                column[j] = board[j][i];
            }
            if (IsValidRow(column) == false)
            {
                Console.WriteLine("Dublicate in column " + i);
                return false;
            }

        }
        return true;

    }

    public static bool IsValidRow(char[] row)
    {
        char[] array = (char[])row.Clone();
        array.Sort();

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

    // public bool IsValidSubBoxes(char[]column) {

    // }
}