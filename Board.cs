using System.Collections;
using System;


//This is the game board class. This class will intialize and build the board for the user.
//As of right now it is an empty board 9 by 9 with dashes
//TODO:
//build a board with actual numbers to show sudoku board
//randomize the numbers that will be on the board so it is not manual
//error check the randomize to make sure it is solvable


namespace Sudoku{
    class Board{

        private string[,] board;

        //builds an empty sudoku board 9 by 9
        private static string[,] buildboard(string[,] board){
        
           for(int i = 0; i< board.GetLength(0); i++){
               for(int j = 0; j < board.GetLength(1); j++){
                   board[i,j] = "-";        
               }
           }

           return board; 
        }

        //displays an empty board (9 by 9)
        public void displayBoard(string[,] board){

            board = buildboard(board);
            for(int i = 0; i< board.GetLength(0); i++){
                for(int j = 0; j < board.GetLength(1); j++){
                    Console.Write(board[i,j] + " ");
                }
                Console.Write("\n");
            }
        }
        
        //setter for the board
        public void setBoard(string[,] board){
            this.board = board;
        }

        //getter for the board
        public string[,] getBoard(){
            return board;
        }

    }
}
