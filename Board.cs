using System.Collections;
using System;

namespace Sudoku
{
    class Board
    {

        private string[,] board;

        //builds an empty sudoku board 9 by 9
        private static string[,] buildboard(string[,] board){
        
           board = new string[9,9];
           for(int i = 0; i< board.GetLength(0); i++){
               for(int j = 0; j < board.GetLength(1); j++){
                   board[i,j] = "-";        
               }
           }

           return board; 
        }

        public void displayBoard(string[,] board){

           for(int i = 0; i< board.GetLength(0); i++){
               for(int j = 0; j < board.GetLength(1); j++){
                   Console.Write(board[i,j] + " ");
               }
               Console.Write("\n");
           }
        }

        public string[,] getBoard(){
            board = buildboard(board);
            return board;
        }

    }
}
