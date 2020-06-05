using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Welcome to my Sudoku Game.");
            //variable to store board from board class
            string[,] testboard;

            Board board = new Board();
            board.setBoard(new string[9,9]);
            testboard = board.getBoard();

            board.displayBoard(testboard);

            

        }
    }
}
