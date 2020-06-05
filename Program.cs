using System;
using System.Collections;

//this class is just the main program where the game will be ran on
//TODO:
//put all the Class' together in here
namespace Sudoku{
    class Program{

        static void Main(string[] args){
            
            //variable to store board from board class
            string[,] testboard;
            int choice = 0;
            string user = "";

            Board board = new Board();
            board.setBoard(new string[9,9]);
            testboard = board.getBoard();

            Game gameplay = new Game();

            //starts game and lets user choose the game
            while(true){

                choice = gameplay.menu(testboard);
                if(choice == 1 | choice == 2 || choice == 3){
                    Console.WriteLine();
                    board.displayBoard(testboard, choice);
                    Console.ReadKey();
                }
                else if(choice == 4){
                    Console.WriteLine();
                }   
                else if(choice == 5){
                    Console.WriteLine();
                    gameplay.Instructions();
                    Console.ReadKey();               
                }  
                else if(choice == 6){
                    Console.WriteLine();
                    Console.WriteLine("Exiting Game...");
                    break;
                }

            }




        }
    }
}
