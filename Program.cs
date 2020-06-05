using System;
using System.Collections;

//this class is just the main program where the game will be ran on
//TODO:
//put all the Class' together in here
namespace Sudoku{
    class Program{

        static void Main(string[] args){
            
            Console.WriteLine("Hello Welcome to my Sudoku Game.");
            //variable to store board from board class
            string[,] testboard;

            Board board = new Board();
            board.setBoard(new string[9,9]);
            testboard = board.getBoard();

            board.displayBoard(testboard, 2);
            

        }
    }
}
