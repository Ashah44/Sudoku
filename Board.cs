using System.Collections;
using System;


//This is the game board class. This class will intialize and build the board for the user.
//As of right now it is an empty board 9 by 9 with dashes
//TODO:
//build a board with actual numbers to show sudoku board
//randomize the numbers that will be on the board so it is not manual
//error check the randomize to make sure it is solvable
//Implement the updating of the board and gameplay.

namespace Sudoku{
    class Board{

        private string[,] board;

        //builds an empty sudoku board 9 by 9
        private static string[,] buildEmptyboard(string[,] board){
        
           for(int i = 0; i< board.GetLength(0); i++){
               for(int j = 0; j < board.GetLength(1); j++){
                   board[i,j] = "-";        
               }
           }

           return board; 
        }

        //displays an empty board (9 by 9)
        public void displayBoard(string[,] board, int choice){

            //based on user choice a board will be printed.
            if(choice == 1){
                Console.WriteLine("Here is the easy difficulty board.");
            }
            else if(choice == 2){
                Console.WriteLine("Here is the medium difficulty board.");
            }
            else if(choice == 3){
                Console.WriteLine("Here is the hard difficulty board.");
            }
            else{
                //Console.WriteLine("Incorrect input.");
                board = buildEmptyboard(this.board);
                board = generateBoard(board);
            }

            for(int i = 0; i< board.GetLength(0); i++){
                for(int j = 0; j < board.GetLength(1); j++){
                    Console.Write(board[i,j] + " ");
                }
                Console.Write("\n");
            }
        }
        
        private static void easyBoard(string[,] board){


            
        }

        //generate a board for different diffuculty.
        //TODO:
        //check the 3 by 3 we are in, check the row and column
        //Start with an empty board.
        // Add a random number at one of the free cells (the cell is chosen randomly, and the number is chosen randomly from the list of 
        //numbers valid for this cell according to the SuDoKu rules).
        // Use the backtracking solver to check if the current board has at least one valid solution. If not, undo step 2 and repeat with another number and cell. 
        //Note that this step might produce full valid boards on its own, but those are in no way random.
        // Repeat until the board is completely filled with numbers.
        public string[,] generateBoard(string[,] board){

            var number = new Random();
            var rand = new Random();
            int count = 0;
            for(int i = 0; i < 81; i++){
                //random location on the board for row and col
                int row = rand.Next(0,9);
                int col = rand.Next(0,9);
                //valid to keep going since location is empty
                if(board[row, col] == "-"){
                    //now check if number generated is possible in that row, column or 3 by 3
                    int numToPlace = rand.Next(1,10);
                    //check rows
                    for(int j = 0; j < board.GetLength(0); j++){
                        //checks the row to see if number is present their
                        if(board[row, j].Equals(numToPlace.ToString())){
                            count = count + 1;
                        }
                        //checks the col to see if number is present their
                        if(board[j, col].Equals(numToPlace.ToString())){
                            count = count + 1;
                        }
                        //checks the 3 by 3 location of the associated row
                        int checkRowBox = row - row % 3;
                        int checkColBox = col - col % 3;
                        for(int k = checkRowBox; k < checkRowBox + (row % 3); k++ ){
                            for(int l = checkColBox; l < checkColBox + (col % 3); l++){
                                if(board[k,l].Equals(numToPlace.ToString())){
                                    count = count + 1;
                                }
                            }
                        }
                    }

                    //count of 0 means that number can be placed in that location.
                    //reset count afterwards
                    if(count == 0){
                        board[row,col] = numToPlace.ToString();
                    }
                    count = 0;
                }
            }

            return board;
        }


        //setter for gameboard
        public void setBoard(string[,] board){
            this.board = board;
        }

        //getter for the board
        public string[,] getBoard(){
            return board;
        }
        

    }
}
