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
            bool winner;
            Board board = new Board();
            board.setBoard(new string[9,9]);
            testboard = board.getBoard();
            ArrayList checkMoves = new ArrayList();
            Game gameplay = new Game();

            //starts game and lets user choose the option
            while(true){

                choice = gameplay.menu(testboard);
                if(choice == 1 | choice == 2 | choice == 3){ //easy,medium, hard boards

                    testboard = board.choiceBoard(testboard, choice);
                    board.displayBoard(testboard);
                    break;
                    
                }
                else if(choice == 4){ //load game
                    Console.WriteLine();
                }   
                else if(choice == 5){ // Instructions
                    gameplay.Instructions();
                }  
                else if(choice == 6){ //Exit game
                    Console.WriteLine();
                    Console.WriteLine("Exiting Game...");
                    break;
                }

            }

            if(choice == 1 | choice == 2 | choice == 3){
                checkMoves = board.checkExistingMoves(testboard);
                testboard = board.inputBoard(testboard, checkMoves);
                //board.displayBoard(testboard);
                
            }




        }
    }
}
