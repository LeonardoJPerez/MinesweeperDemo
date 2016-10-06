using System;
using System.Threading;

namespace MineSweeperDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create matrix
            var board = CreateBoard();
            CheckForMines(board);

            Console.Read();
        }

        private static string[,] CreateBoard(int matrixSize = 10)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            var matrix = new string[matrixSize, matrixSize];

            for (int i = 0, j = 0; i < matrix.GetLength(0); i++)
            {
                matrix[i, j] = random.Next(10) % 5 == 0 ? "*" : "A";

                if (i + 1 != matrixSize) { continue; }
                j++;
                i = j < matrixSize ? -1 : i;
            }

            return matrix;
        }

        private static void CheckForMines(string[,] matrix)
        {
            var xLimit = matrix.GetLength(0);
            var yLimit = matrix.GetLength(1);

            for (var i = 0; i < xLimit; i++)
            {
                for (var j = 0; j < yLimit; j++)
                {
                    Thread.Sleep(10);

                    CheckPositionForMines(i, j, matrix);
                    Console.Write((j + 1) % xLimit == 0 ? $"{matrix[i, j]}\n" : $"{matrix[i, j]}");
                }
            }
        }

        private static void CheckPositionForMines(int x, int y, string[,] matrix)
        {
            // Is there a bomb
            if (matrix[x, y] == "*") { return; }

            var countBombs = 0;
            // Indexes represent the offsets from main point.
            var xLimit = matrix.GetLength(0);
            var yLimit = matrix.GetLength(1);

            for (var i = x - 1; i <= x + 1; i++)
            {
                if (i < 0 || i >= xLimit) { continue; }
                for (var j = y - 1; j <= y + 1; j++)
                {
                    if (j < 0 || j >= yLimit) { continue; }
                    if (matrix[i, j] == "*") { ++countBombs; }
                }
            }

            matrix[x, y] = countBombs.ToString();
        }
    }
}